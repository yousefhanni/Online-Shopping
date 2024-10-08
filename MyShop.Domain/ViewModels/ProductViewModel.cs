﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.Domain.Models;


namespace MyShop.Domain.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
                
    }
}
