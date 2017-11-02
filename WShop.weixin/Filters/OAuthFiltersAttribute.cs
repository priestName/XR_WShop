using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WShop.weixin.Filters
{
    public class OAuthFiltersAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //判断是否登录授权
            if (filterContext.HttpContext.Session["AccessToken"] != null) return;
            //开始授权
            //用户请求访问的地址
            var requestUrl = filterContext.HttpContext.Request.RawUrl;
            //未登录则挑战到登录页
            var urlHelper = new UrlHelper(filterContext.RequestContext);
            filterContext.Result = new RedirectResult(urlHelper.Action("Login", "OAuth", new { requestUrl }));
        }
    }
}