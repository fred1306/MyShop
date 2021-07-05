using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;


namespace MyShop.DataAcess.InMemory
{
   public class ProductCategoryRepository
    {

        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {

            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productCategories"] = productCategories; //save the list of products into cache

        }

        public void Insert(ProductCategory p)
        {
            productCategories.Add(p); //add product to a products list

        }

        public void Update(ProductCategory productCategory)
        {

            ProductCategory productsCategoryToUpdate = productCategories.Find(p => p.Id == productCategory.Id);

            if (productsCategoryToUpdate != null)
            {
                productsCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Category not found!");
            }
        }

        public ProductCategory Find(string Id)
        {

            ProductCategory productCategory = productCategories.Find(p => p.Id == Id);

            if (productCategories != null)
            {
                return productCategory; //if we find the product then we retuen it
            }
            else
            {
                throw new Exception("Product Category not found!");
            }


        }



        //return a list that can be queried

        public IQueryable<ProductCategory> Collection()
        {

            return productCategories.AsQueryable(); //return a queryable list of products

        }

        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == Id);

            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product Category not found!");
            }


        } // end of Delete

    }
}
