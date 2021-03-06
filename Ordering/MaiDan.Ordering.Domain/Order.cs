﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Ordering.Domain
{
    /// <summary>
    /// Description of Order.
    /// </summary>
    public abstract class Order
    {
        public int Id { get; }
        public DateTime OrderingDate { get; }
        public IList<Line> Lines { get; private set; }
        public bool Closed { get; private set; }

        public Order(int id,  DateTime orderingDate, IList<Line> lines, bool closed)
        {
            Id = id;
            OrderingDate = orderingDate;
            Lines = lines;
            Closed = closed;
        }

        public Order Add(Line line)
        {
            Lines.Add(line);
            return this;
        }

        public Order Add(int quantity, bool free, Dish dish)
        {
            return this.Add(new Line(Lines.Count, quantity, free, dish));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Order other))
                return false;

            return this.Id == other.Id && this.Lines.SequenceEqual(other.Lines);
        }

        public void Update(IList<Line> updatedLines)
        {
            Lines = updatedLines;
        }

        public void Close()
        {
            Closed = true;
        }
    }
}
