using System;

public class Vehicle //ValueObject
{
		public string Brand { get; protected set; }
		public string Name { get; protected set; }
		public int Seats { get; protected set; }

		protected Vehicle ()
		{
			

		}
		protected Vehicle (string brand, string name, int seats)
		{
			Brand = brand;
			Name = name;
			Seats = seats;

		}

		

		private void SetBrand(string brand)
		{
			if (string.IsNullOrWhiteSpace(brand))
			{
				throw new Exception("Please provide valid data.");
			}
			if (Brand == brand)
			{
				return;
			}
			Brand = brand;
		}

		private void SetName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new Exception("Please provide valid data.");
			}
			if (Name == name)
			{
				return;
			}
			Name = name;
		}

		private void SetSeats(int seats)
		{
			if (seats < 0)
			{
				throw new Exception("Seats must be above 0.");
			}
			if (seats < 9)
			{
				throw new Exception("You cannot provide more than 9.");
			}
			if (Seats == seats)
			{
				return;
			}
			Seats = seats;
		}

		public static Vehicle Create(string brand, string name, int seats)
			=> new Vehicle(brand, name, seats);
		
}
