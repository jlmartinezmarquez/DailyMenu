using System.Collections.Generic;
using System.Linq;
using DailyMenu.Models;
using DailyMenu.Tests.Framework;
using NUnit.Framework;

namespace DailyMenu.Tests.EndToEnd
{
    [TestFixture]
    public class MenuEndToEndTests : EntToEndTestBase
    {
        [Test]
        public void Should_get_menus()
        {
            var uri = "http://localhost:8080/api/menu";

            var listOfMenus = Client.Get<List<MenuModel>>(uri);

            Assert.That(listOfMenus.Any());
        }
    }
}
