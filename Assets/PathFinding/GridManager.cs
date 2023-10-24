using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheCrown
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Vector2Int gridSize;
        [Tooltip("World grid size should match Unity Editor snap settings.")]
        [SerializeField] private int unityGridSize = 10; 
        public int UnityGridSize => unityGridSize;

        public Dictionary<Vector2Int, Node> Grid { get; private set; } = new Dictionary<Vector2Int, Node>();

        public Node GetNode(Vector2Int coordinates)
        {
            if (Grid.ContainsKey(coordinates))
            {
                return Grid[coordinates];
            }

            return null;
        }

        private void Awake()
        {
            CreateGrid();
        }

        private void CreateGrid()
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    Vector2Int coordinates = new Vector2Int(x, y);
                    Grid.Add(coordinates, new Node(coordinates, true));
                    Debug.Log(Grid[coordinates].coordinates + "=" + Grid[coordinates].isWalkable );
                }
            }
        }

        public void BlockNode(Vector2Int coordinates)
        {
            if (Grid.ContainsKey(coordinates))
            {
                Grid[coordinates].isWalkable = false;
            }
        }

        public void ResetNodes()
        {
            foreach (KeyValuePair<Vector2Int, Node> entry in Grid)
            {
                entry.Value.connectedTo = null;
                entry.Value.isExplored = false;
                entry.Value.isPath = false;
            }
        }

        public Vector2Int GetCoordinatesFromPosition(Vector3 position)
        {
            Vector2Int coordinates = new Vector2Int();
            coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
            coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);

            return coordinates;
        }

        public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
        {
            Vector3 position = new Vector3();
            position.x = Mathf.RoundToInt(coordinates.x * unityGridSize);
            position.y = Mathf.RoundToInt(coordinates.y * unityGridSize);

            return position;
        }
    }
}

