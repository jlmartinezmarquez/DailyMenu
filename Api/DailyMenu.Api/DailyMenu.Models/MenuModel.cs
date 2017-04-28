using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DailyMenu.Models
{
    public class MenuModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime HadLastTimeOn { get; set; }

        public virtual IEnumerable<IngredientModel> Ingredients { get; set; }
    }
}