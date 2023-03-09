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
    public class OrderDAO
    {
        public static List<OrderDetail> GetOrderDetails()
        {
            var list = new List<OrderDetail>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    list = context.OrderDetails.Include(x => x.Order).Include(x => x.Product).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static List<OrderDetail> GetOrderDetailsByUserId(string userId)
        {
            var list = new List<OrderDetail>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    list = context.OrderDetails.Include(x => x.Order).Include(x => x.Product).Where(x => x.Order.MemberId == userId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static OrderDetail FindOrderDetailById(int orderId)
        {
            OrderDetail order = new();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    order = context.OrderDetails.Include(x => x.Order).Include(x => x.Product).SingleOrDefault(x => x.OrderId == orderId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public static void SaveOrder(Order order)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateOrder(Order order)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Entry(order).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteOrder(Order order)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var x = context.Orders.SingleOrDefault(
                        x => x.OrderId == order.OrderId);
                    context.Orders.Remove(x);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void SaveOrderDetail(OrderDetail order)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.OrderDetails.Add(order);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateOrderDetail(OrderDetail order)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Entry(order).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteOrderDetail(OrderDetail order)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var x = context.OrderDetails.SingleOrDefault(x => x.OrderId == order.OrderId
                    && x.ProductId == order.ProductId);
                    context.OrderDetails.Remove(x);
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
