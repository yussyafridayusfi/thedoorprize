using MPMSecurityFilter.Mvc.Filters;
using System.Web;
using System.Web.Mvc;

namespace MPMWEBDOORPRIZE
{
		public class FilterConfig
		{
				public static void RegisterGlobalFilters(GlobalFilterCollection filters)
				{
						filters.Add(new HandleErrorAttribute());
						filters.Add(new MPMAuthorizeAttribute("mpmuploadnosinclaimahm"));
						filters.Add(new MPMActionFilterAttribute());
				}
		}
}
