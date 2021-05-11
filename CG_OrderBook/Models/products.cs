using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CG_OrderBook.Models
{
    public class products
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage ="Product Name Required")]
        public string ProductName { get; set; }
        public string Brand { get; set; }
        [Required(ErrorMessage ="Style Code Required")]
        public string SCode { get; set; }
        public string Specification { get; set; }
    }
}