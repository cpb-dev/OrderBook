using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CG_OrderBook.Models
{
    public class OrderListView
    {
        public int CustomerID { get; set; }
        public string CustFName { get; set; }
        public string CustSName { get; set; }
        public int OrderID { get; set; }
        public string OrderDate { get; set; }
        public int ProductID { get; set; }
        public string Brand { get; set; }
        public string SCode { get; set; }
    }
}