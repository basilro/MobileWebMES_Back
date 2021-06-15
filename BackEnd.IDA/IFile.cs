using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.IDA
{
    public interface IFile
    {
        object AddFile(string param);

        object GetImage(string file_code);
    }
}
