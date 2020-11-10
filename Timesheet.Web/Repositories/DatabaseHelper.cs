using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel;

namespace Timesheet.Web
{
    public class DatabaseHelper
    {
        private string cnnString = "";
        public string ConnectionString
        {
            get
            {
                if (cnnString == "")
                    cnnString = ConfigurationManager.AppSettings["ServerDB"];
                return cnnString;
            }
            set
            {
                cnnString = value;
            }
        }

        public string TestConnection()
        {
            this.commandType = CommandType.Text;
            try
            {
                this.ExecuteNonQuery("SELECT 1");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "";
        }

        SqlConnection cnn;
        private CommandType commandType = CommandType.StoredProcedure;
        private SqlTransaction trans = null;
        public CommandType SQLCommandType
        {
            get
            {
                return commandType;
            }
            set
            {
                commandType = value;
            }
        }

        private List<SqlParameter> Param = new List<SqlParameter>();

        public void ClearParameter()
        {
            Param.Clear();
        }

        public void AddParameter(string paramName, object value)
        {
            this.Param.Add(new SqlParameter(paramName, value));
        }

        public void AddParameterV2(string paramName, object value)
        {
            if (value == null) {
                value = DBNull.Value;
            }
            else if (value.GetType() == typeof(string))
            {
                if ((string)value == "")
                    value = DBNull.Value;
            }

            this.Param.Add(new SqlParameter(paramName, value));
        }

        public void AddParameterV3(SqlParameter p)
        {
            this.Param.Add(p);
        }

        public string GetOutParam(int index)
        {
            return this.Param[index].Value + "";
        }

        public DatabaseHelper()
        {
            cnn = new SqlConnection(this.ConnectionString);
        }

        public void BeginTransaction()
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            trans = cnn.BeginTransaction();
        }

        public void CommitTransaction()
        {
            trans.Commit();
            cnn.Close();
        }

        public void RollbackTransaction()
        {
            trans.Rollback();
            cnn.Close();
        }

        public DataTable ExecuteQueryDataTable(string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                cnn.Close();
                da.Dispose();

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (trans == null)
                    cnn.Close();
            }

            return dataTable;
        }

        public DataSet ExecuteQueryDataSet(string query)
        {
            DataSet dataTable = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                cnn.Close();
                da.Dispose();

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (trans == null)
                    cnn.Close();
            }

