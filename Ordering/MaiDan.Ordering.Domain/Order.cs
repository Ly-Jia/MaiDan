using System.Collections.Generic;

namespace MaiDan.Ordering.Domain
{
	/// <summary>
	/// Description of Order.
	/// </summary>
	public class Order
	{
		public string Id { get; }
		public IList<Line> Lines { get; private set; }
        public Table Table { get; }
        public int NumberOfGuests { get; }

	    public Order(string id, Table table, int numberOfGuests, IList<Line> lines)
	    {
	        Id = id;
	        Lines = lines;
	        Table = table;
	        NumberOfGuests = numberOfGuests;
	    }

        public Order Add(Line line)
		{
			Lines.Add(line);
			return this;
		}
		
		public Order Add(int quantity, Dish dish)
		{
			return this.Add(new Line(quantity, dish));
		}
		
		public override bool Equals(object obj)
		{
			Order other = obj as Order;
			if (other == null)
				return false;
			return this.Id == other.Id;
		}

		public void Update(IList<Line> updatedLines)
		{
			Lines = updatedLines;
		}
	}
}
