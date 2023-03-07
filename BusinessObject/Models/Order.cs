using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }        
        public string MemberId { get; set; }
        [ForeignKey(nameof(MemberId))]
        public IdentityUser? User { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public string Freight { get; set; }
    }
}
