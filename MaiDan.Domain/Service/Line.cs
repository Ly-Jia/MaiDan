using System;

namespace MaiDan.Domain.Service
{
	/// <summary>
	/// Description of Line.
	/// </summary>
	public class Line
	{
		public int Quantity;
		public String DishName;
		
		public Line(int quantity, String dishName)
		{
			Quantity = quantity;
			DishName = dishName;
		}
		
		public override bool Equals(object obj)
		{
			Line other = obj as Line;
			if (other == null)
				return false;
			return this.Quantity == other.Quantity && this.DishName == other.DishName;
		}

	}
}
