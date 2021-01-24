using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCart.Domain.Models
{
    public class Cart
    {
        // [autogeneration]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Email { get; set; }

        [ForeignKey("Product")]
        [Required]
        public Guid ProductFk { get; set; }

        [Required]
        public virtual Product Product {get;set;}

        [Required]
        public int Quantity { get; set; }
    }
}
