using MPMSecurityFilter.Mvc.Filters;
using System.Web;
using System.Web.Mvc;

namespace MPMWEB
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new MPMFreeAuthorizeAttribute("mpmwmspdtbastlku"));
            filters.Add(new MPMActionFilterAttribute());
        }
    }
}
