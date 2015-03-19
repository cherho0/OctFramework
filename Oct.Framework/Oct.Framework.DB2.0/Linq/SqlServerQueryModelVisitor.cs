using System;
using Oct.Framework.DB.DynamicObj;
using Oct.Framework.DB.Linq.Commandbuilder;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;

namespace Oct.Framework.DB.Linq
{
    internal class SqlServerQueryModelVisitor : QueryModelVisitorBase
    {
        protected readonly SqlServerQueryCommandBuilder commandBuilder;
        private string _tblName;
        #region QueryModelVisitor

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tblName"></param>
        public SqlServerQueryModelVisitor(string tblName)
        {
            _tblName = tblName;
            this.commandBuilder = new SqlServerQueryCommandBuilder();
        }

        #endregion

        #region Translate

        /// <summary>
        /// 将 QueryModel 翻译成查询命令
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        public SqlServerQueryCommandBuilder Translate( QueryModel queryModel )
        {
            this.VisitQueryModel( queryModel );

            return this.commandBuilder;
        }

        #endregion

        #region VisitQueryModel

        /// <summary>
        /// 解析 QueryModel
        /// </summary>
        /// <param name="queryModel"></param>
        public override void VisitQueryModel( QueryModel queryModel )
        {
            queryModel.SelectClause.Accept( this, queryModel );
            queryModel.MainFromClause.Accept( this, queryModel );

            this.VisitBodyClauses( queryModel.BodyClauses, queryModel );
            this.VisitResultOperators( queryModel.ResultOperators, queryModel );
        }

        #endregion

        #region VisitMainFromClause

        /// <summary>
        /// 解析 from 语句
        /// </summary>
        /// <param name="fromClause"></param>
        /// <param name="queryModel"></param>
        public override void VisitMainFromClause( MainFromClause fromClause, QueryModel queryModel )
        {
            string itemName = fromClause.ItemName.Contains( "<generated>" ) ? "t" : fromClause.ItemName.ToLower();
            var tblname = fromClause.ItemType.Name;

            this.commandBuilder.FromParts.Add(string.Format("{0} {1}",  _tblName, itemName));

            base.VisitMainFromClause( fromClause, queryModel );
        }

        #endregion

        #region VisitSelectClause

        /// <summary>
        /// 解析 select 语句
        /// </summary>
        /// <param name="selectClause"></param>
        /// <param name="queryModel"></param>
        public override void VisitSelectClause( SelectClause selectClause, QueryModel queryModel )
        {
            SqlServerSelectPartsCommandBuilder selectBuilder = new SqlServerSelectPartsCommandBuilder();

            SqlServerSelectClauseVisitor visitor = new SqlServerSelectClauseVisitor();

            visitor.Translate( selectClause.Selector, selectBuilder );

            commandBuilder.SelectPart = selectBuilder.ToString();

            base.VisitSelectClause( selectClause, queryModel );
        }

        #endregion

        #region VisitWhereClause

        /// <summary>
        /// 解析 Where 语句
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="queryModel"></param>
        /// <param name="index"></param>
        public override void VisitWhereClause( WhereClause whereClause, QueryModel queryModel, int index )
        {
            SqlServerWherePartsCommandBuilder whereBuilder = new SqlServerWherePartsCommandBuilder( this.commandBuilder.Parameters );

            SqlServerWhereClauseVisitor visitor = new SqlServerWhereClauseVisitor();

            visitor.Translate( whereClause.Predicate, whereBuilder );

            commandBuilder.WhereParts.Add( whereBuilder.ToString() );

            base.VisitWhereClause( whereClause, queryModel, index );
        }

        #endregion

        #region VisitOrderByClause

        /// <summary>
        /// 解析 orderby 语句
        /// </summary>
        /// <param name="orderByClause"></param>
        /// <param name="queryModel"></param>
        /// <param name="index"></param>
        public override void VisitOrderByClause( OrderByClause orderByClause, QueryModel queryModel, int index )
        {
            foreach( var ordering in orderByClause.Orderings )
            {
                SqlServerOrderByPartsCommandBuilder orderBuilder = new SqlServerOrderByPartsCommandBuilder();

                SqlServerOrderByClauseVisitor visitor = new SqlServerOrderByClauseVisitor();

                visitor.Translate( ordering.Expression, orderBuilder );

                string direction = ordering.OrderingDirection == OrderingDirection.Desc ? "desc" : "asc";

                commandBuilder.OrderByParts.Add( string.Format( "{0} {1}", orderBuilder.ToString(), direction ) );
            }

            base.VisitOrderByClause( orderByClause, queryModel, index );
        }

        #endregion

        #region VisitResultOperator

        /// <summary>
        /// Visits the result operator.
        /// </summary>
        /// <param name="resultOperator">Result operator.</param>
        /// <param name="queryModel">Query model.</param>
        /// <param name="index">Index.</param>
        public override void VisitResultOperator( ResultOperatorBase resultOperator, QueryModel queryModel, int index )
        {
            if( resultOperator is SkipResultOperator )
            {
                SkipResultOperator @operator = resultOperator as SkipResultOperator;
                this.commandBuilder.LimitParts.From = @operator.GetConstantCount();
            }
            else if( resultOperator is TakeResultOperator )
            {
                TakeResultOperator @operator = resultOperator as TakeResultOperator;
                this.commandBuilder.LimitParts.Count = @operator.GetConstantCount();
            }
            else if( resultOperator is CountResultOperator || resultOperator is LongCountResultOperator )
            {
                this.commandBuilder.IsCount = true;
            }
            else if( resultOperator is FirstResultOperator || resultOperator is SingleResultOperator )
            {
            }
            else if( resultOperator is DistinctResultOperator )
            {
                this.commandBuilder.IsDistinct = true;
            }
            else
            {
                if( resultOperator is AverageResultOperator )
                {
                    throw new NotSupportedException();
                }
                if( resultOperator is MaxResultOperator )
                {
                    throw new NotSupportedException();
                }
                if( resultOperator is MinResultOperator )
                {
                    throw new NotSupportedException();
                }
                if( resultOperator is SumResultOperator )
                {
                    throw new NotSupportedException();
                }
                if( resultOperator is ContainsResultOperator )
                {
                    throw new NotSupportedException();
                }
                if( resultOperator is DefaultIfEmptyResultOperator )
                {
                    throw new NotSupportedException();
                }
                if( resultOperator is ExceptResultOperator )
                {
                    throw new NotSupportedException();
                }
                if( resultOperator is GroupResultOperator )
                {
                    throw new NotSupportedException();
                }
                if( resultOperator is IntersectResultOperator )
                {
                    throw new NotSupportedException();
                }
                if( resultOperator is OfTypeResultOperator )
                {
                    throw new NotSupportedException();
                }
                if( resultOperator is UnionResultOperator )
                {
                    throw new NotSupportedException();
                }
            }
        }

        #endregion
    }
}
