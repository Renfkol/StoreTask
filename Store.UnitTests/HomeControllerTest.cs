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
    public class HomeControllerTest
    {
        [TestMethod]
        public void CanPaginate()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>{
                new Product { ProductId=1, Name = "Product1", Price = 1499 },
                new Product { ProductId=2, Name = "Product2", Price = 2299 },
                new Product { ProductId=3, Name = "Product3", Price = 899.4M },
                new Product { ProductId=4, Name = "Product4", Price = 899.4M },
                new Product { ProductId=5, Name = "Product5", Price = 899.4M }

            });
            HomeController controller = new HomeController(mock.Object);
            controller.pageSize = 3;

            //Act
            ProductListViewModel result = (ProductListViewModel)controller.List(null,2).Model;

            //Assert
            List<Product> products = result.Products.ToList();
            Assert.IsTrue(products.Count == 2);
            Assert.AreEqual(products[0].Name, "Product4");
            Assert.AreEqual(products[1].Name, "Product5");
        }
        [TestMethod]
        public void CanSendPaginationViewModel()
        {
            //Arrange

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m=>m.Products).Returns(new List<Product>{
                new Product { ProductId=1, Name = "Product1", Price = 1499 },
                new Product { ProductId=2, Name = "Product2", Price = 2299 },
                new Product { ProductId=3, Name = "Product3", Price = 899.4M },
                new Product { ProductId=4, Name = "Product4", Price = 899.4M },
                new Product { ProductId=5, Name = "Product5", Price = 899.4M }

            });

            HomeController controller = new HomeController(mock.Object);
            controller.pageSize = 3;

            //Act
            ProductListViewModel result = (ProductListViewModel)controller.List(null,2).Model;

            //Assert
            PageInfo pageInfo = result.PageInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void CanFilterProduct()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>{
                new Product { ProductId=1, Name = "Product1", Category="Category1"},
                new Product { ProductId=2, Name = "Product2", Category="Category2"},
                new Product { ProductId=3, Name = "Product3", Category="Category1"},
                new Product { ProductId=4, Name = "Product4", Category="Category2"},
                new Product { ProductId=5, Name = "Product5", Category="Category3"}

            });

            HomeController controller = new HomeController(mock.Object);
            controller.pageSize = 3;

            List<Product> result = ((ProductListViewModel)controller.List("Category2", 1).Model).Products.ToList();

            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Product2" && result[0].Category == "Category2");
            Assert.IsTrue(result[1].Name == "Product4" && result[1].Category == "Category2");
        }

        [TestMethod]
        public void GenerateCategorySpecificCount()
        {

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>{
                new Product { ProductId=1, Name = "Product1", Category="Category1"},
                new Product { ProductId=2, Name = "Product2", Category="Category2"},
                new Product { ProductId=3, Name = "Product3", Category="Category1"},
                new Product { ProductId=4, Name = "Product4", Category="Category2"},
                new Product { ProductId=5, Name = "Product5", Category="Category3"}

            });

            HomeController controller = new HomeController(mock.Object);
            controller.pageSize = 3;


            int res1 = ((ProductListViewModel)controller.List("Category1").Model).PageInfo.TotalItems;
            int res2 = ((ProductListViewModel)controller.List("Category2").Model).PageInfo.TotalItems;
            int res3 = ((ProductListViewModel)controller.List("Category3").Model).PageInfo.TotalItems;
            int resAll = ((ProductListViewModel)controller.List(null).Model).PageInfo.TotalItems;


            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }

        [TestMethod]
        public void CanRetrieveImageData()
        {

            Product product = new Product
            {
                ProductId = 2,
                Name = "Product2",
                ImageData = new byte[] { },
            };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Product1" },
                product,
                new Product { ProductId = 3, Name = "Product3" }
            }.AsQueryable());
            HomeController controller = new HomeController(mock.Object);


            ActionResult result = controller.GetImage(2);


            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
        }

        [TestMethod]
        public void CannotRetrieveImageDataForInvalidID()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product {ProductId = 1, Name = "Product1"},
                new Product {ProductId = 2, Name = "Product2"}
            }.AsQueryable());
            HomeController controller = new HomeController(mock.Object);

            ActionResult result = controller.GetImage(10);

            Assert.IsNull(result);
        }
    }
}
