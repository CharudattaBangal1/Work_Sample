using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class DAL
{
    private SqlConnection objCon;
    private SqlCommand objCom;
    private SqlDataReader objDtReader;
    private SqlParameter objParm;
    int return_value;
    SqlDataAdapter objAdp;

    private string constr = System.Configuration.ConfigurationManager.ConnectionStrings["xxx"].ToString();
    private string constr1 = System.Configuration.ConfigurationManager.ConnectionStrings["xxx"].ToString();
    ////new 
    private string constr2 = System.Configuration.ConfigurationManager.ConnectionStrings["xxx"].ToString();

    private string constr3 = System.Configuration.ConfigurationManager.ConnectionStrings["xxx"].ToString();
    private string strValue;

    public class Collections
    {
        string Name;
        int size;
        object type;
        public Collections()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public Collections(string Name, int size, object type)
        {
            this.Name = Name;
            this.size = size;
            this.type = type;
        }
        public string GetName()
        {
            return Name;
        }
        public int GetSize()
        {
            return size;
        }
        public new object GetType()
        {
            return type;
        }
    }

    public void exec_command(string strSql, Hashtable htable)
    {
        try
        {
            objCon = new SqlConnection(constr);
            objCom = new SqlCommand(strSql, objCon);
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            //objCon.ConnectionTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString());
            foreach (DictionaryEntry dist in htable)
            {
                objCom.Parameters.AddWithValue((string)dist.Key, dist.Value);
            }
            objCon.Open();
            objCom.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            objCon.Close();
        }

    }
    public object exec_Scaler(string strSql, Hashtable htable)
    {
        object obj;
        try
        {
            objCon = new SqlConnection(constr);
            objCom = new SqlCommand(strSql, objCon);
            //objCon.ConnectionTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString());
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            foreach (DictionaryEntry dist in htable)
            {
                objCom.Parameters.AddWithValue((string)dist.Key, dist.Value);
            }
            objCon.Open();
            obj = objCom.ExecuteScalar();

        }
        catch (Exception ert)
        {
            string s = ert.Message.ToString();
            throw;
        }
        finally
        {
            //objCon.Close();
        }
        return obj;

    }

    public long exec_command(string strSql)
    {
        long retvalue;
        try
        {
            objCon = new SqlConnection(constr);
            if (objCon.State == ConnectionState.Open)
            {
                objCon.Close();
            }

            objCom = new SqlCommand(strSql, objCon);
            //objCon.ConnectionTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString());
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            objCon.Open();
            retvalue = Convert.ToUInt32(objCom.ExecuteScalar());
            objCom.Dispose();
        }
        catch
        {
            throw;
        }//nitin 28
        finally
        {
            objCon.Close();
        }
        return retvalue;
    }

    public long exec_ins_command(string strSql)
    {
        long retvalue;
        try
        {
            objCon = new SqlConnection(constr);
            if (objCon.State == ConnectionState.Open)
            {
                objCon.Close();
            }

            objCom = new SqlCommand(strSql, objCon);
            //objCon.ConnectionTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString());
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            objCon.Open();
            retvalue = Convert.ToUInt32(objCom.ExecuteNonQuery());
            objCom.Dispose();
        }
        catch
        {
            throw;
        }//nitin 28
        finally
        {
            objCon.Close();
        }
        return retvalue;
    }

    public long Common_exec_command(string strSql)
    {
        long retvalue;
        try
        {
            objCon = new SqlConnection(constr1);
            if (objCon.State == ConnectionState.Open)
            {
                objCon.Close();
            }

            objCom = new SqlCommand(strSql, objCon);
            //objCon.ConnectionTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString());
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            objCon.Open();
            retvalue = Convert.ToUInt32(objCom.ExecuteScalar());
            objCom.Dispose();
        }
        catch
        {
            throw;
        }//nitin 28
        finally
        {
            objCon.Close();
        }
        return retvalue;
    }

    public SqlDataReader exec_reader(string strSql)
    {

        try
        {
            objCon = new SqlConnection(constr);
            if (objCon.State == ConnectionState.Open)
            {
                objCon.Close();
            }

            objCom = new SqlCommand(strSql, objCon);
            //objCon.ConnectionTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString());
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            objCon.Open();
            objDtReader = objCom.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch
        {
            throw;
        }//nitin 28
        finally
        {
            // GC.Collect();
            //objCon.Close();
        }
        return objDtReader;

    }

    public SqlDataReader exec_reader(string strSql, Hashtable htable, string strDBKey)
    {

        try
        {
            objCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[strDBKey].ToString());
            if (objCon.State == ConnectionState.Open)
            {
                objCon.Close();
            }

            objCom = new SqlCommand(strSql, objCon);
            //objCon.ConnectionTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString());
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            foreach (DictionaryEntry dist in htable)
            {
                objCom.Parameters.AddWithValue((string)dist.Key, dist.Value);
            }
            objCon.Open();
            objDtReader = objCom.ExecuteReader(CommandBehavior.CloseConnection);

        }
        catch
        {
            throw;
        }//nitin 28
        finally
        {
            //objCon.Close();
            //GC.Collect();
        }
        return objDtReader;

    }


    public SqlDataReader Common_exec_reader(string strSql)
    {

        try
        {
            objCon = new SqlConnection(constr1);

            if (objCon.State == ConnectionState.Open)
            {
                objCon.Close();
            }

            objCom = new SqlCommand(strSql, objCon);
            //objCon.ConnectionTimeout = System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString();
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            objCon.Open();
            objDtReader = objCom.ExecuteReader(CommandBehavior.CloseConnection);

        }
        catch
        {
            throw;
        }//nitin 28
        finally
        {
            //GC.Collect();
            //objCon.Close();
        }
        return objDtReader;

    }

    public SqlDataReader exec_reader_prc(string strSql)
    {
        try
        {
            objCon = new SqlConnection(constr);
            if (objCon.State == ConnectionState.Open)
            {
                objCon.Close();
            }

            objCom = new SqlCommand();
            //objCon.ConnectionTimeout = System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString();
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            objCom.Connection = objCon;
            objCom.CommandText = strSql;
            objCom.CommandType = CommandType.StoredProcedure;
            objCon.Open();
            objDtReader = objCom.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch
        {
            throw;
        }//nitin 28
        finally
        {
            //GC.Collect();
            //objCon.Close();
        }
        return objDtReader;

    }

    public SqlDataReader exec_reader(string strSql, Hashtable htable)
    {
        try
        {
            objCon = new SqlConnection(constr);
            if (objCon.State == ConnectionState.Open)
            {
                objCon.Close();
            }

            objCom = new SqlCommand(strSql, objCon);
            //objCon.ConnectionTimeout = System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString();
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            foreach (DictionaryEntry dist in htable)
            {
                objCom.Parameters.AddWithValue((string)dist.Key, dist.Value);
            }
            objCon.Open();
            objDtReader = objCom.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch
        {
            throw;
        }//nitin 28
        finally
        {
            //GC.Collect();
            //objCon.Close();
        }
        return objDtReader;


    }

    public SqlDataReader exec_reader_prc(string strSql, Hashtable htable)
    {
        try
        {
            objCon = new SqlConnection(constr);
            if (objCon.State == ConnectionState.Open)
            {
                objCon.Close();
            }

            objCom = new SqlCommand();
            //objCon.ConnectionTimeout = System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString();
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            objCom.CommandText = strSql;
            objCom.CommandType = CommandType.StoredProcedure;
            objCom.Connection = objCon;
            foreach (DictionaryEntry dist in htable)
            {
                objCom.Parameters.AddWithValue((string)dist.Key, dist.Value);
            }
            objCon.Open();
            objDtReader = objCom.ExecuteReader(CommandBehavior.CloseConnection);

        }
        catch
        {
            throw;
        }//nitin 28
        finally
        {
            //GC.Collect();
            //objCon.Close();
        }
        return objDtReader;

    }

    public int execute_sprc(string sprc_name, Hashtable htable)
    {
        try
        {
            objCon = new SqlConnection(constr1);
            objCom = new SqlCommand(sprc_name, objCon);
            //objCon.ConnectionTimeout = System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString();
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            objCom.CommandType = CommandType.StoredProcedure;
            foreach (DictionaryEntry dist in htable)
            {
                objCom.Parameters.AddWithValue((string)dist.Key, dist.Value);
            }
            objCon.Open();
            return objCom.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            objCon.Close();
        }
    }

    public int execute_sprc(string sprc_name, Hashtable htable, string strDBKey)
    {
        int retvalue;

        objCon = new SqlConnection(Convert.ToString(ConfigurationManager.ConnectionStrings[strDBKey]));
        objCom = new SqlCommand(sprc_name, objCon);
        //objCon.ConnectionTimeout = System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString();
        objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        objCom.CommandType = CommandType.StoredProcedure;
        foreach (DictionaryEntry dist in htable)
        {
            objCom.Parameters.AddWithValue((string)dist.Key, dist.Value);
        }
        objCon.Open();
        retvalue = objCom.ExecuteNonQuery();
        //nitin 28
        objCon.Close();
        return retvalue;
    }

    public SqlDataReader Common_exec_reader_prc(string strSql, Hashtable htable)
    {
        try
        {
            objCon = new SqlConnection(constr1);
            if (objCon.State == ConnectionState.Open)
            {
                objCon.Close();
            }

            objCom = new SqlCommand();
            //objCon.ConnectionTimeout = System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString();
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            objCom.CommandText = strSql;
            objCom.CommandType = CommandType.StoredProcedure;
            objCom.Connection = objCon;
            foreach (DictionaryEntry dist in htable)
            {
                objCom.Parameters.AddWithValue((string)dist.Key, dist.Value);
            }
            objCon.Open();
            objDtReader = objCom.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch
        {
            throw;
        }
        finally
        {
            //GC.Collect();
        }
        return objDtReader;
        //objCon.Close();
    }

    public int execute_sprc_with_return(string sprc_name, Hashtable htable)
    {
        try
        {
            objCon = new SqlConnection(constr1);
            objCom = new SqlCommand(sprc_name, objCon);
            //objCon.ConnectionTimeout = System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString();
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            objCom.CommandType = CommandType.StoredProcedure;
            objParm = objCom.Parameters.AddWithValue("ReturnValue", DbType.Int16);
            objParm.Direction = ParameterDirection.ReturnValue;
            foreach (DictionaryEntry dist in htable)
            {
                objCom.Parameters.AddWithValue((string)dist.Key, dist.Value);
            }
            objCon.Open();
            objCom.ExecuteNonQuery();
            return_value = (int)objCom.Parameters["ReturnValue"].Value;
            //  objCon.Close();
        }
        catch
        {
            throw;
        }
        finally
        {
            objCon.Close();
        }
        return return_value;
    }

    public string execute_sprc_with_output(string sprc_name, Hashtable htable, string parm_name)
    {
        try
        {
            objCon = new SqlConnection(constr);
            objCom = new SqlCommand(sprc_name, objCon);
            //objCon.ConnectionTimeout = System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString();
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            objCon.Open();
            objCom.CommandType = CommandType.StoredProcedure;
            foreach (DictionaryEntry dist in htable)
            {
                objCom.Parameters.AddWithValue((string)dist.Key, dist.Value);
            }
            objParm = objCom.Parameters.AddWithValue(parm_name, DbType.String.ToString());
            objParm.Size = 60;
            objParm.Direction = ParameterDirection.Output;
            objCom.ExecuteNonQuery();
            if (objCom.Parameters[parm_name].Value != null)
            {
                strValue = objCom.Parameters[parm_name].Value.ToString();
            }
            else
            {
                strValue = "INVALID";
            }
        }
        catch
        {
            throw;
        }
        finally
        {
            objCon.Close();
        }
        return strValue;

    }
    public object[] execute_sprc_with_output(string sprc_name, Hashtable htable, ArrayList arrLst)
    {
        object[] paramValue;
        try
        {
            Collections[] arrCollection = new Collections[arrLst.Count];
            objCon = new SqlConnection(constr);
            objCom = new SqlCommand(sprc_name, objCon);
            //objCon.ConnectionTimeout = System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString();
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            objCom.CommandType = CommandType.StoredProcedure;
            foreach (DictionaryEntry dict in htable)
            {
                objCom.Parameters.AddWithValue((string)dict.Key, dict.Value);
            }
            foreach (Collections obj in arrLst)
            {
                objParm = objCom.Parameters.AddWithValue(obj.GetName(), obj.GetType());
                objParm.Size = obj.GetSize();
                objParm.Scale = 2;
                objParm.Direction = ParameterDirection.Output;
            }
            for (int i = 0; i <= (arrLst.Count - 1); i++)
            {
                arrCollection[i] = (Collections)arrLst[i];
            }
            paramValue = new object[arrLst.Count];
            if (objCon.State != ConnectionState.Open)
            {
                objCon.Open();
            }
            objCom.ExecuteNonQuery();
            for (int i = 0; i <= (arrCollection.Length - 1); i++)
            {
                if (objCom.Parameters[arrCollection[i].GetName()].Value != null)
                {
                    paramValue[i] = objCom.Parameters[arrCollection[i].GetName()].Value;
                }
            }
        }
        catch
        {
            throw;
        }
        finally
        {
            objCon.Close();
        }
        return paramValue;
    }

    public DataSet GetDataSet(string strSql, Hashtable htable)
    {
        DataSet dsResult = new DataSet();
        try
        {
            objAdp = new SqlDataAdapter(strSql, constr);
            objAdp.SelectCommand.CommandTimeout = 0;
            foreach (DictionaryEntry dist in htable)
            {
                objAdp.SelectCommand.Parameters.AddWithValue((string)dist.Key, dist.Value);
            }
            //objComBldr=new SqlCommandBuilder(objAdp);
            objAdp.Fill(dsResult);

        }
        catch
        {
            throw;
        }
        return dsResult;
    }

    public DataSet GetDataSetForPrc(string strSql)
    {
        DataSet dsResult = new DataSet();
        SqlCommand cmdObj = new SqlCommand();
        cmdObj.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlConnection conn = new SqlConnection(constr1);
        //conn.ConnectionTimeout = System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString();
        cmdObj.CommandText = strSql;
        cmdObj.CommandType = CommandType.StoredProcedure;
        cmdObj.Connection = conn;
        try
        {
            objAdp = new SqlDataAdapter(cmdObj);
            objAdp.SelectCommand.CommandTimeout = 0;
            //foreach (DictionaryEntry dist in htable)
            //{
            //    objAdp.SelectCommand.Parameters.AddWithValue((string)dist.Key, dist.Value);
            //}
            ////objComBldr=new SqlCommandBuilder(objAdp);
            objAdp.Fill(dsResult);

        }
        catch
        {
            throw;
        }
        return dsResult;
    }

    public DataSet GetDataSetForPrc(string strSql, Hashtable htable)
    {
        DataSet dsResult = new DataSet();
        SqlCommand cmdObj = new SqlCommand();
        cmdObj.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlConnection conn = new SqlConnection(constr1);
        //conn.ConnectionTimeout = System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString();
        cmdObj.CommandText = strSql;
        cmdObj.CommandType = CommandType.StoredProcedure;
        cmdObj.Connection = conn;
        try
        {

            objAdp = new SqlDataAdapter(cmdObj);
            objAdp.SelectCommand.CommandTimeout = 0;
            foreach (DictionaryEntry dist in htable)
            {
                objAdp.SelectCommand.Parameters.AddWithValue((string)dist.Key, dist.Value);
            }
            objAdp.Fill(dsResult);

        }
        catch
        {
            throw;
        }
        return dsResult;
    }

    public DataSet GetDataSetForPrcDBConn(string strSql, Hashtable htable, string strDBKey)
    {
        DataSet dsResult = new DataSet();
        SqlCommand cmdObj = new SqlCommand();
        cmdObj.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[strDBKey].ToString());
        //conn.ConnectionTimeout = System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString();
        cmdObj.CommandText = strSql;
        cmdObj.CommandType = CommandType.StoredProcedure;
        cmdObj.Connection = conn;
        try
        {
            objAdp = new SqlDataAdapter(cmdObj);
            objAdp.SelectCommand.CommandTimeout = 0;
            foreach (DictionaryEntry dist in htable)
            {
                objAdp.SelectCommand.Parameters.AddWithValue((string)dist.Key, dist.Value);
            }
            objAdp.Fill(dsResult);

        }
        catch
        {
            throw;
        }
        return dsResult;
    }

    public DataSet GetDataSet(string strSql, Hashtable htable, string strTblName)
    {
        DataSet dsResult = new DataSet();
        try
        {
            objAdp = new SqlDataAdapter(strSql, constr);
            objAdp.SelectCommand.CommandTimeout = 0;
            foreach (DictionaryEntry dist in htable)
            {
                objAdp.SelectCommand.Parameters.AddWithValue((string)dist.Key, dist.Value);
            }
            //objComBldr=new SqlCommandBuilder(objAdp);
            objAdp.Fill(dsResult, strTblName);

        }
        catch
        {
            throw;
        }
        return dsResult;
    }

    public DataSet GetDataSetWithoutParam(string strSql, string strConnKey)
    {
        DataSet dsResult = new DataSet();
        try
        {
            objAdp = new SqlDataAdapter(strSql, System.Configuration.ConfigurationManager.ConnectionStrings[strConnKey].ToString());
            objAdp.SelectCommand.CommandTimeout = 0;
            objAdp.Fill(dsResult);
        }
        catch
        {
            throw;
        }
        return dsResult;
    }

    public SqlDataReader exec_reader1(string strSql)
    {
        try
        {
            objCon = new SqlConnection(constr2);
            if (objCon.State == ConnectionState.Open)
            {
                objCon.Close();
            }

            objCom = new SqlCommand(strSql, objCon);
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            objCon.Open();
            objDtReader = objCom.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch
        {
            throw;
        }
        return objDtReader;
    }
    public DataSet GetDataSetForPrc1(string strSql, Hashtable htable)
    {
        DataSet dsResult = new DataSet();
        SqlCommand cmdObj = new SqlCommand();
        cmdObj.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlConnection conn = new SqlConnection(constr);
        cmdObj.CommandText = strSql;

        cmdObj.CommandType = CommandType.StoredProcedure;
        cmdObj.Connection = conn;
        try
        {
            objAdp = new SqlDataAdapter(cmdObj);
            objAdp.SelectCommand.CommandTimeout = 0;
            foreach (DictionaryEntry dist in htable)
            {
                objAdp.SelectCommand.Parameters.AddWithValue((string)dist.Key, dist.Value);
            }
            objAdp.Fill(dsResult);

        }
        catch
        {
            throw;
        }
        return dsResult;
    }

    public SqlDataReader exec_reader_prc1(string strSql, Hashtable htable)
    {
        try
        {
            objCon = new SqlConnection(constr2);
            if (objCon.State == ConnectionState.Open)
            {
                objCon.Close();
            }

            objCom = new SqlCommand();
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            objCom.CommandText = strSql;

            objCom.CommandType = CommandType.StoredProcedure;
            objCom.Connection = objCon;
            foreach (DictionaryEntry dist in htable)
            {
                objCom.Parameters.AddWithValue((string)dist.Key, dist.Value);
            }
            objCon.Open();
            objDtReader = objCom.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch
        {
            throw;
        }
        return objDtReader;
    }

    public int execute_sprc1(string sprc_name, Hashtable htable)
    {
        try
        {
            objCon = new SqlConnection(constr2);
            objCom = new SqlCommand(sprc_name, objCon);
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            objCom.CommandType = CommandType.StoredProcedure;
            foreach (DictionaryEntry dist in htable)
            {
                objCom.Parameters.AddWithValue((string)dist.Key, dist.Value);
            }
            objCon.Open();
            return objCom.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            objCon.Close();
        }
    }
    public DataSet GetDataSetForPrc1(string strSql)
    {
        DataSet dsResult = new DataSet();
        SqlCommand cmdObj = new SqlCommand();
        cmdObj.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlConnection conn = new SqlConnection(constr);
        cmdObj.CommandText = strSql;

        cmdObj.CommandType = CommandType.StoredProcedure;
        cmdObj.Connection = conn;
        try
        {
            objAdp = new SqlDataAdapter(cmdObj);
            objAdp.SelectCommand.CommandTimeout = 0;
            //foreach (DictionaryEntry dist in htable)
            //{
            //    objAdp.SelectCommand.Parameters.AddWithValue((string)dist.Key, dist.Value);
            //}
            ////objComBldr=new SqlCommandBuilder(objAdp);
            objAdp.Fill(dsResult);

        }
        catch
        {
            throw;
        }
        return dsResult;
    }

    public long exec_commandrec(string strSql)
    {
        long retvalue;
        try
        {
            objCon = new SqlConnection(constr3);
            if (objCon.State == ConnectionState.Open)
            {
                objCon.Close();
            }

            objCom = new SqlCommand(strSql, objCon);
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            objCon.Open();
            retvalue = Convert.ToUInt32(objCom.ExecuteScalar());
            objCom.Dispose();
        }
        catch
        {
            throw;
        }
        return retvalue;
    }
    public void exec_commandWithConn(string strSql, Hashtable htable, string strDBKey)
    {
        try
        {

            objCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[strDBKey].ToString());
            objCom = new SqlCommand(strSql, objCon);
            objCom.CommandTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            objCom.CommandType = CommandType.StoredProcedure;
            //objCon.ConnectionTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeOut"].ToString());
            foreach (DictionaryEntry dist in htable)
            {
                objCom.Parameters.AddWithValue((string)dist.Key, dist.Value);
            }
            objCon.Open();
            objCom.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            objCon.Close();
        }

    }
}

