using BusinessObject.Data;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProductDAO
    {
        public static List<Category> GetCategories()
        {
            var list = new List<Category>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    list = context.Categories.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public static List<Product> GetProducts()
        {
            var list = new List<Product>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    list = context.Products.Include(x => x.Category).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static Product FindProductById(int propId)
        {
            Product p = new();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    p = context.Products.Include(x => x.Category).SingleOrDefault(x => x.ProductId == propId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return p;
        }

        public static void SaveProduct(Product product)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateProduct(Product product)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Entry(product).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteProduct(Product product)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var p = context.Products.SingleOrDefault(
                        c => c.ProductId == product.ProductId);
                    context.Products.Remove(p);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
