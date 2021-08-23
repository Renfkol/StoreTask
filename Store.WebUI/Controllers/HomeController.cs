using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Domain.Interfaces;
using Store.Domain.Entities;
using Store.WebUI.Models;

namespace Store.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository repository;
        public int pageSize = 4;

        public HomeController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult List(string category,int page = 1)
        {
            ProductListViewModel viewModel = new ProductListViewModel
            {
                Products = repository.Products
                    .Where(x => category==null || x.Category == category)
                    .OrderBy(x => x.ProductId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),

                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?  
                        repository.Products.Count() :
                        repository.Products.Where(x=>x.Category==category).Count()
                },
                CurrentCategory = category
            };
            return View(viewModel);
        }

        public FileContentResult GetImage(int productId)
        {
            Product product = repository.Products.FirstOrDefault(p=>p.ProductId==productId);

            if(product!=null)
            {
                return File(product.ImageData, "image/png");
            }
            return null;
        }

    }
}