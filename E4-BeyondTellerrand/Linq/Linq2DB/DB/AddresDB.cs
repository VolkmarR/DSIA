using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Demos.Linq2DB.DB
{
    public class AddressDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string StreetName { get; set; }
        public string StreetNo { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}
