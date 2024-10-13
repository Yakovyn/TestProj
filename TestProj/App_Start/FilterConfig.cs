using System.Web;
using System.Web.Mvc;
using TestProj.Filters;

namespace TestProj
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new UserActionFilter());
        }
    }
}
