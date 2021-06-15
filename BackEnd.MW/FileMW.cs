using BackEnd.IDA;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.MW
{
    public class FileMW
    {
        private IFile _file;
        public FileMW(IFile file)
        {
            _file = file;
        }
        public object AddFile(string param) => _file.AddFile(param);

        public object GetImage(string file_code) => _file.GetImage(file_code);
    }
}
