using System;
using System.Collections.Generic;
using DailyMenu.Interfaces.Repositories;
using DailyMenu.Models;

namespace DailyMenu.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        public IEnumerable<MenuModel> GetExampleMenus()
        {
            return new List<MenuModel>
            {
                new MenuModel
                {
                    Id = 1,
                    Name = "Arroz con pollo y zanahorias",
                    HadLastTimeOn = DateTime.MinValue,
                    Ingredients = new List<IngredientModel>
                    {
                        new IngredientModel {Id = 1, Name = "Arroz"},
                        new IngredientModel {Id = 2, Name = "Pollo"},
                        new IngredientModel {Id = 3, Name = "Zanahorias"},
                        new IngredientModel {Id = 4, Name = "Ajo"},
                        new IngredientModel {Id = 5, Name = "Cubo de pollo"},
                    }
                },
                new MenuModel
                {
                    Id = 2,
                    Name = "Spaghetti con carne picada",
                    HadLastTimeOn = DateTime.MinValue,
                    Ingredients = new List<IngredientModel>
                    {
                        new IngredientModel {Id = 6, Name = "Spaghetti"},
                        new IngredientModel {Id = 7, Name = "Carne Picada"},
                        new IngredientModel {Id = 8, Name = "Tomate frito"},
                    }
                }
            };
        }
    }
}
