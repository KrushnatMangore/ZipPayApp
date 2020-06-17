using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZipPay.Models
{
    public partial class ZipPayUser
    {
        public ZipPayUser()
        {
            ZipPayAccount = new HashSet<ZipPayAccount>();
        }

        public int ZipPayId { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email ID is required")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Incorrect Email Format")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Salary is required")]
        [RegularExpression("([0-9]+)", ErrorMessage ="Salary must be positive number")]
        public int? Salary { get; set; }

        [Required(ErrorMessage = "Expenses is required")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Expenses must be positive number")]
        public int? Expenses { get; set; }

        public ICollection<ZipPayAccount> ZipPayAccount { get; set; }
    }
}
