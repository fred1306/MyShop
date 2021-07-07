using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAcess.InMemory;
 

namespace MyShop.WebUI.Controllers
{
    public class productManagerController : Controller
    {
        //create an instance of product repository
        //  ProductRepository context;
        //  ProductCategoryRepository productCategories;

        //InMemoryRepository<Product> context;
        //InMemoryRepository<ProductCategory> productCategories;

        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
              

        //constructor to initialize the repository
        // modify the constructor to accept two parameters inject two classes
        public productManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            //context = new InMemoryRepository<Product>();
            //productCategories = new InMemoryRepository<ProductCategory>();

            context = productContext;
            productCategories = productCategoryContext;

        }

        // GET: productManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();

            //send the products back to the view
            return View(products);
        }

        //to fill the product details
        public ActionResult Create()
        {
           // Product product = new Product();

            //we want to create a product with a list of categories
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }

        //have product details posted in
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            else
            {
                //insert the product into the collection
                context.Insert(product);

                //save the changes back
                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Edit(string Id)
        {

            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }

            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();
                return View(viewModel);
            }

        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product productToEdit = context.Find(Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }

            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Price = product.Price;

                context.Commit(); // commit changes

                return RedirectToAction("Index");
               
            }

        }          


        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);

            if (productToDelete == null )
            {
                return HttpNotFound();
            }

            else
            {
                return View(productToDelete);
            }


        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }

        }
           
    } // end of class
}