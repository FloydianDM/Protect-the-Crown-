using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheCrown
{
    public class TargetFinder : MonoBehaviour
    {
        // Targeting enemies in scene.
        
        [SerializeField] private Transform shooter;

        private Transform _target;

        private void Start()
        {
            _target = FindObjectOfType<EnemyMovement>().transform;
        }
        
        private void Update()
        {
            AimTarget();
        }

        private void AimTarget()
        {
            shooter.LookAt(_target);
        }
    }
}

