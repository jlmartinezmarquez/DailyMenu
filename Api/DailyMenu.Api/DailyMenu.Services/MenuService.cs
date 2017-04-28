using System.Collections.Generic;
using DailyMenu.Interfaces;
using DailyMenu.Interfaces.Repositories;
using DailyMenu.Models;

namespace DailyMenu.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public IEnumerable<MenuModel> GetExampleMenus()
        {
            return _menuRepository.GetExampleMenus();
        }
    }
}
