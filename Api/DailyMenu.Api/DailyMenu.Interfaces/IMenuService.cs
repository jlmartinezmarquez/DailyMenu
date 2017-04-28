using System.Collections.Generic;
using DailyMenu.Models;

namespace DailyMenu.Interfaces
{
    public interface IMenuService
    {
        IEnumerable<MenuModel> GetExampleMenus();
    }
}
