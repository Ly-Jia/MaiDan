using System;
using System.Collections.Generic;
using System.Linq;
using MaiDan.Domain.Service;

namespace MaiDan.DAL
{
    public class Menu : IRepository<Dish, String>
    {
        private IList<Dish> dishes;

        public Menu(IList<Dish> _dishes)
        {
            dishes = _dishes;
        }

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
