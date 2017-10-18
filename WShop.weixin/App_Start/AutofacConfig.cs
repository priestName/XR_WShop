using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using WShop.weixin;

namespace WShop
{
    public class AutofacConfig
    {
        public static void RegisterDenpendency()
        {
            //创建有个容器配置对象
            var builder = new ContainerBuilder();
            //注册当前MVC应用里的所有控制器（自动注册，只需要给他提供要注册的程序集，在这里我们注册是自己）
            //Assembly.GetExecutingAssembly()就是获取当前运行中的程序的所有的类
            //PropertiesAutowired表示使用属性的方式进行依赖注入
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            //加载其他程序集的代码
            var service = Assembly.Load("WShop.Service");
            var repositoty = Assembly.Load("WShop.Repositoty");

            var iservice = Assembly.Load("WShop.IService");
            var irepositoty = Assembly.Load("WShop.IRepositoty");

            //注册接口的实现方（即其他程序集的类）
            builder.RegisterAssemblyTypes(service,iservice).Where(r => r.Name.EndsWith("Service")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(repositoty, irepositoty).Where(r => r.Name.EndsWith("Repositoty")).AsImplementedInterfaces();

            //创建Loc容器对象
            var container = builder.Build();

            //替换掉MVC内置的控制器实例化对象（转移控制器的实例化的权限）
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}