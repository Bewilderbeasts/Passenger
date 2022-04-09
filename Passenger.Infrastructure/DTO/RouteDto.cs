using System;

namespace Passenger.Infrastructure.DTO
{
    public class RouteDto
    {
        public Guid Id { get; protected set; }
		public Node StartNode { get;  set; }
		public Node EndNode { get;  set; }
    }
}