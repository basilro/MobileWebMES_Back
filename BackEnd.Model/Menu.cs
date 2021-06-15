using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Model
{
    public class Menu
    {
        public string menu_id { get; set; }
        public string menu_nm { get; set; }
        public string parent_id { get; set; }
        public string icon { get; set; }
        public string path { get; set; }
        public string auth { get; set; }
        public string useflag { get; set; }
        public string insrt_dt { get; set; }
        public string insrt_user { get; set; }
        public string updt_dt { get; set; }
        public string updt_user { get; set; }
        public bool expanded { get; set; }
        public List<Menu> items { get; set; }
    }
}
