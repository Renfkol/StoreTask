using Store.Domain.EF;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Store.Domain.EF
{
    class ProductDbInitializer : DropCreateDatabaseAlways<ProductDbContext>
    {
        protected override void Seed(ProductDbContext context)
        {

            context.Products.Add(new Product
            {
                Name = "Товар первый",
                Description = "Описание для товара",
                Category = "Обычные товары",
                Price = 100
            });

            context.Products.Add(new Product
            {
                Name = "Товар второй",
                Description = "Описание для товара",
                Category = "Обычные товары",
                Price = 200
            });
            context.Products.Add(new Product
            {
                Name = "Товар третий",
                Description = "Описание для необычного товара",
                Category = "Необычные товары",
                Price = 300
            });
            context.Products.Add(new Product
            {
                Name = "Товар четвертый",
                Description = "Описание для редкого товара",
                Category = "Редкие товары",
                Price = 400
            });
            context.Products.Add(new Product
            {
                Name = "Товар пятый",
                Description = "Описание для необычного товара",
                Category = "Необычные товары",
                Price = 500
            });
            context.Products.Add(new Product
            {
                Name = "Товар шестой",
                Description = "Описание для необычного товара",
                Category = "Необычные товары",
                Price = 600
            });
            context.Products.Add(new Product
            {
                Name = "Товар седьмой",
                Description = "Описание для товара",
                Category = "Обычные товары",
                Price = 700
            });
            context.Products.Add(new Product
            {
                Name = "Товар восьмой",
                Description = "Описание для товара",
                Category = "Обычные товары",
                Price = 800
            });
            context.Products.Add(new Product
            {
                Name = "Товар девятый",
                Description = "Описание для необычного товара",
                Category = "Необычные товары",
                Price = 900
            });
            context.Products.Add(new Product
            {
                Name = "Товар десятый",
                Description = "Описание для товара",
                Category = "Обычные товары",
                Price = 1000
            });
            context.Products.Add(new Product
            {
                Name = "Товар одиннадцатый",
                Description = "Описание для товара",
                Category = "Обычные товары",
                Price = 1100
            });
            context.Products.Add(new Product
            {
                Name = "Товар двенадцатый",
                Description = "Описание для товара",
                Category = "Обычные товары",
                Price = 1200
            });
            context.Products.Add(new Product
            {
                Name = "Товар тринадцатый",
                Description = "Описание для необычного товара",
                Category = "Необычные товары",
                Price = 1300
            });
            context.Products.Add(new Product
            {
                Name = "Товар четырнадцатый",
                Description = "Описание для товара",
                Category = "Обычные товары",
                Price = 1400
            });
            context.Products.Add(new Product
            {
                Name = "Товар пятнадцатый",
                Description = "Описание для редкого товара",
                Category = "Редкие товары",
                Price = 1500
            });

            base.Seed(context);
        }
    }
}