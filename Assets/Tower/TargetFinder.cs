using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProtectTheCrown
{
    public class TargetFinder : MonoBehaviour
    {
        // Targeting enemies in scene.
        
        [SerializeField] private Transform shooter;
        [SerializeField] private float towerRange = 1f;
        [SerializeField] private GameObject ammo;

        private Transform _target;
        private ParticleSystem _ammoParticles;

        private void Start()
        {
            _ammoParticles = ammo.GetComponent<ParticleSystem>();
        }
        
        private void Update()
        {
            FindClosestEnemy();
            AimTarget();
        }

        private void FindClosestEnemy()
        {
            // Find every enemy target in the scene and compare the distances

            Enemy[] enemies = FindObjectsOfType<Enemy>();
            Transform closestTarget = null;
            float maxDistance = Mathf.Infinity;

            foreach (Enemy enemy in enemies)
            {
                float targetDistance = Vector3.Distance(enemy.transform.position, transform.position);

                if (targetDistance < maxDistance)
                {
                    closestTarget = enemy.transform;
                    maxDistance = targetDistance;
                }
            }
            
            _target = closestTarget;
        }

        private void AimTarget()
        {
            float targetDistance = Vector3.Distance(transform.position, _target.position);

            ToggleAttack(targetDistance <= towerRange);

            shooter.LookAt(_target);
        }

        private void ToggleAttack(bool isInRange)
        {
            var ammoEmission = _ammoParticles.emission;
            
            if (!isInRange)
            {
                ammoEmission.enabled = false;
            }
            else
            {
                ammoEmission.enabled = true;
            }
        }
    }
}

