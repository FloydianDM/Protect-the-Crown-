using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheCrown
{
    public class Tile : MonoBehaviour
    {
        // Waypoint locator
        // Instantiate defenses by clicking on waypoint tiles
        
        [SerializeField] private bool isPlaceable;
        [SerializeField] private Defense tower;
        
        public bool IsPlaceable => isPlaceable;
        private GridManager _gridManager;
        private Pathfinder _pathfinder;
        private Vector2Int _coordinates = new Vector2Int();

        private void Awake()
        {
            _gridManager = FindObjectOfType<GridManager>();
            _pathfinder = FindObjectOfType<Pathfinder>();
        }

        private void Start()
        {
            if (_gridManager != null)
            {
                _coordinates = _gridManager.GetCoordinatesFromPosition(transform.position);
               
                if (!isPlaceable)
                {
                    _gridManager.BlockNode(_coordinates);
                }
            }
        }
        
        private void OnMouseDown()
        {
            if (_gridManager.GetNode(_coordinates).isWalkable && !_pathfinder.WillBlockPath(_coordinates))
            {
                bool isPlaced = tower.CreateTower(tower, transform.position);
                isPlaceable = !isPlaced;
                _gridManager.BlockNode(_coordinates);
            }
        }
    }
}


