using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ProtectTheCrown
{
    public class Pathfinder : MonoBehaviour
    {
        // Breadth First Search (BFS) Pathfinding Algorithm

        [SerializeField] private Vector2Int startingCoordinates;
        [SerializeField] private Vector2Int destinationCoordinates;
        
        private Node _startingNode;
        private Node _destinationNode;
        private Node _currentSearchNode;

        private Queue<Node> _frontierRoute = new Queue<Node>();
        private Dictionary<Vector2Int, Node> _reachedRoute = new Dictionary<Vector2Int, Node>(); // for collecting reached nodes
        
        private readonly Vector2Int[] _directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
        private GridManager _gridManager;
        private Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();

        private void Awake()
        {
            _gridManager = FindObjectOfType<GridManager>();
        }

        private void Start()
        {
            _startingNode = _gridManager.Grid[startingCoordinates];
            _destinationNode = _gridManager.Grid[destinationCoordinates];

            GetNewPath();
        }

        private List<Node> GetNewPath()
        {
            _gridManager.ResetNodes();
            BreadthFirstSearch();
            BuildPath();

            return BuildPath();
        }

        private void ExploreNeighbors()
        {
            /* Create an empty list called "neighbors"
               Loop through all for directions
               Calculate the coordinates of the node in that direction from our currentSearchNode
               Check if the neighbor's coordinates exist in the grid
               If it does exist in the grid, add it to our neighbors list */

            List<Node> neighbors = new List<Node>();
            
            foreach (Vector2Int direction in _directions)
            {
                Vector2Int nextNodeCoordinates = _currentSearchNode.coordinates + direction;
                if (_gridManager.Grid.ContainsKey(nextNodeCoordinates))
                {
                    neighbors.Add(_gridManager.Grid[nextNodeCoordinates]);
                }
            }

            foreach (Node neighbor in neighbors)
            {
                if (!_reachedRoute.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
                {
                    neighbor.connectedTo = _currentSearchNode;
                    _reachedRoute.Add(neighbor.coordinates, neighbor);
                    _frontierRoute.Enqueue(neighbor);
                }
            }
        }

        private void BreadthFirstSearch()
        {
            _frontierRoute.Clear();
            _reachedRoute.Clear();
            
            bool isRunning = true;
            
            _frontierRoute.Enqueue(_startingNode);
            _reachedRoute.Add(startingCoordinates, _startingNode);

            while (_frontierRoute.Count > 0 && isRunning)
            {
                _currentSearchNode = _frontierRoute.Dequeue();
                _currentSearchNode.isExplored = true;
                ExploreNeighbors();
                if (_currentSearchNode.coordinates == _destinationNode.coordinates)
                {
                    isRunning = false;
                }
            }
        }

        private List<Node> BuildPath()
        {
            List<Node> path = new List<Node>();
            Node currentNode = _destinationNode;
            path.Add(currentNode);
            currentNode.isPath = true;

            while (currentNode.connectedTo != null)
            {
                currentNode = currentNode.connectedTo;
                path.Add(currentNode);
                currentNode.isPath = true;
            }
            
            path.Reverse();

            return path;
        }

        public bool WillBlockPath(Vector2Int coordinates)
        {
            if (_grid.ContainsKey(coordinates))
            {
                bool previousState = _grid[coordinates].isWalkable;
                
                _grid[coordinates].isWalkable = false;
                List<Node> newPath = GetNewPath();
                _grid[coordinates].isWalkable = previousState;

                if (newPath.Count <= 1)
                {
                    GetNewPath();
                    return true;
                }
            }
            
            return false;
        }
    }   
}

