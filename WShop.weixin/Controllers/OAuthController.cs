using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.Helpers;
using WShop.EFModel;
using WShop.IService;
using WShop.weixin.Models;

namespace WShop.weixin.Controllers
{
    public class OAuthController : Controller
    {
        public ICustomerService CustomerService { get; set; }
        // GET: OAuth
        private string _appId = "wx28dba51c0843fd27";
        private string _appsecret = "130a82e7e42d6bf474b5605943beece8";

        private string _domain = "http://wx.priest.ink";
        // GET: OAuth
        public ActionResult Login(string requestUrl)
        {
            //生成回调url
            var redirectUrl = $"{_domain}{Url.Action("CallBack", new { requestUrl })}";
            //生成验证码
            var state = "wx" + DateTime.Now.Millisecond;
            Session["state"] = state;
            //生成微信授权码
            var url = OAuthApi.GetAuthorizeUrl(_appId, redirectUrl, state, OAuthScope.snsapi_base);
            return Redirect(url);
        }

        public ActionResult CallBack(string code, string state, string requestUrl)
        {
            //判断验证码
            if (state != (string)Session["state"])
            {
                return Content("非法访问！");
            }
            //判断code是否有数据
            if (string.IsNullOrEmpty(code))
            {
                return Content("授权失败");
            }
            //通过code获取令牌
            var OAuthAccessToken = OAuthApi.GetAccessToken(_appId, _appsecret, code);
            //判断令牌获取成功与否
            if (OAuthAccessToken.errcode != ReturnCode.请求成功)
            {
                return Content("授权失败");
            }
            //获取令牌成功，传出令牌，登录成功
            Session["AccessToken"] = OAuthAccessToken;
            //尝试获取用户信息，若获取到了，则说明令牌有效
            //不管令牌是否有权限，OpenID都是一样
            try
            {
                var userinfo = OAuthApi.GetUserInfo(OAuthAccessToken.access_token, OAuthAccessToken.openid);
                //如果不为空，则获取成功
                Session["userinfo"] = userinfo;
                addCus(userinfo);
                //重新定向到用户请求
                return Redirect(requestUrl);
            }
            catch (Exception)
            {
                var redirectUrl = $"{_domain}{Url.Action("CallBack", new { requestUrl })}";
                var url = OAuthApi.GetAuthorizeUrl(_appId, redirectUrl, state, OAuthScope.snsapi_userinfo);

                return Redirect(url);
            }
        }
        public ActionResult JsSdkconfig()
        {
            var url = _domain + Request.RawUrl;
            var config = JSSDKHelper.GetJsSdkUiPackage(_appId, _appsecret, url);
            return PartialView(config);
        }
        public void addCus(OAuthUserInfo cus)
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            Customer cust = new Customer();
            cust.OpenId = cus.openid;
            cust.UImg = cus.headimgurl;
            cust.Name = cus.nickname;
            cust.CreateTime = DateTime.Now;
            if (CustomerService.GetCount(n => n.OpenId == cus.openid) < 1)
            {
                CustomerService.Add(cust);
            }
            else if (CustomerService.GetCount(n => n.UImg == cus.headimgurl && n.Name == cus.nickname) < 1)
            {
                cust.ID = CustomerService.GetEntities(c => c.OpenId == cust.OpenId).First().ID;
                CustomerService.Add(cust);
            }
            Session["openid"] = cus.openid;
        }

    }
}