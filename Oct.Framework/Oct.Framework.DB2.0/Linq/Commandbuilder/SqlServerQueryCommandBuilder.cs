using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Oct.Framework.DB.Linq.Commandbuilder
{
    internal class SqlServerQueryCommandBuilder : SqlServerCommandBuilder
    {
        public bool IsDistinct { get; set; }

        public bool IsCount { get; set; }

        public string SelectPart { get; set; }

        public IList<string> FromParts { get; private set; }

        public IList<string> WhereParts { get; private set; }

        public IList<string> OrderByParts { get; private set; }

        public SqlServerLimitPartsAggregator LimitParts { get; private set; }

       

        /// <summary>
        /// 准备参数
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="parameters">参数列表</param>
        public  List<SqlParameter> PrepareParameter()
        {
            if (parameters == null || parameters.Count == 0)
            {
                return new List<SqlParameter>();
            }
             List<SqlParameter>  sqlparameters = new List<SqlParameter>();
            foreach (var item in parameters)
            {
                SqlParameter parameter =new SqlParameter();

                parameter.ParameterName = item.Key;
                parameter.Value = item.Value == null ? DBNull.Value : item.Value;

                sqlparameters.Add(parameter);
            }
            return sqlparameters;
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlServerQueryCommandBuilder() : base()
        {
            this.FromParts = new List<string>();
            this.WhereParts = new List<string>();
            this.OrderByParts = new List<string>();
            this.LimitParts = new SqlServerLimitPartsAggregator();
        }

        #region OnPageQuery

        /// <summary>
        /// 
        /// </summary>
        private string OnPageQuery()
        {
            if( OrderByParts.Count == 0 )
                throw new NotSupportedException( "在 SQL Server 中进行分页查询，必须指定排序字段。" );

            var builder = new StringBuilder();

            builder.Append( "select " );

            if( this.IsDistinct )
            {
                builder.Append( "distinct " );
            }

            builder.Append( this.SelectPart );

            builder.AppendFormat( " from {0} ", string.Join( ", ", this.FromParts ) );

            if( this.WhereParts.Count > 0 )
            {
                builder.AppendFormat( "where {0} ", string.Join( " and ", this.WhereParts ) );
            }

            string orderBy = string.Empty;

            for( int i = 0; i < OrderByParts.Count; i++ )
            {
                if( i == 0 )
                {
                    orderBy = orderBy + OrderByParts[i].Remove( 0, 2 );
                }
                else
                {
                    orderBy = orderBy + ", " + OrderByParts[i].Remove( 0, 2 );
                }
            }

            string fields = this.IsCount ? "count(*)" : "*";

            string sql = string.Format( "select {0} from ( select *, row_number() over( order by {1} ) as RowNumber from ( {2} ) a ) t where RowNumber between {3} and {4} ", fields, orderBy, builder.ToString(), this.LimitParts.From + 1, this.LimitParts.From + this.LimitParts.Count );

            return sql;
        }

        #endregion

        #region OnNonPageQuery

        private string OnNonPageQuery()
        {
            var builder = new StringBuilder();

            builder.Append( "select " );

            if( this.IsDistinct )
            {
                builder.Append( "distinct " );
            }

            builder.Append( this.IsCount ? "count(*)" : this.SelectPart );

            builder.AppendFormat( " from {0} ", string.Join( ", ", this.FromParts ) );

            if( this.WhereParts.Count > 0 )
            {
                builder.AppendFormat( "where {0} ", string.Join( " and ", this.WhereParts ) );
            }

            if( OrderByParts.Count > 0 )
            {
                builder.AppendFormat( "order by {0} ", string.Join( ", ", OrderByParts ) );
            }

            return builder.ToString();
        }

        #endregion

        #region ToString

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="SqlServerQueryCommandBuilder"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="SqlServerQueryCommandBuilder"/>.</returns>

        public override string ToString()
        {
            // 如果是分页查询
            if( this.LimitParts.From != 0 || this.LimitParts.Count != 0 )
            {
                return OnPageQuery();
            }

            return OnNonPageQuery();
        }

        #endregion
    }
}
