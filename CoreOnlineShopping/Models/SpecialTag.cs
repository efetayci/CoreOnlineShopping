﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOnlineShopping.Models
{
    public class SpecialTag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Special Tag Name")]
        public string SpeacialTagNAme { get; set; }
    }
}
