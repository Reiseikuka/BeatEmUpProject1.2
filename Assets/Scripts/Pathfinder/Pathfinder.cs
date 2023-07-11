using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	public class Pathfinder 
	{
		Node startNode;
		Node endNode;


		public Pathfinder(Vector3 start, Vector3 target)
		{
			startNode = GetNode(start);
			endNode = GetNode(target);
		}

		public List<Node> FindPath()
		{
			List<Node> path = FindPathActual();
			return path;
		}

		List<Node> FindPathActual()
		{
            List<Node> foundPath = new List<Node>();

            //We need two lists, one for the nodes we need to check and one for the nodes we've already checked
            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();

            //We start adding to the open set
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];

                for (int i = 0; i < openSet.Count; i++)
                {
                    //We check the costs for the current node
                    //You can have more opt. here but that's not important now
                    if (openSet[i].fCost < currentNode.fCost ||
                        (openSet[i].fCost == currentNode.fCost &&
                        openSet[i].hCost < currentNode.hCost))
                    {
                        //and then we assign a new current node
                        if (!currentNode.Equals(openSet[i]))
                        {
                            currentNode = openSet[i];
                        }
                    }
                }

                //we remove the current node from the open set and add to the closed set
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                //if the current node is the target node
                if (currentNode.Equals(endNode))
                {
                    //that means we reached our destination, so we are ready to retrace our path
                    foundPath = RetracePath(startNode, currentNode);
                    break;
                }

                //if we haven't reached our target, then we need to start looking the neighbours
                foreach (Node neighbour in GetNeighbours(currentNode))
                {
                    if (!closedSet.Contains(neighbour))
                    {
                        //we create a new movement cost for our neighbours
                        float newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                        //and if it's lower than the neighbour's cost
                        if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                        {
                            //we calculate the new costs
                            neighbour.gCost = newMovementCostToNeighbour;
                            neighbour.hCost = GetDistance(neighbour, endNode);
                            //Assign the parent node
                            neighbour.parentNode = currentNode;
                            //And add the neighbour node to the open set
                            if (!openSet.Contains(neighbour))
                            {
                                openSet.Add(neighbour);
                            }
                        }
                    }
                }
            }

            //we return the path at the end
            return foundPath;
        }

        private List<Node> GetNeighbours(Node node)
        {
            List<Node> retList = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {

                    if (x == 0 && y == 0 && y == 0)
                    {
                        //000 is the current node
                    }
                    else
                    {
                        int _x = node.x + x;
                        int _y = node.y + y;

                        Node targetNode = GetNode(_x, _y);
                        if (targetNode.isWalkable)
                            retList.Add(targetNode);
                    }
                }
            }

            return retList;

        }

        private List<Node> RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parentNode;
            }

            path.Reverse();
            return path;
        }

        private int GetDistance(Node posA, Node posB)
        {
            int distX = Mathf.Abs(posA.x - posB.x);
            int distY = Mathf.Abs(posA.y - posB.y);
         
            return 14 * distX + 10 * (distY - distX) + 10 * distY;
        }

        Node GetNode(Vector3 p)
		{
			return GridManager.singleton.GetNode(p);
		}

		Node GetNode(int x, int y)
		{
			return GridManager.singleton.GetNode(x, y);
		}

      

	}

	public class Node {
		public int x;
		public int y;
        public int step;
		public float hCost;
		public float gCost;
		public float fCost {
			get {
				return gCost + hCost;
			}
		}

		public Node parentNode;
		public bool isWalkable;
		public Vector3 worldPosition;

	}
}