using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Oct.Framework.Core.Common
{
    public class ExpressionHelper
    {
        #region 属性

        public IDictionary<string, object> Argument
        {
            get;
            private set;
        }

        public string SqlWhere
        {
            get;
            private set;
        }

        public SqlParameter[] Paras
        {
            get;
            private set;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string SetArgument(string name, string value)
        {
            name = string.Format("@{0}", name);

            string temp = name;

            while (this.Argument.ContainsKey(temp))
            {
                int code = Guid.NewGuid().GetHashCode();

                if (code < 0)
                    code *= -1;

                temp = name + code;
            }

            this.Argument[temp] = value;

            return temp;
        }

        /// <summary>
        /// 根据条件生成对应的sql查询操作符
        /// </summary>
        /// <param name="expressiontype"></param>
        /// <returns></returns>
        private string GetOperator(ExpressionType expressiontype)
        {
            switch (expressiontype)
            {
                case ExpressionType.And:
                    return "and";

                case ExpressionType.AndAlso:
                    return "and";

                case ExpressionType.Or:
                    return "or";

                case ExpressionType.OrElse:
                    return "or";

                case ExpressionType.Equal:
                    return "=";

                case ExpressionType.NotEqual:
                    return "<>";

                case ExpressionType.LessThan:
                    return "<";

                case ExpressionType.LessThanOrEqual:
                    return "<=";

                case ExpressionType.GreaterThan:
                    return ">";

                case ExpressionType.GreaterThanOrEqual:
                    return ">=";

                default:
                    throw new Exception(string.Format("不支持{0}此种运算符查找！", expressiontype));
            }
        }

        /// <summary>
        /// 解析表达式
        /// </summary>
        /// <param name="expression"></param>
        public void ResolveExpression(Expression expression)
        {
            this.Argument = new Dictionary<string, object>();
            this.SqlWhere = this.Resolve(expression);
            this.Paras = this.Argument.Select(x => new SqlParameter(x.Key, x.Value)).ToArray();
        }

        public SqlParameter[] GetParameters()
        {
            return Argument.Select(x => new SqlParameter(x.Key, x.Value)).ToArray();
        }

        private string Resolve(Expression expression)
        {
            if (expression is LambdaExpression)
                return this.Resolve((expression as LambdaExpression).Body);

            //1元运算
            if (expression is UnaryExpression)
            {
                var unary = expression as UnaryExpression;

                //解析 x => !x.Name.Contains("xxx") 或 !array.Contains(x.Name)这类
                if (unary.Operand is MethodCallExpression)
                    return this.ResolveLinqToObject(unary.Operand, false);

                //解析x => x.isDeletion这样的
                if (unary.Operand is MemberExpression && unary.NodeType == ExpressionType.Not)
                {
                    var constant = Expression.Constant(false);

                    return this.ResolveFunc(unary.Operand, constant, ExpressionType.Equal);
                }

                expression = unary.Operand;
            }

            //2元运算
            if (expression is BinaryExpression)
            {
                var binary = expression as BinaryExpression;

                //解析x => x.Name == "123"这类
                if (binary.Left is MemberExpression && binary.Right is ConstantExpression)
                    return this.ResolveFunc(binary.Left, binary.Right, binary.NodeType);

                //解析x => x.Name.Contains("xxx") == false这类
                if (binary.Left is MethodCallExpression && binary.Right is ConstantExpression)
                {
                    var value = (binary.Right as ConstantExpression).Value;

                    return this.ResolveLinqToObject(binary.Left, value, binary.NodeType);
                }

                //解析x => x.Date == DateTime.Now这类
                if (binary.Left is MemberExpression && binary.Right is MemberExpression)
                {
                    var lambda = Expression.Lambda(binary.Right);
                    var fn = lambda.Compile();
                    var value = Expression.Constant(fn.DynamicInvoke(null), binary.Right.Type);

                    return this.ResolveFunc(binary.Left, value, binary.NodeType);
                }
            }

            //静态或实例方法
            //解析x => x.Name.Contains("xxx") 或 array.Contains(x.Name)这类
            if (expression is MethodCallExpression)
            {
                var methodcall = expression as MethodCallExpression;

                return this.ResolveLinqToObject(methodcall, true);
            }

            var body = expression as BinaryExpression;

            if (body == null)
                throw new Exception(string.Format("无法解析{0}", expression));

            var left = this.Resolve(body.Left);
            var oper = this.GetOperator(body.NodeType);
            var right = this.Resolve(body.Right);

            var result = string.Format("({0} {1} {2})", left, oper, right);

            return result;
        }

        private string ResolveFunc(Expression left, Expression right, ExpressionType expressiontype)
        {
            var name = (left as MemberExpression).Member.Name;
            var value = (right as ConstantExpression).Value;
            var oper = this.GetOperator(expressiontype);
            var compName = this.SetArgument(name, value.ToString());

            var result = string.Format("({0} {1} {2})", name, oper, compName);

            return result;
        }

        private string ResolveLinqToObject(Expression expression, object value, ExpressionType? expressiontype = null)
        {
            var methodCall = expression as MethodCallExpression;
            var methodName = methodCall.Method.Name;

            switch (methodName.ToLower())
            {
                case "contains":
                    if (methodCall.Object != null)
                        return this.Like(methodCall);

                    return this.In(methodCall, value);

                case "count":
                    return this.Len(methodCall, value, expressiontype.Value);

                case "longcount":
                    return this.Len(methodCall, value, expressiontype.Value);

                default:
                    throw new Exception(string.Format("不支持{0}方法的查找！", methodName));
            }
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private string Like(MethodCallExpression expression)
        {
            var name = (expression.Object as MemberExpression).Member.Name;
            var value = string.Format("%{0}%", (expression.Arguments[0] as ConstantExpression).Value);
            var oper = "like";
            var compName = this.SetArgument(name.ToString(), value.ToString());

            var result = string.Format("({0} {1} {2})", name, oper, compName);

            return result;
        }

        /// <summary>
        /// 包含比较
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="isTrue"></param>
        /// <returns></returns>
        private string In(MethodCallExpression expression, object isTrue)
        {
            var argument1 = (expression.Arguments[0] as MemberExpression).Expression as ConstantExpression;
            var argument2 = expression.Arguments[1] as MemberExpression;
            var fieldArray = argument1.Value.GetType().GetFields().First();
            var array = fieldArray.GetValue(argument1.Value) as object[];
            var setInPara = new List<string>();

            for (int i = 0; i < array.Length; i++)
            {
                var key = this.SetArgument(string.Format("InParameter{0}", i), array[i].ToString());

                setInPara.Add(key);
            }

            var name = argument2.Member.Name;
            var oper = Convert.ToBoolean(isTrue) ? "in" : " not in";
            var compName = string.Join(",", setInPara);

            var result = string.Format("{0} {1} ({2})", name, oper, compName);

            return result;
        }

        /// <summary>
        /// 长度比较
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <param name="expressiontype"></param>
        /// <returns></returns>
        private string Len(MethodCallExpression expression, object value, ExpressionType expressiontype)
        {
            var name = (expression.Arguments[0] as MemberExpression).Member.Name;
            var oper = this.GetOperator(expressiontype);
            var compName = this.SetArgument(name.ToString(), value.ToString());
            var result = string.Format("len({0}) {1} {2}", name, oper, compName);
            return result;
        }

        #endregion
    }
}
