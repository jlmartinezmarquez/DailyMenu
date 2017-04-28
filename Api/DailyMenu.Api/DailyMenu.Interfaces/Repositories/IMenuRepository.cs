using System.Collections.Generic;
using DailyMenu.Models;

namespace DailyMenu.Interfaces.Repositories
{
    public interface IMenuRepository
    {
        IEnumerable<MenuModel> GetExampleMenus();
    }
}
