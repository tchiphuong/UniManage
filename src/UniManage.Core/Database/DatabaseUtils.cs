using Dapper;
using DevExpress.Data.Filtering;
using DevExpress.Web.Mvc;
using Ivs.Web.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace UniManage.Core.Database
{
    public class SqlBuilder : ISerializable
    {
        private StringBuilder _sql = new StringBuilder();
        
        public SqlBuilder(string sql = null)
		{
            _sql.Append(sql);
        }

        public void Append(string value)
		{
            _sql.Append(value);
		}

        public void AppendLine(string value)
		{
            _sql.AppendLine(value);
		}

        public void AppendFiltering(GridViewFilteringState filtering)
        {
			if (filtering != null && !string.IsNullOrEmpty(filtering.FilterExpression))
            {
                var conditions = filtering.FilterExpression;
                var fieldColumns = GetFieldColumns();
                //foreach (var item in fieldColumns)
                //{
                //    conditions = conditions.ToUpper().Replace($"[{item.Key}]".ToUpper(), item.Value);
                //}
                var op = CriteriaOperator.Parse(conditions);
                var tt = op.GetType();
				//switch (tt.Name)
				//{
    //                case "FunctionOperator":
    //                    var f = op as FunctionOperator;
    //                    var or = f.Operands;
    //                    var orp = or[0] as OperandProperty;
    //                    orp.PropertyName = fieldColumns[orp.PropertyName.ToLower()];
    //                    break;
    //                case "GroupOperator":
    //                    var g = op as GroupOperator;
    //                    foreach (FunctionOperator g_fo in g.Operands)
    //                    {
    //                        var g_or = g_fo.Operands as CriteriaOperatorCollection;
    //                        var g_orp = g_or[0] as OperandProperty;
    //                        g_orp.PropertyName = fieldColumns[g_orp.PropertyName.ToLower()];
    //                    }
    //                    break;
    //                case "BinaryOperator":
    //                    var bo = op as BinaryOperator;
    //                    bo.LeftOperand = fieldColumns[bo.LeftOperand.ToString().Replace("[", string.Empty).Replace("]", string.Empty).ToLower()];
    //                    break;
    //                default:
				//		break;
				//}

				conditions = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetMsSqlWhere(op);
				foreach (var item in fieldColumns)
				{
					conditions = conditions.ToUpper().Replace($"\"{item.Key.ToUpper()}\"", $"\"{item.Value}\"");
				}
				_sql.Append($" AND {conditions.Replace("\"", string.Empty)}");
            }
        }

        public void AppendSorting(string defaultSort, GridViewColumnState sorting)
        {
			if (sorting != null && !string.IsNullOrEmpty(sorting.FieldName)/* && !string.IsNullOrEmpty(sorting.FilterExpression)*/)
			{
                //var fieldColumns = GetFieldColumns();
                var sortOrder = sorting.SortOrder == DevExpress.Data.ColumnSortOrder.Ascending ? "ASC" : "DESC";
                var column = sorting.FieldName.ToLower();
                _sql.Append($"ORDER BY {column} {sortOrder}");
            }
            else
			{
                _sql.Append($"ORDER BY {(!string.IsNullOrEmpty(defaultSort) ? defaultSort : "(SELECT NULL)")}");
            }
        }

        public Dictionary<string, string> GetFieldColumns()
		{
            var result = new Dictionary<string, string>();
			{
                var splFields = Regex.Matches(_sql.ToString().ToLower().Replace(Environment.NewLine, string.Empty), "(?=[a-zA-Z0-9._=\\,\\-\\(\\)\\[\\]\\']+\\s)(.*?)(?<=(as\\s+[a-zA-Z0-9]+\\,))");
				if (splFields.Count > 0)
				{
					foreach (Match d in splFields)
					{
                        var val = d.Value;
                        var c1 = Regex.Matches(val, "([a-zA-Z0-9._\\-\\=\\,\\[\\]\\(\\)\\'\\=\\<\\>\\+\\s]+as)");
                        var c2 = Regex.Matches(val, "(as\\s+[a-zA-Z0-9]+,)");

                        var sqlField = c1[0].Value;
                        var sqlColumn = c2[0].Value.TrimStart("as".ToCharArray()).TrimEnd(',').Trim();

                        sqlField = sqlField.TrimEnd("as".ToCharArray()).Trim();
						if (val.StartsWith("select"))
						{
                            sqlField = sqlField.TrimStart("select".ToCharArray()).Trim();
                        }
						if (sqlField.StartsWith("string_agg"))
						{
                            sqlField = sqlField.Replace(" ", "").Replace("string_agg(", string.Empty).Replace(",',')", string.Empty).Trim();
                        }
                        result.Add(sqlColumn, $"({sqlField})");
                    }
                }
			}

            return result;
		}

        public override string ToString()
		{
			return _sql.ToString();
		}
		
        public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}
	}
    public static class DatabaseUtils
    {
        public static DbConnection GetDBConnection(string host, string port, string database, string username, string password)
        {
            // Connection String.
            var connString = $"data source={host};initial catalog={database};persist security info=True;user id={username};password={password};multipleactiveresultsets=True;";
            DbConnection conn = new SqlConnection(connString);
           
            return conn;
        }

        private static CorePagingModel QueryPagingInfoBase(this DbContext dbContext, string query, object conditions)
		{
            CorePagingModel pageInfo;
			var baseSearch = conditions as CoreBaseSearchModel;
			//var queryTotal = $"SELECT COUNT(*) FROM ({query} OFFSET 0 ROWS) tb";
			var queryTotal = $"SELECT COUNT(1) FROM ({query}) tb";
			var totalRow = dbContext.connection.ExecuteScalar<int>(queryTotal, conditions, transaction: dbContext.transacion);

			pageInfo = new CorePagingModel(baseSearch.PageIndex, baseSearch.PageSize, totalRow);

			return pageInfo;
        }

        private static List<T> QueryPageBase<T>(this DbContext dbContext, string query, object conditions)
        {
            var result = default(List<T>);
            var baseSearch = conditions as CoreBaseSearchModel;
            // kiểm tra có sử dụng phân trang hay không
            if ((baseSearch != null || baseSearch.Paging != null) && baseSearch.IsPaging)
            {
				if (baseSearch.Paging != null)
				{
                    baseSearch.PageIndex = baseSearch.Paging.PageIndex + 1;
                }
                query = $"{query} OFFSET @FromRow ROWS FETCH NEXT @TakeRow ROWS ONLY";
            }

            var resultVal = dbContext.connection.Query<T>($"{query}", conditions, transaction: dbContext.transacion);
            if (resultVal != null && resultVal.Any())
            {
                result = resultVal.ToList();
            }
            return result;
        }

        /// <summary>
        /// Lấy thông tin phân trang
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="query"></param>
        /// <param name="conditions"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static CorePagingModel QueryPagingInfo(this DbContext dbContext, StringBuilder query, object conditions)
        {
            return dbContext.QueryPagingInfoBase($"{query}", conditions);
        }

        /// <summary>
        /// Lấy thông tin phân trang
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="query"></param>
        /// <param name="conditions"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static CorePagingModel QueryPagingInfo(this DbContext dbContext, SqlBuilder query, object conditions)
        {
            return dbContext.QueryPagingInfoBase($"{query}", conditions);
        }

        /// <summary>
        /// Phân trang dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="query"></param>
        /// <param name="conditions"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static List<T> QueryPage<T>(this DbContext dbContext, StringBuilder query, object conditions)
        {
            return dbContext.QueryPageBase<T>($"{query}", conditions);
        }

        /// <summary>
        /// Phân trang dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="query"></param>
        /// <param name="conditions"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static List<T> QueryPage<T>(this DbContext dbContext, SqlBuilder query, object conditions)
        {
            return dbContext.QueryPageBase<T>($"{query}", conditions);
		}

		public static T ExecuteScalarCustom<T>(this DbContext dbContext, SqlBuilder query, object conditions)
		{
            var sqlScalar = "SELECT @@ROWCOUNT";
            if (query.ToString().ToLower().StartsWith("insert"))
            {
                sqlScalar = "SELECT SCOPE_IDENTITY()";
            }

			return dbContext.connection.ExecuteScalar<T>($"{query};{sqlScalar}", conditions, transaction: dbContext.transacion);
		}

		/// <summary>
		/// Trả về datatable
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="query"></param>
		/// <param name="conditions"></param>
		/// <param name="transaction"></param>
		/// <returns></returns>
		public static DataTable ExecuteDataTable(this DbConnection connection, StringBuilder query, object conditions, SqlTransaction transaction = null)
        {
            return connection.ExecuteDataTable(query.ToString(), conditions, transaction);
        }

		/// <summary>
		/// Trả về datatable
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="query"></param>
		/// <param name="conditions"></param>
		/// <param name="transaction"></param>
		/// <returns></returns>
		public static DataTable ExecuteDataTable(this DbConnection connection, SqlBuilder query, object conditions, SqlTransaction transaction = null)
        {
            return connection.ExecuteDataTable(query.ToString(), conditions, transaction);
        }

        /// <summary>
        /// Trả về datatable
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="query"></param>
        /// <param name="conditions"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(this DbConnection connection, string query, object conditions, SqlTransaction transaction = null)
        {
            var result = new DataTable();
			var reader = connection.ExecuteReader($"{query}", conditions, transaction: transaction);
			if (reader.FieldCount > 0)
			{
				result.Load(reader);

				foreach (DataColumn col in result.Columns) col.ReadOnly = false;

			}
			return result;
        }

        /// <summary>
        /// Trả về dataset
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="query"></param>
        /// <param name="conditions"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this DbConnection connection, string query, object conditions, SqlTransaction transaction = null)
        {
            var result = new DataSet();
			var reader = connection.ExecuteReader($"{query}", conditions, transaction: transaction);
			if (reader.FieldCount > 0)
			{
				result = ConvertDataReaderToDataSet(reader);
			}
			return result;
        }
        

        /// <summary>
        /// Trả về dataset
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="query"></param>
        /// <param name="conditions"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this DbConnection connection, StringBuilder query, object conditions, SqlTransaction transaction = null)
        {
            var result = new DataSet();
			var reader = connection.ExecuteReader($"{query}", conditions, transaction: transaction);
			if (reader.FieldCount > 0)
			{
				result = ConvertDataReaderToDataSet(reader);
			}
			return result;
        }

        private static DataSet ConvertDataReaderToDataSet(IDataReader data)
        {
            DataSet ds = new DataSet();
            int i = 0;
            while (!data.IsClosed)
            {
                ds.Tables.Add("Table" + (i + 1));
                ds.EnforceConstraints = false;
                ds.Tables[i].Load(data);
                i++;
            }
            return ds;
        }

        public  static StringBuilder AppendOrderBy(this StringBuilder sqlBuilder, string sqlOrder)
		{
            return sqlBuilder.Append(!string.IsNullOrEmpty(sqlOrder) ? $" {sqlOrder} " : " ORDER BY (SELECT NULL) ");
        }
    }
}
