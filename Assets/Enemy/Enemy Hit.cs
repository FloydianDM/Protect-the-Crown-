using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheCrown
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyHit : MonoBehaviour
    {
        // Controls enemy health and HP
        
        [SerializeField] private int maxHitPoint = 5;
        
        [Tooltip("Adds amount of extra hitpoints to nre enemy when enemy dies")]
        [SerializeField] private int difficultyMultiplier = 1;
        
        private Enemy _enemy;
        private int _enemyHitPoint;
        
        private void OnEnable()
        {
            _enemyHitPoint = maxHitPoint;
        }

        private void Start()
        {
            _enemy = GetComponent<Enemy>();
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
                gameObject.SetActive(false); // return enemy object to object pool
                maxHitPoint += difficultyMultiplier;
                _enemy.RewardGold();
            }
        }
    }
}


