using System.Web;
using System.Web.Mvc;

namespace Tiende_De_Video_Juegos
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
