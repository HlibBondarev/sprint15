using Newtonsoft.Json.Linq;
using ProductsValidation.Models.ModelValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


// TODO: 1. + Price: value should be within boundaries 0 and 100000
// TODO: 2. + Name: mandatory field
// TODO: 3. + Description:
// TODO:        + not mandatory fild.
// TODO:        + if set, length sould be longer than 2 symbols
// TODO:        + should not be equal to Name but should start with the Name of the product
// TODO: 4. + Type: enum with values: Toy, Technique, Clothes, Transport - make this field displayed with a SelectList on
//            Views for editing and creation to make impossible entering wrong value.

namespace ProductsValidation.Models
{
    [DescriptionNameValidation]
    public class Product
    {
        public enum Category { Toy, Technique, Clothes, Transport}
        public int Id { get; set; }
        public Category Type { get; set; }
        [Required]
        public string Name { get; set; }
        [DescriptionValidation(2)]
        public string Description { get; set; }
		[Display(Name = "Price, $")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F4}")]
		[Range(0, 100000)]
        public decimal Price { get; set; }
    }
}
