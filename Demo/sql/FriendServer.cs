using Demo.Items;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.sql
{
    public class FriendServer
    {
        public int Add(Friend friend)
        {
            object obj = null;
            try
            {


                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into friend(");
                strSql.Append("Head,Nickname)");
                strSql.Append(" value(");
                strSql.Append("@Head,@Nickname)");
                MySqlParameter[] parameters =
                {
                new MySqlParameter("@Head",MySqlDbType.LongBlob),
                new MySqlParameter("@Nickname",MySqlDbType.VarChar)

            };

                MemoryStream stream = new MemoryStream();

                friend.Head.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] bt = stream.ToArray();
                parameters[0].Value = bt;
                parameters[1].Value = friend.Nickname;
                
                obj = MySqlHelpDao.ExecuteNonQuery(strSql.ToString(),parameters);
            }
            catch (Exception e)
            {

            }
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }


        }
        public DataTable GetTable()
        {
            DataTable table = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from friend");
                table = MySqlHelpDao.ExecuteDataSet(strSql.ToString()).Tables[0];


            }
            catch (Exception E)
            {

            }
            return table;
        }
    }
}
