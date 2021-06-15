using BackEnd.IDA;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.MSSQL
{
    public class FileDA : IFile
    {
        public object AddFile(string param)
        {
            return Utils.getQuery("setFile", param);
        }

        public object GetImage(string file_code)
        {
            return Utils.getQuery("getImage", file_code);
        }
    }
}
