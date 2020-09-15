using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.sql
{
    public class MySqlHelpDao
    {
        
        public static string ConnctionString = "server=127.0.0.1;port=3306;user=root;password=1995107;database=qqfriend;Allow User Variables=True";
        public static int ExecuteNonQuery(string sql,params MySqlParameter[] parameters)
        {
            int val = 0;
            MySqlTransaction sqlTransaction = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConnctionString))
                {
                    con.Open();
                    //开启事务
                    sqlTransaction = con.BeginTransaction();
                    MySqlCommand mySqlCommand = new MySqlCommand(sql,con);
                    foreach(var item in parameters)
                    {
                        mySqlCommand.Parameters.Add(item);
                    }
                    val = mySqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    con.Close();
                   
                }
                return val;
            }
            catch(Exception e)
            {
                //事务回滚
                sqlTransaction.Rollback();
                //事务提交
                sqlTransaction.Commit();

            }
            return val;

        }
        public static DataSet ExecuteDataSet(string sql)
        {
            DataSet dataSet = new DataSet();
            using(MySqlConnection con = new MySqlConnection(ConnctionString))
            {
                con.Open();
                MySqlCommand mySqlCommand = new MySqlCommand(sql, con);
                using(MySqlDataAdapter da = new MySqlDataAdapter(sql,con))
                {
                    da.SelectCommand = mySqlCommand;
                    da.Fill(dataSet, "table");
                    con.Close();
                }

            }
            return dataSet;
        }
    }
}
