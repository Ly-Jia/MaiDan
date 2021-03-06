﻿using System;
using System.Collections.Generic;
using MaiDan.Ordering.Domain;

namespace Test.MaiDan.Ordering
{
    /// <summary>
    /// Builder that creates Order
    /// </summary>
    public class AnOrder
    {
        private const int DefaultId = 1;
        private static readonly Table DefaultTable = new Table("1");
        private readonly int id;
        private List<Line> lines;
        private bool takeAway = false;
        private int numberOfGuests = 0;
        private Table table;
        private DateTime orderingDate = new DateTime(2018, 08, 08);
        private bool closed = false;

        /// <summary>
        /// Initialize the future order with a specific id (creation date)
        /// </summary>
        /// <param name="id"></param>
        public AnOrder(int id)
        {
            this.lines = new List<Line>();
            this.id = id;
        }

        /// <summary>
        /// Initialize the order with a default id (DateTime : 2012/12/21)
        /// </summary>
        public AnOrder() : this(DefaultId)
        {
        }

        public AnOrder OnSite(string tableId, int guests)
        {
            takeAway = false;
            table = new Table(tableId);
            numberOfGuests = guests;
            return this;
        }

        public AnOrder OnSite(Table t, int guests)
        {
            takeAway = false;
            table = t;
            numberOfGuests = guests;
            return this;
        }

        public AnOrder TakeAway()
        {
            takeAway = true;
            return this;
        }

        /// <summary>
        /// Add a new line to the created order
        /// </summary>
        /// <param name="quantity">The dish quantity</param>
        /// <param name="dish">The dish</param>
        /// <returns>The order</returns>
        public AnOrder With(int quantity, Dish dish)
        {
            return With(quantity, false, dish);
        }

        /// <summary>
        /// Add a new line to the created order
        /// </summary>
        /// <param name="quantity">The dish quantity</param>
        /// <param name="free">Is the dish free or not</param>
        /// <param name="dish">The dish</param>
        /// <returns>The order</returns>
        public AnOrder With(int quantity, bool free, Dish dish)
        {
            return With(new Line(this.lines.Count + 1, quantity, free, dish));
        }

        /// <summary>
        /// Add a new line to the created order
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public AnOrder With(Line line)
        {
            lines.Add(line);
            return this;
        }

        public AnOrder AlreadyPrinted()
        {
            closed = true;
            return this;
        }

        public AnOrder And(int quantity, Dish dish)
        {
            return With(quantity, dish);
        }

        /// <summary>
        /// Instanciate the order
        /// </summary>
        /// <returns></returns>
        public Order Build()
        {
            if (takeAway)
                return new TakeAwayOrder(id, orderingDate, lines, false);

            return new OnSiteOrder(id, table ?? DefaultTable, numberOfGuests, orderingDate, lines, closed);
        }
    }
}
