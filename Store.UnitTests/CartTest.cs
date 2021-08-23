using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using Store.Domain.Entities;
using Moq;
using Store.Domain.Interfaces;
using Store.WebUI.Controllers;
using System.Web.Mvc;
using System.Web;
using Store.WebUI.Models;

namespace Store.UnitTests
{
    [TestClass]
    public class CartTest
    {
        [TestClass]
        public class CartTests
        {
            [TestMethod]
            public void CanAddNewLines()
            {
                Product product1 = new Product { ProductId = 1, Name = "Product1" };
                Product product2 = new Product { ProductId = 2, Name = "Product2" };

                Cart cart = new Cart();

                cart.AddItem(product1, 1);
                cart.AddItem(product2, 1);
                List<CartLine> results = cart.Lines.ToList();

                Assert.AreEqual(results.Count(), 2);
                Assert.AreEqual(results[0].Product, product1);
                Assert.AreEqual(results[1].Product, product2);
            }

            [TestMethod]
            public void Can_Add_Quantity_For_Existing_Lines()
            {
                Product product1 = new Product { ProductId = 1, Name = "Product1" };
                Product product2 = new Product { ProductId = 2, Name = "Product2" };

                Cart cart = new Cart();

                cart.AddItem(product1, 1);
                cart.AddItem(product2, 1);
                cart.AddItem(product1, 5);
                List<CartLine> results = cart.Lines.OrderBy(c => c.Product.ProductId).ToList();

                Assert.AreEqual(results.Count(), 2);
                Assert.AreEqual(results[0].Quantity, 6);    
                Assert.AreEqual(results[1].Quantity, 1);
            }


            [TestMethod]
            public void CanRemoveLine()
            {
                Product product1 = new Product { ProductId = 1, Name = "Product1" };
                Product product2 = new Product { ProductId = 2, Name = "Product2" };
                Product product3 = new Product { ProductId = 3, Name = "Product3" };

                Cart cart = new Cart();

                cart.AddItem(product1, 1);
                cart.AddItem(product2, 4);
                cart.AddItem(product3, 2);
                cart.AddItem(product2, 1);

                cart.RemoveLine(product2);

                Assert.AreEqual(cart.Lines.Where(c => c.Product == product2).Count(), 0);
                Assert.AreEqual(cart.Lines.Count(), 2);
            }

            [TestMethod]
            public void CanClearContents()
            {
                Product product1 = new Product { ProductId = 1, Name = "Product1"};
                Product product2 = new Product { ProductId = 2, Name = "Product2"};

                Cart cart = new Cart();

                cart.AddItem(product1, 1);
                cart.AddItem(product2, 1);
                cart.AddItem(product1, 5);
                cart.Clear();

                Assert.AreEqual(cart.Lines.Count(), 0);
            }


            [TestMethod]
            public void CanAddToCart()
            {
                Mock<IProductRepository> mock = new Mock<IProductRepository>();
                mock.Setup(m => m.Products).Returns(new List<Product> {
                    new Product {ProductId = 1, Name = "Product1", Category = "Category1"},
                }.AsQueryable());

                Cart cart = new Cart();
                CartController controller = new CartController(mock.Object,null);
 
                controller.AddToCart(cart, 1, null);

                Assert.AreEqual(cart.Lines.Count(), 1);
                Assert.AreEqual(cart.Lines.ToList()[0].Product.ProductId, 1);
            }


            [TestMethod]
            public void AddingProductToCartGoesToCartScreen()
            {
                Mock<IProductRepository> mock = new Mock<IProductRepository>();
                mock.Setup(m => m.Products).Returns(new List<Product> {
                    new Product {ProductId = 1, Name = "Product1", Category = "Category1"},
                }.AsQueryable());

                Cart cart = new Cart();

                CartController controller = new CartController(mock.Object,null);

                RedirectToRouteResult result = controller.AddToCart(cart, 2, "myUrl");

                Assert.AreEqual(result.RouteValues["action"], "Index");
                Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
            }

            [TestMethod]
            public void CannotCheckoutEmptyCart()
            {
                Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
                Cart cart = new Cart();
                ShippingDetails shippingDetails = new ShippingDetails();
                CartController controller = new CartController(null, mock.Object);

                ViewResult result = controller.Checkout(cart, shippingDetails);
                mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                    Times.Never());


                Assert.AreEqual("", result.ViewName);
                Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
            }

            [TestMethod]
            public void CannotCheckoutInvalidShippingDetails()
            {
                Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
                Cart cart = new Cart();
                cart.AddItem(new Product(), 1);
                CartController controller = new CartController(null, mock.Object);
                controller.ModelState.AddModelError("error", "error");


                ViewResult result = controller.Checkout(cart, new ShippingDetails());

                mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                    Times.Never());
                Assert.AreEqual("", result.ViewName);
                Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
            }

        }

    }
}
