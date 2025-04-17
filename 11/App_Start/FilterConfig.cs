using log4net;
using log4net.Config;
using System.Web;
using System.Web.Mvc;

namespace _11
{
    public class FilterConfig
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(FilterConfig));
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            XmlConfigurator.Configure(); // Kích hoạt log4net

            log.Info("Log4Net đã được khởi tạo thành công trong App_Start!");
        }
    }
}
