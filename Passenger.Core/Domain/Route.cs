using System;

namespace Passenger.Core.Domain
{
public class Route
	{
	
		public string Name { get; protected set; }
		public Guid Id { get; protected set; }
		public Node StartNode { get; protected set; }
		public Node EndNode { get; protected set; }


		protected Route()
		{
			Id = Guid.NewGuid();
		}
		protected Route(string name, Node startnode, Node endnode)
		{
			Name = name;
			StartNode = startnode;
			EndNode = endnode;
		}

		public static Route Create(string name, Node startnode, Node endnode)
			=> new Route(name, startnode, endnode);
	}
}