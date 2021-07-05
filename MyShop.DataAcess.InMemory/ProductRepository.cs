using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAcess.InMemory
{
    public class ProductRepository
    {

        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {

            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["products"] = products; //save the list of products into cache

        }

        public void Insert(Product p)
        {
            products.Add(p); //add product to a products list

        }

        public void Update(Product product)
        {

            Product productsToUpdate = products.Find(p => p.Id == product.Id);

            if (productsToUpdate != null)
            {
                productsToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }

        public Product Find(string Id)
        {

            Product product = products.Find(p => p.Id == Id);

            if (product != null)
            {
                return product; //if we find the product then we retuen it
            }
            else
            {
                throw new Exception("Product not found!");
            }


        }



        //return a list that can be queried

        public IQueryable<Product> Collection()
        {

            return products.AsQueryable(); //return a queryable list of products

        }

        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found!");
            }
            

        } // end of Delete
    }
}
