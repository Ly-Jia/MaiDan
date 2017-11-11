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

	    public Order(string id, IList<Line> lines)
	    {
	        Id = id;
	        Lines = lines;
	    }

        public Order Add(Line line)
		{
			Lines.Add(line);
			return this;
		}
		
		public Order Add(int quantity, string dishName)
		{
			return this.Add(new Line(quantity, dishName));
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
