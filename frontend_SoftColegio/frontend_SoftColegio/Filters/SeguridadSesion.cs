using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using frontendUtil;


namespace frontend_SoftColegio.Filters
{
    public class SecuritySession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool bValidar = UtlAuditoria.ValidarSession();
            string Url = HttpContext.Current.Request.Url.AbsolutePath;
            string res = Url.Remove(0, 1);
            //string sMenu = UtlAuditoria.ObtenerMenu();

            if (bValidar)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "principal", action = "timeout" }));

            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class SecuritySessionSales : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool bValidar = UtlAuditoria.ValidarSession();
            string Url = HttpContext.Current.Request.Url.AbsolutePath;
            string res = Url.Remove(0, 1);
            //string sMenu = UtlAuditoria.ObtenerMenu();

            if (bValidar)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "vendedor", action = "Index" }));

            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class SeguridadSessionAjax : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool bValidar = UtlAuditoria.ValidarSession();

            if (bValidar)
            {

                filterContext.Result = new JsonResult
                {
                    Data = new { iTipoResultado = -5, message = UtlConstantes.msgErrorSesion },
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };

            }
            base.OnActionExecuting(filterContext);
        }
    }
}