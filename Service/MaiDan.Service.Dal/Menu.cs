using System;
using System.Collections.Generic;
using System.Linq;
using MaiDan.Infrastructure.Contract;
using MaiDan.Service.Domain;

namespace MaiDan.Service.Dal
{
    public class Menu : IRepository<Dish, String>
    {
        private IList<Dish> dishes;

        public Menu(IList<Dish> _dishes)
        {
            dishes = _dishes;
        }

        public Menu() : this(new List<Dish>()) { }

        public Dish Get(string id)
        {
            throw new NotImplementedException();
        }

        public void Add(Dish item)
        {
            throw new NotImplementedException();
        }

        public void Update(Dish item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(String id)
        {
            return dishes.Any(d => d.Id == id);
        }
    }
}
