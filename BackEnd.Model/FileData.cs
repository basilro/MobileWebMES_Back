using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Model
{
    public class FileData
    {
        public string file_Code { get; set; }
        public string file_NM { get; set; }
        public string size { get; set; }
        public string type { get; set; }
        public string location { get; set; }
        public string insrt_Dt { get; set; }
        public string insrt_User { get; set; }
    }
}
