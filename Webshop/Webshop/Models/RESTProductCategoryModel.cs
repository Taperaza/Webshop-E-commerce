using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop.Models
{
    public class RESTProductCategoryModel
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }  
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

       
    }
}