using System.Web.Http;
using DailyMenu.Interfaces;

namespace DailyMenu.Api.Controllers
{
    [RoutePrefix("api/menu")]
    public class MenuController : ApiController
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_menuService.GetExampleMenus());
        }
    }
}
