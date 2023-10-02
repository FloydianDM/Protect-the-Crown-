using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheCrown
{
    public class Waypoints : MonoBehaviour
    {
        // Waypoint locator
        // Instantiate defenses by clicking on waypoint tiles
        
        [SerializeField] private bool isPlaceable;
        [SerializeField] private Defense tower;

        public bool GetIsPlaceable()
        {
            return isPlaceable;
        }
   
        private void OnMouseDown()
        {
            if (isPlaceable)
            {
                bool isPlaced = tower.CreateTower(tower, transform.position);
                isPlaceable = !isPlaced;
            }
        }
    }
}


