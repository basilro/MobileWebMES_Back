using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace BackEnd.MSSQL
{
    internal class Loader
    {
        private static SqlConnection _sqlCon;

        internal static string _constring;

        internal static void DBSet(IConfiguration configuration)
        {
#if DEBUG
            //_constring = configuration.GetConnectionString("DBConn2");   //시스너내부
            _constring = configuration.GetConnectionString("DBConn");  //디앤티 테스트
#else
            _constring = configuration.GetConnectionString("DBConn"); //디앤티
            //_constring = configuration.GetConnectionString("DBConn_TEST");  //디앤티 테스트
#endif
        }

        //
        internal static SqlConnection SqlCon
        {
            get
            {
                if (_sqlCon == null)
                {
                    _sqlCon = new SqlConnection(_constring);//"Data Source=192.168.0.211;Initial Catalog=SYSMES;User ID=sa;Password=Sysner0523");
                }
                return _sqlCon;
            }
        }
    }
}
