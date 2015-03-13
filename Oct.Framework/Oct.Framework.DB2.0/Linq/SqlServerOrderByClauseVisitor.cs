﻿using System;
using System.Linq.Expressions;
using Oct.Framework.DB.Linq.Commandbuilder;
using Remotion.Linq.Clauses.Expressions;
using Remotion.Linq.Clauses.ExpressionTreeVisitors;
using Remotion.Linq.Parsing;

namespace Oct.Framework.DB.Linq
{
    internal class SqlServerOrderByClauseVisitor : ThrowingExpressionTreeVisitor
    {
        private SqlServerOrderByPartsCommandBuilder commandBuilder;

        #region OrderByClauseVisitor

        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlServerOrderByClauseVisitor()
        {
        }

        #endregion

        #region Translate

        /// <summary>
        /// 翻译表达式
        /// </summary>
        /// <returns></returns>
        public void Translate( Expression expression, SqlServerOrderByPartsCommandBuilder commandBuilder )
        {
            this.commandBuilder = commandBuilder;

            this.VisitExpression( expression );
        }

        #endregion

        #region VisitQuerySourceReferenceExpression

        /// <summary>
        /// 解析查询源引用表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected override Expression VisitQuerySourceReferenceExpression( QuerySourceReferenceExpression expression )
        {
            this.commandBuilder.OrderByParts.Append( expression.ReferencedQuerySource.ItemName.ToLower() );

            return expression;
        }

        #endregion

        #region VisitMemberExpression

        /// <summary>
        /// 解析成员访问表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected override Expression VisitMemberExpression( MemberExpression expression )
        {
            this.VisitExpression( expression.Expression );

            this.commandBuilder.OrderByParts.AppendFormat( ".{0}{1}{2}", Constants.LeftQuote, expression.Member.Name, Constants.RightQuote );

            return expression;
        }

        #endregion

        #region CreateUnhandledItemException

        protected override Exception CreateUnhandledItemException<T>( T unhandledItem, string visitMethod )
        {
            string itemText = FormatUnhandledItem( unhandledItem );
            var message = string.Format( "The expression '{0}' (type: {1}) is not supported by this LINQ provider.", itemText, typeof( T ) );
            return new NotSupportedException( message );
        }

        #endregion

        #region FormatUnhandledItem<T>

        private string FormatUnhandledItem<T>( T unhandledItem )
        {
            var itemAsExpression = unhandledItem as Expression;
            return itemAsExpression != null ? FormattingExpressionTreeVisitor.Format( itemAsExpression ) : unhandledItem.ToString();
        }

        #endregion
    }
}
