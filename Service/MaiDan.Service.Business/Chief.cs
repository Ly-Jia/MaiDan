using MaiDan.Infrastructure.Contract;
using MaiDan.Service.Dal;
using MaiDan.Service.Domain;

namespace MaiDan.Service.Business
{
    public class Chief
    {
        private IRepository<Dish, string> menu;

        public Chief(IRepository<Dish, string> menu)
        {
            this.menu = menu;
        }

        public void AddToMenu(string id, string dishName)
        {
            menu.Add(new Dish(id, dishName));
        }

        public void Update(string id, string newDishName)
        {
            menu.Update(new Dish(id, newDishName));
        }
    }
}
