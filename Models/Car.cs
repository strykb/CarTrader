﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace CarTrader.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        [Display(Name = "Published at")]
        public DateTime PublishedAt { get; set; }

        [Display(Name = "Sold at")]
        public DateTime? SoldAt { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        [Range(0, 999999999)]
        public int Price { get; set; }
        public bool Sold { get; set; } = false;
        public bool Approved { get; set; } = false;
        public bool Canceled { get; set; } = false;
        public string Image { get; set; }
        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
