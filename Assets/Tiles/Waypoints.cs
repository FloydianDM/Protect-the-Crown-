using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheCrown
{
    public class Waypoints : MonoBehaviour
    {
        [SerializeField] private bool isPlaceable;
        [SerializeField] private GameObject tower;

        public bool GetIsPlaceable()
        {
            return isPlaceable;
        }
   
        private void OnMouseDown()
        {
            if (isPlaceable)
            {
                Instantiate(tower, transform.position, Quaternion.identity, gameObject.transform);
                Debug.Log(gameObject.name);
                isPlaceable = false;
            }
        }
    }
}


