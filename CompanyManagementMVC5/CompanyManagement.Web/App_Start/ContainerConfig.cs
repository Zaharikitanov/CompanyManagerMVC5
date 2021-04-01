using Autofac;
using Autofac.Integration.Mvc;
using CompanyManagement.Data;
using CompanyManagement.Data.Services;
using CompanyManagement.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompanyManagement.Web
{
    public class ContainerConfig
    {
        internal static void RegisterContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<CompanyManagerContext>().InstancePerRequest();
            builder.RegisterType<CompanyService>().As<ICompanyService>().InstancePerRequest();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerRequest();
            builder.RegisterType<OfficeService>().As<IOfficeService>().InstancePerRequest();

            var container = builder.Build();

            DependencyResolver.SetResolver( new AutofacDependencyResolver(container));
        } 
    }
}