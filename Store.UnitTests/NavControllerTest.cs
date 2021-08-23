using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;
using Moq;
using System.Linq;
using Store.Domain.Interfaces;
using System.Collections.Generic;
using Store.Domain.Entities;
using Store.WebUI.Controllers;
using Store.WebUI.Models;
using Store.WebUI.HtmlHelpers;

namespace Store.UnitTests
{
    [TestClass]
    public class NavControllerTest
    {
        [TestMethod]
        public void CanCreateCategories()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>{
                new Product { ProductId=1, Name = "Product1", Category="Category1"},
                new Product { ProductId=2, Name = "Product2", Category="Category2"},
                new Product { ProductId=3, Name = "Product3", Category="Category1"},
                new Product { ProductId=4, Name = "Product4", Category="Category2"},
                new Product { ProductId=5, Name = "Product5", Category="Category3"}

            });

            NavController controller = new NavController(mock.Object);

            List<string> result = ((IEnumerable<string>)controller.Menu().Model).ToList();

            Assert.AreEqual(result.Count(), 3);
        }

       
    }
}
