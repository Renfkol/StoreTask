using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Domain.Interfaces;
using Store.Domain.Entities;

namespace Store.WebUI.Controllers
{
    //[Authorize (Roles ="admin")]
    public class AdminController : Controller
    {
        IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productId)
        {
            Product product = repository.Products
                .FirstOrDefault(g => g.ProductId == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                repository.SaveProduct(product);
                TempData["message"] = string.Format("Изменения в \"{0}\" были сохранены", product.Name);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ViewResult Create()
        {
            return View("Edit",new Product());
        }

        [HttpPost]
        public ActionResult Delete(int ProductId)
        {
            Product deletedProduct = repository.DeleteProduct(ProductId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("Игра \"{0}\" была удалена", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }

    }
}