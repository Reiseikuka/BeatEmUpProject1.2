using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class GridManager : MonoBehaviour
    {
		public float scale = .1f;
		public Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

		public LayerMask walkLayer;

        public static GridManager singleton;

		private void Awake()
		{
			singleton = this;
			
		}

		public Node GetNode(Vector3 p)
		{
			int x = Mathf.RoundToInt(p.x / scale);
			int y = Mathf.RoundToInt(p.y / scale);

			return GetNode(x, y);
		}

		public Node GetNode(int x, int y)
		{
			Vector2Int p = Vector2Int.zero;
			p.x = x;
			p.y = y;

			if (!grid.ContainsKey(p))
			{
				Node n = new Node();
				n.x = x;
				n.y = y;
				Vector3 tp = Vector3.zero;
				tp.x =	x * scale;
				tp.y = y * scale;
				tp.z = 10;
				n.worldPosition = tp;
				n.isWalkable = GetValidPosition(tp);
				grid.Add(p, n);
			}

			grid.TryGetValue(p, out Node retVal);
			return retVal;
		}
        /*We are never going to iterate into the entire grid, we are 
          going to be dynamically creating the nodes where we need themfor the AI to walk by*/

		bool GetValidPosition(Vector3 o)
		{
			bool retVal = false;
			Collider2D[] colliders = Physics2D.OverlapPointAll(o, walkLayer);

			foreach (var col in colliders)
			{
				TBlockMovement b = col.transform.GetComponentInParent<TBlockMovement>();
				if (b != null)
					continue;

				TWalkable w = col.transform.GetComponentInParent<TWalkable>();
				if (w != null)
				{
					retVal = true;
				}
			}

			return retVal;
		}

		public List<Node> GetPath(Vector3 from, Vector3 to)
		{
			Pathfinder p = new Pathfinder(from, to);
			return p.FindPath();
		}

		public List<Node> GetFlowmap(Vector3 o, int stepCount = 10, int offset = 1)
        {
            List<Node> result = new List<Node>();

			Node origin = GetNode(o);
			List<Node> openSet = new List<Node>();
			List<Node> closedSet = new List<Node>();
			origin.step = 0;
			openSet.Add(origin);
			int steps = 0;

			while (openSet.Count > 0)
			{
				Node currentNode = openSet[0];
				steps = currentNode.step;
				steps++;

				if (steps < stepCount)
				{
					foreach (var n in GetFlowmapNeighbours(currentNode, offset))
					{
						if (!closedSet.Contains(n))
						{
							if (!openSet.Contains(n))
							{
								if(steps <= stepCount)
								{
									n.step = steps;
									openSet.Add(n);
									result.Add(n);
								}
							}
						}
					}
				}

				openSet.Remove(currentNode);
				closedSet.Add(currentNode);
			}

            return result;
        }

		List<Node> GetFlowmapNeighbours(Node currentNode, int offset)
		{
			List<Node> result = new List<Node>();

			for (int x = -1; x <= 1; x++)
			{
				for (int y = -1; y <= 1; y++)
				{
					if (x == 0 && y == 0)
					   continue;

					int _x = currentNode.x + (x*offset);
					int _y = currentNode.y + (y*offset);

					Node n = GetNode(_x, _y);

					if (n.isWalkable)
						result.Add(n);
				}
			}

			return result;
		}
	}
}
