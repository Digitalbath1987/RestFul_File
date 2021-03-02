using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRV.RESTFulAlo.Helpers
{
    public static class Controles
    {


        /// <summary>
        /// RESOLVER URL DINAMICA
        /// </summary>
        /// <param name="url"></param>
        /// <param name="UrlHelper"></param>
        /// <returns></returns>
        private static string ResolveURL(string url, UrlHelper UrlHelper)
        {

            string URL = UrlHelper.Content(url);
            return URL;
        }

        /// <summary>
        /// RESOLVER URL DINAMICA CSS
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="cssURL"></param>
        /// <returns></returns>
        public static MvcHtmlString CSSLink(this HtmlHelper helper, string cssURL)
        {
            var UrlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            return new MvcHtmlString(string.Format("<link href='{0}' rel='stylesheet' type='text/css'/>",
                                     ResolveURL(cssURL, UrlHelper)));
        }

        /// <summary>
        /// RESOLVER URL DINAMICA JS
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="jsURL"></param>
        /// <returns></returns>
        public static MvcHtmlString JSLink(this HtmlHelper helper, string jsURL)
        {

            var UrlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            return new MvcHtmlString(string.Format("<script src='{0}' type='text/javascript'></script>",
                                     ResolveURL(jsURL, UrlHelper)));
        }


        
        /// <summary>
        /// RESOLVER URL
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="URL"></param>
        /// <returns></returns>
        public static MvcHtmlString Href(this HtmlHelper helper, string URL)
        {
            var UrlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            return new MvcHtmlString(ResolveURL(URL, UrlHelper));
        }

    }
}