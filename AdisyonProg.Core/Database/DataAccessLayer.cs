using AdisyonProg.Core.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdisyonProg.Core.Database
{
    public class DataAccessLayer : Globalislemler
    {
        public SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        int ReturnValue;
        string ReturnString;
        decimal ReturnDecimal;
        DateTime ReturnDateTime;

        public DataAccessLayer()
        {
            ReturnDateTime = new DateTime();
            string path = Path.GetFullPath(Environment.CurrentDirectory);
            string databaseName = "AdisyonStok.mdf";
            con = new SqlConnection("data source = (local)\\SQLEXPRESS; initial catalog= AdisyonStok; user id = sa; password = 3220611s;");
            //con = new SqlConnection("data source = tsfteam.club\\MSSQLSERVER2012; initial catalog= adisyonStok; user id = adisyon; password = _Rg7mf29;");
            //con = new SqlConnection(@"Data Source =(localdb)\v12.0;AttachDbFilename="+path+@"\"+databaseName+ ";Integrated Security=True;");
        }

        public int Calistir(SqlCommand cmd)
        {
            cmd.Connection = con;
            con.Open();
            TryCatchKullan(() =>
            {
                ReturnValue = cmd.ExecuteNonQuery();
            });
            con.Close();
            return ReturnValue;
        }

        public SqlDataReader VeriGetir(SqlCommand cmd)
        {
            cmd.Connection = con;
            con.Open();
            TryCatchKullan(() =>
            {
                reader = cmd.ExecuteReader();
            });
            return reader;
        }

        public int CalistirINT(SqlCommand cmd)
        {
            cmd.Connection = con;
            con.Open();
            TryCatchKullan(() =>
            {
                ReturnValue = (int)cmd.ExecuteScalar();
            });
            con.Close();
            return ReturnValue;
        }

        public decimal CalistirDecimal(SqlCommand cmd)
        {
            cmd.Connection = con;
            con.Open();
            TryCatchKullan(() =>
            {
                ReturnDecimal = (decimal)cmd.ExecuteScalar();
            });
            con.Close();
            return ReturnDecimal;
        }

        public string CalistirString(SqlCommand cmd)
        {
            cmd.Connection = con;
            con.Open();
            TryCatchKullan(() =>
            {
                ReturnString = (string)cmd.ExecuteScalar();
            });
            con.Close();
            return ReturnString;
        }

        public DateTime CalistirDateTime(SqlCommand cmd)
        {
            cmd.Connection = con;
            con.Open();
            TryCatchKullan(() =>
            {
                ReturnDateTime = (DateTime)cmd.ExecuteScalar();
            });
            con.Close();
            return ReturnDateTime;
        }

    }
}