            return dataTable;
        }


        public int ExecuteNonQuery(string sql)
        {
            return this.ExecuteNonQuery(sql, this.Param);
        }

        private int ExecuteNonQuery(string sql, List<SqlParameter> param)
        {
            SqlCommand cmm = new SqlCommand(sql, cnn);
            cmm.CommandType = this.SQLCommandType;
            if (trans != null) cmm.Transaction = trans;

            int retValue = 0;

            for (int i = 0; i < param.Count; i++)
            {
                cmm.Parameters.Add(param[i]);
            }
            try
            {
                if (cnn.State == ConnectionState.Closed) cnn.Open();

                retValue = cmm.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (trans == null)
                    cnn.Close();
            }

            return retValue;
        }

        public object ExecuteScalar(string sql)
        {
            return this.ExecuteScalar(sql, this.Param);
        }

        private object ExecuteScalar(string sql, List<SqlParameter> param)
        {
            SqlCommand cmm = new SqlCommand(sql, cnn);
            cmm.CommandType = this.SQLCommandType;
            if (trans != null) cmm.Transaction = trans;
            object retValue = 0;

            for (int i = 0; i < param.Count; i++)
            {
                cmm.Parameters.Add(param[i]);
            }
            try
            {
                if (cnn.State == ConnectionState.Closed) cnn.Open();

                retValue = cmm.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (trans == null)
                    cnn.Close();
            }

            return retValue;
        }

        public DataTable ExecuteDataTable(string sql)
        {
            return this.ExecuteDataTable(sql, this.Param);
        }

        private DataTable ExecuteDataTable(string sql, List<SqlParameter> param)
        {
            SqlDataAdapter ad = new SqlDataAdapter(sql, cnn);
            DataTable dt = new DataTable();
            ad.SelectCommand.CommandTimeout = 1800;
            ad.SelectCommand.CommandType = this.SQLCommandType;
            if (trans != null) ad.SelectCommand.Transaction = trans;

            for (int i = 0; i < param.Count; i++)
            {
                ad.SelectCommand.Parameters.Add(param[i]);
            }
            try
            {
                if (cnn.State == ConnectionState.Closed) cnn.Open();

                ad.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (trans == null)
                    cnn.Close();
            }

            return dt;
        }

        public DataSet ExecuteDataSet(string sql)
        {
            return this.ExecuteDataSet(sql, this.Param);
        }

        private DataSet ExecuteDataSet(string sql, List<SqlParameter> param)
        {
            SqlDataAdapter ad = new SqlDataAdapter(sql, cnn);
            DataSet ds = new DataSet();
            ad.SelectCommand.CommandType = this.SQLCommandType;
            if (trans != null) ad.SelectCommand.Transaction = trans;

            for (int i = 0; i < param.Count; i++)
            {
                ad.SelectCommand.Parameters.Add(param[i]);
            }
            try
            {
                if (cnn.State == ConnectionState.Closed) cnn.Open();

                ad.Fill(ds);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (trans == null)
                    cnn.Close();
            }

            return ds;
        }


        #region Method CRUD ที่ใช้ในระบบทั่ว ๆ เอามาไว้เป็นทางเลือกในการเรียกใช้งาน
        public static List<T> QueryToList<T>(SqlConnection conn, string sql, List<SqlParameter> list_param = null)
        {
            if (sql.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr;
                var entities = new List<T>();

                try
                {
                    cmd = BuildQueryCommand(conn, sql, list_param);
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    cmd.Parameters.Clear();

                    if (dr != null && dr.HasRows)
                    {
                        var entity = typeof(T);
                        var propDict = new Dictionary<string, PropertyInfo>();
                        var props = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                        propDict = props.ToDictionary(p => p.Name.ToUpper(), p => p);

                        while (dr.Read())
                        {
                            var newObject = Activator.CreateInstance<T>();

                            Parallel.For(0, dr.FieldCount, index =>
                            {
                                if (propDict.ContainsKey(dr.GetName(index).ToUpper()))
                                {
                                    var info = propDict[dr.GetName(index).ToUpper()];
                                    if ((info != null) && info.CanWrite)
                                    {
                                        lock (newObject)
                                        {
                                            var val = dr.GetValue(index);
                                            if (val != DBNull.Value)
                                            {
                                                Type piType = info.PropertyType;
                                                if (piType.IsGenericType && piType.GetGenericTypeDefinition() == typeof(Nullable<>))
                                                {
                                                    info.SetValue(newObject, Convert.ChangeType(val, piType.GetGenericArguments()[0]), null);
                                                }
                                                else
                                                {
                                                    info.SetValue(newObject, Convert.ChangeType(val, piType), null);
                                                }
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    info.SetValue(newObject, null, null);
                                                }
                                                catch { }
                                            }
                                        }
                                    }
                                }
                            });

                            entities.Add(newObject);
                        }
                    }

                    return entities;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd = null;
                    dr = null;
                }
            }
            else
            {
                return null;
            }
        }

        public static void ExecuteSql(SqlConnection conn, string vsql, List<SqlParameter> list_param = null)
        {
            if (vsql.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = vsql;
                    cmd.CommandTimeout = 600;
                    cmd.Connection = conn;

                    if (list_param != null)
                    {
                        foreach (SqlParameter param in list_param)
                        {
                            if ((param.Direction == ParameterDirection.InputOutput) || (param.Direction == ParameterDirection.Input) && (param.Value == null))
                            {
                                param.Value = DBNull.Value;
                            }

                            cmd.Parameters.Add(param);
                        }
                    }

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    cmd = null;
                }
            }
        }

        public static void ExecuteSql(SqlConnection conn, string vsql, List<SqlParameter> list_param = null, SqlTransaction trans = null)
        {
            if (vsql.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = vsql;
                    cmd.CommandTimeout = 600;
                    cmd.Transaction = trans;
                    cmd.Connection = conn;

                    if (list_param != null)
                    {
                        foreach (SqlParameter param in list_param)
                        {
                            if ((param.Direction == ParameterDirection.InputOutput) || (param.Direction == ParameterDirection.Input) && (param.Value == null))
                            {
                                param.Value = DBNull.Value;
                            }

                            cmd.Parameters.Add(param);
                        }
                    }

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    if (trans != null) conn.Close();
                    cmd = null;
                }
            }
        }

        public static void ExecuteSqlProd(SqlConnection conn, string storedName, List<SqlParameter> list_param = null)
        {
            if (storedName.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedName;
                    cmd.CommandTimeout = 600;
                    cmd.Connection = conn;

                    if (list_param != null)
                    {
                        foreach (SqlParameter param in list_param)
                        {
                            if ((param.Direction == ParameterDirection.InputOutput) || (param.Direction == ParameterDirection.Input) && (param.Value == null))
                            {
                                param.Value = DBNull.Value;
                            }

                            cmd.Parameters.Add(param);
                        }
                    }

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    cmd = null;
                }
            }
        }

        public static void ExecuteSqlProd(SqlConnection conn, string storedName, List<SqlParameter> list_param = null, SqlTransaction trans = null)
        {
            if (storedName.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedName;
                    cmd.CommandTimeout = 600;
                    cmd.Transaction = trans;
                    cmd.Connection = conn;

                    if (list_param != null)
                    {
                        foreach (SqlParameter param in list_param)
                        {
                            if ((param.Direction == ParameterDirection.InputOutput) || (param.Direction == ParameterDirection.Input) && (param.Value == null))
                            {
                                param.Value = DBNull.Value;
                            }

                            cmd.Parameters.Add(param);
                        }
                    }

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    if (trans == null) conn.Close();
                    cmd = null;
                }
            }
        }

        /// <summary>
        /// การใช้งาน Method นี้ ใน Store Procedure 
        /// จะต้องมีการ Declare Parameter ชื่อว่า @ReturnValue ให้เป็น int และมีค่า Direct = out
        /// และประกาศไว้ท้ายสุด 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="storedName"></param>
        /// <param name="list_param"></param>
        /// <returns></returns>
        public static int ExecuteSqlProdRetureValue(SqlConnection conn, string storedName, List<SqlParameter> list_param)
        {
            if (storedName.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedName;
                    cmd.CommandTimeout = 600;
                    cmd.Connection = conn;

                    if (list_param != null)
                    {
                        foreach (SqlParameter param in list_param)
                        {
                            if ((param.Direction == ParameterDirection.InputOutput) || (param.Direction == ParameterDirection.Input) && (param.Value == null))
                            {
                                param.Value = DBNull.Value;
                            }

                            cmd.Parameters.Add(param);
                        }

                        SqlParameter paramReturn = new SqlParameter();
                        paramReturn.ParameterName = "@ReturnValue";
                        paramReturn.Direction = ParameterDirection.Output;
                        paramReturn.SqlDbType = SqlDbType.Int;
                        paramReturn.Value = -1;
                        cmd.Parameters.Add(paramReturn);
                    }

                    cmd.ExecuteNonQuery();

                    return (int)cmd.Parameters["@ReturnValue"].Value;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    cmd = null;
                }
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// การใช้งาน Method นี้ ใน Store Procedure 
        /// จะต้องมีการ Declare Parameter ชื่อว่า @ReturnValue ให้เป็น int และมีค่า Direct = out
        /// และประกาศไว้ท้ายสุด 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="storedName"></param>
        /// <param name="list_param"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteSqlProdRetureValueAsync(SqlConnection conn, string storedName, List<SqlParameter> list_param)
        {
            if (storedName.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedName;
                    cmd.CommandTimeout = 600;
                    cmd.Connection = conn;

                    if (list_param != null)
                    {
                        foreach (SqlParameter param in list_param)
                        {
                            if ((param.Direction == ParameterDirection.InputOutput) || (param.Direction == ParameterDirection.Input) && (param.Value == null))
                            {
                                param.Value = DBNull.Value;
                            }

                            cmd.Parameters.Add(param);
                        }

                        SqlParameter paramReturn = new SqlParameter();
                        paramReturn.ParameterName = "@ReturnValue";
                        paramReturn.Direction = ParameterDirection.Output;
                        paramReturn.SqlDbType = SqlDbType.Int;
                        paramReturn.Value = -1;
                        cmd.Parameters.Add(paramReturn);
                    }

                    await cmd.ExecuteNonQueryAsync();

                    return (int)cmd.Parameters["@ReturnValue"].Value;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    cmd = null;
                }
            }
            else
            {
                return -1;
            }
        }

        private static SqlCommand BuildQueryCommand(SqlConnection conn, string storeProcName, List<SqlParameter> list_param)
        {
            if (conn.State != ConnectionState.Open) conn.Open();

            SqlCommand cmd = new SqlCommand(storeProcName, conn);
            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;

            if (list_param != null)
            {
                foreach (SqlParameter param in list_param)
                {
                    if ((param.Direction == ParameterDirection.InputOutput || param.Direction == ParameterDirection.Input)
                        && (param.Value == null))
                    { param.Value = DBNull.Value; }

                    cmd.Parameters.Add(param);
                }
            }

            return cmd;
        }

        public int? ExecuteCustomSqlReturnValue(string sql) 
        {
            return this.ExecuteCustomReturnValue(sql,this.Param);
        }
        public int? ExecuteCustomReturnValue(string storedName, List<SqlParameter> list_param)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            if (storedName.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedName;
                    cmd.CommandTimeout = 600;
                    cmd.Connection = conn;

                    if (list_param != null)
                    {
                        foreach (SqlParameter param in list_param)
                        {
                            if ((param.Direction == ParameterDirection.InputOutput) || (param.Direction == ParameterDirection.Input) && (param.Value == null))
                            {
                                param.Value = DBNull.Value;
                            }

                            cmd.Parameters.Add(param);
                        }

                        SqlParameter paramReturn = new SqlParameter();
                        paramReturn.ParameterName = "@NEW_IDENTITY";
                        paramReturn.Direction = ParameterDirection.Output;
                        paramReturn.SqlDbType = SqlDbType.Int;
                        paramReturn.Value = -1;
                        cmd.Parameters.Add(paramReturn);
                    }

                    cmd.ExecuteNonQuery();

                    return (int?)cmd.Parameters["@NEW_IDENTITY"].Value;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    cmd = null;
                }
            }
            else
            {
                return -1;
            }
        }

        #endregion Method CRUD ที่ใช้ในระบบทั่ว ๆ เอามาไว้เป็นทางเลือกในการเรียกใช้งาน
    }

    public class MSSqlConnector : IDisposable
    {
        public SqlConnection conn;
        private SqlTransaction _tr;

        public MSSqlConnector()
        {
            conn = new SqlConnection(new DatabaseHelper().ConnectionString);
        }

        public MSSqlConnector(string driver)
        {
            conn = new SqlConnection(driver);

        }
        public SqlTransaction tr
        {
            get { return _tr; }
            set { _tr = value; }
        }

        public void BeginTrans()
        {
            if (conn.State != ConnectionState.Open) conn.Open();
            _tr = conn.BeginTransaction();
        }

        public void CommitTrans()
        {
            if (_tr != null) _tr.Commit();
        }

        public void RollBackTrans()
        {
            if (_tr != null) _tr.Rollback();
        }

        #region Open / Close Connection

        public void openConnection()
        {
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
        }

        public void closeConnection()
        {
            if (conn.State != System.Data.ConnectionState.Closed)
                conn.Close();
        }

        #endregion


        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }

    public static class Extension
    {
        public static DataTable ToDataTable<T>(this IList<T> list)
        {
            System.ComponentModel.PropertyDescriptorCollection props = System.ComponentModel.TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                System.ComponentModel.PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in list)
            {
                for (int i = 0; i < values.Length; i++)
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                table.Rows.Add(values);
            }
            return table;
        }
    }
}
