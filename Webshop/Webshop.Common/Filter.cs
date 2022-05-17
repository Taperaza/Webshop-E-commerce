using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Common
{
    public class Filter
    {
        public Guid Id { get; set; }

        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductCategoryDescription { get; set; }



        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }


    }
}
