using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CG_OrderBook.Models
{
    public class Orders
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string OrderDate { get; set; }
        public string OrderPaid { get; set; }
        public string OrderStatus { get; set; }
        public string OrderNotes { get; set; }
    }
}