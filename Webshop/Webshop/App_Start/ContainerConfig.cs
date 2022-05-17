using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Webshop.Service;
using Webshop.Service.Common;
using Webshop.Repository;
using Webshop.Repository.Common;
using System.Web.Http;
using Autofac.Integration.WebApi;

namespace Webshop.App_Start
{
	public class ContainerConfig
	{
		public static void RegisterDependencies()
		{
			var builder = new ContainerBuilder();

			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			builder.RegisterType<ProductOrderService>().As<IProductOrderService>();
			builder.RegisterType<ProductOrderRepository>().As<IProductOrderRepository>();

			builder.RegisterType<OrderService>().As<IOrderService>();
			builder.RegisterType<OrderRepository>().As<IOrderRepository>();


			builder.RegisterType<ProductRepository>().As<IProductRepository>();
			builder.RegisterType<ProductService>().As<IProductService>();
			builder.RegisterType<CustomerService>().As<ICustomerService>();
			builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();


			var container = builder.Build();
			GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);
		}
	}
}