using System;

namespace MaiDan.Domain.Service
{
	/// <summary>
	/// Description of Line.
	/// </summary>
	public class Line
	{
		public int Quantity;
		public String DishCode;
		
		public Line(int quantity, String dishCode)
		{
			Quantity = quantity;
			DishCode = dishCode;
		}
		
		public override bool Equals(object obj)
		{
			Line other = obj as Line;
			if (other == null)
				return false;
			return this.Quantity == other.Quantity && this.DishCode == other.DishCode;
		}

	}
}
