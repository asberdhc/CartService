﻿using CartService.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CartService.Models
{
    public class Data
    {
       // DataProductsContext db = new DataProductsContext();
        private DataProductsContext db;
        public Data(DataProductsContext db)
        {
            this.db = db;
        }


        public bool AddtoCart(CartIn cart)
        {
            try
            {
                CatTypeDetails catTypeDetails = new CatTypeDetails
                {
                   IdType = 1,
                    Code = cart.idProduct.ToString(),
                    Name = cart.idClient.ToString(),
                    Description = cart.quantity.ToString()
                };
                db.CatTypeDetails.Add(catTypeDetails);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public Cart GetCartById(string id)
        {
            Cart cartList = new Cart();
            cartList.idClient = id;
            List<Items> cart = db.CatTypeDetails
                .Where(p => p.Name == id)
                .Select(p => new Items
                {
                    idProduct = p.Code,
                    quantity = Int32.Parse(p.Description)
                }
                ).ToList();
            cartList.Items = cart;
            return cartList;
        }


        public bool EmptyCart(string id)
        {
            var result = from c in db.CatTypeDetails
                .Where(c => c.Name == id)
                         select c;

            foreach (var item in result)
            {
                db.CatTypeDetails.Remove(item);
            }
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

    }
}
