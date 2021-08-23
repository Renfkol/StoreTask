using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using Store.Domain.Entities;
using Moq;
using Store.Domain.Interfaces;
using Store.WebUI.Controllers;
using System.Web.Mvc;

namespace Store.UnitTests
{
    [TestClass]
    public class AdminTest
    {
        [TestMethod]
        public void IndexContainsAllProducts()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Product1"},
                new Product { ProductId = 2, Name = "Product2"},
                new Product { ProductId = 3, Name = "Product3"},
                new Product { ProductId = 4, Name = "Product4"},
                new Product { ProductId = 5, Name = "Product5"}
            });

            AdminController controller = new AdminController(mock.Object);


            List<Product> result = ((IEnumerable<Product>)controller.Index().
                ViewData.Model).ToList();


            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Product1", result[0].Name);
            Assert.AreEqual("Product2", result[1].Name);
            Assert.AreEqual("Product3", result[2].Name);
        }

        [TestMethod]
        public void CanEditProduct()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Product1"},
                new Product { ProductId = 2, Name = "Product2"},
                new Product { ProductId = 3, Name = "Product3"},
                new Product { ProductId = 4, Name = "Product4"},
                new Product { ProductId = 5, Name = "Product5"}
            });


            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            Product product1 = controller.Edit(1).ViewData.Model as Product;
            Product product2 = controller.Edit(2).ViewData.Model as Product;
            Product product3 = controller.Edit(3).ViewData.Model as Product;

            // Assert
            Assert.AreEqual(1, product1.ProductId);
            Assert.AreEqual(2, product2.ProductId);
            Assert.AreEqual(3, product3.ProductId);
        }

        [TestMethod]
        public void CannotEditNonexistentProduct()
        {

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Product1"},
                new Product { ProductId = 2, Name = "Product2"},
                new Product { ProductId = 3, Name = "Product3"},
                new Product { ProductId = 4, Name = "Product4"},
                new Product { ProductId = 5, Name = "Product5"}
            });

            AdminController controller = new AdminController(mock.Object);

            Product result = controller.Edit(6).ViewData.Model as Product;

            Assert.IsNull(result);

        }
        [TestMethod]
        public void CanDeleteProduct()
        {
            Product product = new Product { ProductId = 3, Name = "Product3" };

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Product1"},
                new Product { ProductId = 2, Name = "Product2"},
                new Product { ProductId = 3, Name = "Product3"},
                new Product { ProductId = 4, Name = "Product4"},
                new Product { ProductId = 5, Name = "Product5"}
            });

            AdminController controller = new AdminController(mock.Object);
            controller.Delete(product.ProductId);

            mock.Verify(m => m.DeleteProduct(product.ProductId));
        }

        
    }
}
