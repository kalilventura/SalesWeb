﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWeb.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        private ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() { }

        public Department(string name)
        {
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}