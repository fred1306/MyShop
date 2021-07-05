﻿using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAcess.InMemory
{
  public  class InMemoryRepository<T> where T: BaseEntity  //becaue BaseEntity has Id, so whenever refer to iD he generic class knows what that is
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;



        public InMemoryRepository()
        {

            className = typeof(T).Name;
            items = cache[className] as List<T>;

            if (items == null)
            {
                items = new List<T>();
            }
        } // end of constructor

        //generic method to store items in memory
        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {

            T tToUpdate = items.Find(i => i.Id == t.Id);

            if (tToUpdate != null)
            {
                tToUpdate = t;

            }

            else
            {
                throw new Exception(className + "Not Found!");
            }
        }

        public T Find(T t)
        {
            T t = items.Find(i => i.Id == t.Id);

            if (t != null)
            {
                return t;
            }

            else
            {
                throw new Exception(className + "Not Found!");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(String Id)
        {
            T tToDelete = items.Find(i => i.Id == Id);

            if (tToDelete != null)
            {
                items.Remove(tToDelete);

            }

            else
            {
                throw new Exception(className + "Not Found!");
            }

        }


    } // end of class


       
    } // end of namespace

