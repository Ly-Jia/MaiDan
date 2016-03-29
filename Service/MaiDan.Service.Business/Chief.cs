using MaiDan.Service.Dal;
using MaiDan.Service.Domain;

namespace MaiDan.Service.Business
{
    public class Chief
    {
        private Menu menu;

        public Chief(Menu menu)
        {
            this.menu = menu;
        }

        public void AddToMenu(string dishName)
        {
            menu.Add(new Dish(dishName));
        }
    }
}
