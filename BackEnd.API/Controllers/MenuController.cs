using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BackEnd.Model;
using BackEnd.MW;

namespace BackEnd.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly BasicMW _basic;

        public MenuController(BasicMW basic)
        {
            _basic = basic;
        }

        /// <summary>
        /// 메뉴 목록 데이터 로드(메뉴용)
        /// </summary>
        [HttpGet("[action]/{user}")]
        public IEnumerable<Menu> MenuList(string user)
        {
            Log.INFO("[Menu.MenuList] 메뉴목록 조회 : {0}", user);

            JArray array = _basic.getMenuList(user.ToLower());
            List<Menu> Manager = new List<Menu>();
            List<Menu> Products = new List<Menu>();

            foreach (JObject json in array)
            {
                var parent = Manager.Where(e => e.menu_id.Equals(json["PARENT_ID"].ToString()));    //상위메뉴 코드가 같은 메뉴코드가 있으면 해당 메뉴정보 가져옴

                Menu info = new Menu()
                {
                    menu_id = json["MENU_ID"].ToString(),
                    menu_nm = json["MENU_NM"].ToString(),
                    parent_id = json["PARENT_ID"].ToString(),
                    path = json["PATH"].ToString(),
                    items = new List<Menu>()
                };

                if (parent.Count() > 0)
                {
                    //가져온 상위메뉴에 items에 메뉴 추가해줌
                    parent.ToList()[0].items.Add(info);
                }
                else
                {
                    //최상위메뉴
                    Products.Add(info);
                }
                //상위메뉴 체크용 메뉴관리 리스트
                Manager.Add(info);
            }
            Manager.Clear();
            Products[0].expanded = true;
            return Products;
        }

        [HttpGet("[action]")]
        public IEnumerable<Menu> BoardMenu()
        {
            Log.INFO("[Menu.MenuList] 메뉴목록 조회");

            JArray array = _basic.getMenu_Board();
            List<Menu> Manager = new List<Menu>();
            List<Menu> Products = new List<Menu>();

            foreach (JObject json in array)
            {
                var parent = Manager.Where(e => e.menu_id.Equals(json["PARENT_ID"].ToString()));    //상위메뉴 코드가 같은 메뉴코드가 있으면 해당 메뉴정보 가져옴

                Menu info = new Menu()
                {
                    menu_id = json["MENU_ID"].ToString(),
                    menu_nm = json["MENU_NM"].ToString(),
                    parent_id = json["PARENT_ID"].ToString(),
                    path = json["PATH"].ToString(),
                    items = new List<Menu>()
                };

                if (parent.Count() > 0)
                {
                    //가져온 상위메뉴에 items에 메뉴 추가해줌
                    parent.ToList()[0].items.Add(info);
                }
                else
                {
                    //최상위메뉴
                    Products.Add(info);
                }
                //상위메뉴 체크용 메뉴관리 리스트
                Manager.Add(info);
            }
            Manager.Clear();
            Products[0].expanded = true;
            return Products;
        }

        /// <summary>
        /// 메뉴 목록 데이터 로드(그리드용)
        /// </summary>
        [HttpGet("[action]/{auth}")]
        public IEnumerable<Menu> Get(string auth)
        {
            JArray array = _basic.getMenuList(auth.ToLower());
            List<Menu> menus = new List<Menu>();

            foreach (JObject json in array)
            {
                var info = JsonConvert.DeserializeObject<Menu>(JsonConvert.SerializeObject(json));
                menus.Add(info);
            }

            return menus;
        }

        /// <summary>
        /// 상위메뉴 목록 리스트
        /// </summary>
        [HttpGet("[action]")]
        public IEnumerable<Menu> GetParent()
        {
            JArray array = _basic.getMenuList("master");
            List<Menu> menus = new List<Menu>();

            //상위메뉴 값으로 필요한거기 때문에 메뉴목록만 있으면 됨
            foreach (JObject json in array)
            {
                Menu info = new Menu()
                {
                    menu_id = json["MENU_ID"].ToString(),
                    menu_nm = json["MENU_NM"].ToString(),
                };
                menus.Add(info);
            }
            return menus;
        }

        /// <summary>
        /// 메뉴 추가
        /// </summary>
        [HttpPost("[action]")]
        public object AddMenu(object data)
        {
            var menu = JsonConvert.DeserializeObject<menudata>(data.ToString());    //string값을 json을 통한 객체형으로 변환
            var types = typeof(menudata).GetProperties().ToList();  //해당 객체의 property 리스트 가져옴
            string param = string.Empty;
            menu.mode = "C";
            for (int i = 0; i < types.Count; i++)
            {
                //순서대로 값을 param에 추가
                param = string.IsNullOrEmpty(param) ? "'" + types[i].GetValue(menu) + "'" : param + ",'" + types[i].GetValue(menu) + "'";
            }
            
            var result = _basic.setMenu(param);
            return result;
        }

        /// <summary>
        /// 메뉴 수정
        /// </summary>
        [HttpPost("[action]")]
        public object UpdateMenu(object data)
        {
            var menu = JsonConvert.DeserializeObject<menudata>(data.ToString());    //string값을 json을 통한 객체형으로 변환
            var types = typeof(menudata).GetProperties().ToList();  //해당 객체의 property 리스트 가져옴
            string param = string.Empty;
            menu.mode = "U";
            for (int i = 0; i < types.Count; i++)
            {
                //순서대로 값을 param에 추가
                param = string.IsNullOrEmpty(param) ? "'" + types[i].GetValue(menu) + "'" : param + ",'" + types[i].GetValue(menu) + "'";
            }

            var result = _basic.setMenu(param);
            return result;
        }

        /// <summary>
        /// 메뉴 삭제
        /// </summary>
        [HttpPost("[action]")]
        public object DeleteMenu(object data)
        {
            var menu = JsonConvert.DeserializeObject<menudata>(data.ToString());    //string값을 json을 통한 객체형으로 변환
            var types = typeof(menudata).GetProperties().ToList();  //해당 객체의 property 리스트 가져옴
            string param = string.Empty;
            menu.mode = "D";
            for (int i = 0; i < types.Count; i++)
            {
                //순서대로 값을 param에 추가
                param = string.IsNullOrEmpty(param) ? "'" + types[i].GetValue(menu) + "'" : param + ",'" + types[i].GetValue(menu) + "'";
            }

            var result = _basic.setMenu(param);
            return result;
        }

        [HttpGet("[action]")]
        public string test()
        {
            return "메뉴호출";
        }
    }

    /// <summary>
    /// 저장용 객체
    /// </summary>
    public class menudata
    {
        public string menu_id { get; set; }
        public string menu_nm { get; set; }
        public string parent_id { get; set; }
        public string icon { get; set; }
        public string path { get; set; }
        public string auth { get; set; }
        public string useflag { get; set; }
        public string user_id { get; set; }
        public string mode { get; set; }
        public string msg_cd { get; set; }
        public string msg { get; set; }
    }
}