using System.ComponentModel.DataAnnotations;

namespace DailyMenu.Models
{
    public class IngredientModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }    
    }
}