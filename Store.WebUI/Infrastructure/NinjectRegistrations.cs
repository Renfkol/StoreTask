using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Modules;
using Moq;
using Store.Domain.Entities;
using Store.Domain.Interfaces;
using Store.Domain.EF;
using Store.Domain.Concrete;
using System.Configuration;

namespace Store.WebUI.Infrastructure
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>{
            //    new Product { Name = "Товар1", Price = 1499 },
            //    new Product { Name = "Товар2", Price = 2299 },
            //    new Product { Name = "Товар3", Price = 899.4M }
            //});
            //Bind<IProductRepository>().ToConstant(mock.Object);

            Bind<IProductRepository>().To<ProductRepository>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                    .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);
            
        }
    }
}