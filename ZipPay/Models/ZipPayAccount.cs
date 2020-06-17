using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZipPay.Models
{
    public partial class ZipPayAccount
    {
        public int AccountId { get; set; }

        [Required(ErrorMessage ="Account name is required")]
        public string AccountName { get; set; }

        [Required(ErrorMessage ="Creadit amount is required")]
        [Range(0, 100, ErrorMessage = "Zip Pay allows credit for only up to $1000")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Credit must be positive number")]
        public int? Credit { get; set; }
        public int? ZipPayId { get; set; }

        public ZipPayUser ZipPay { get; set; }
    }
}
