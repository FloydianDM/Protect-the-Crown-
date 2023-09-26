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
        [SerializeField] private GameObject tower;

        private ObjectPool _objectPool;

        private void Start()
        {
            _objectPool = FindObjectOfType<ObjectPool>();
        }

        public bool GetIsPlaceable()
        {
            return isPlaceable;
        }
   
        private void OnMouseDown()
        {
            if (isPlaceable)
            {
                Instantiate(tower, transform.position, Quaternion.identity, _objectPool.transform);
                Debug.Log(gameObject.name);
                isPlaceable = false;
            }
        }
    }
}


