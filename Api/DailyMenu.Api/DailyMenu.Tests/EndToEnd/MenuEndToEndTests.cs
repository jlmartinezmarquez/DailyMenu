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
            var uri = "http://localhost/api/menu";

            var listOfMenus = JsonServiceClient.Get<List<MenuModel>>(uri);

            Assert.That(listOfMenus.Any());
        }
    }
}
