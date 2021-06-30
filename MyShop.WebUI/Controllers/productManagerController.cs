﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAcess.InMemory;
 

namespace MyShop.WebUI.Controllers
{
    public class productManagerController : Controller
    {
        //create an instance of product repository
        ProductRepository context;

        //constructor to initialize the repository
        public productManagerController()
        {
            context = new ProductRepository();        
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
            Product product = new Product();
            return View(product);
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
                return View(product);
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
                    return View(product)
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
               
            }

        }          
           
    }
}