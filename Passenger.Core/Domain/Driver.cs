﻿using System;
using System.Collections.Generic;

namespace Passenger.Core.Domain
{
public class Driver
{
	
		public Guid UserId { get; protected set; }
		public string Name { get; protected set; }
		
		public Vehicle Vehicle { get; protected set; }

		public IEnumerable<Route> Routes { get; protected set; }

		public IEnumerable<DailyRoute> DailyRoutes { get; protected set; }
		public DateTime UpdatedAt {get; protected set;}

		protected Driver()
		{

		}

		public Driver ( User User)
		{
			UserId = User.Id;
			Name = User.Username;
			
		}

        public void SetVechicle(string brand, string name, int seats)
        {
            Vehicle = Vehicle.Create(brand, name, seats);
			UpdatedAt = DateTime.UtcNow;
        }
    }
}