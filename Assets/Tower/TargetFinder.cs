using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheCrown
{
    public class TargetFinder : MonoBehaviour
    {
        [SerializeField] private GameObject enemy;

        private void Update()
        {
            transform.LookAt(enemy.transform);
        }
    }
}

