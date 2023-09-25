using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheCrown
{
    public class EnemyHit : MonoBehaviour
    {
        [SerializeField] private int maxHitPoint = 5;

        private int _enemyHitPoint;

        private void Start()
        {
            _enemyHitPoint = maxHitPoint;
        }

        private void OnParticleCollision(GameObject other)
        {
            if (!other.CompareTag("Weapon"))
            {
                return;
            }
            
            ProcessHit();
        }

        private void ProcessHit()
        {
            _enemyHitPoint--;

            if (_enemyHitPoint <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

