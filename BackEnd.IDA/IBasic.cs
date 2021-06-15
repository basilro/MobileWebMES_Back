using Newtonsoft.Json.Linq;

namespace BackEnd.IDA
{
    public interface IBasic
    {
        JArray getMenuList(string authj);

        JObject Login(string userId);

        object setMenu(string data);

        JArray getMenu_Board();
    }
}
