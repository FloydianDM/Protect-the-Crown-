using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheCrown
{
    public class ObjectPool : MonoBehaviour
    {
        // Controls how many items will be active in the scene.
        // Collect all instantiated objects in one parent.
        // Instantiate enemies every second of play.
        
        [SerializeField] private GameObject enemy;
        [SerializeField] [Range(0, 50)] private int poolSize = 5;
        [SerializeField] [Range(0.1f, 20f)] private float spawnTime = 1f;

        private GameObject[] _pool;

        private void Awake()
        {
            PopulatePool();
        }

        private void PopulatePool()
        {
            _pool = new GameObject[poolSize];

            for (int i = 0; i < _pool.Length; i++)
            {
                _pool[i] = Instantiate(enemy, transform);
                _pool[i].SetActive(false); // keep enemy object in pool
            }
        }

        private void Start()
        {
            StartCoroutine(SpawnEnemies());
        }

        private void EnableObjectInPool()
        {
            for (int i = 0; i < _pool.Length; i++)
            {
                if (_pool[i].activeInHierarchy == false)
                {
                    _pool[i].SetActive(true);
                    return;
                }
            }
        }

        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                EnableObjectInPool();
            
                yield return new WaitForSeconds(spawnTime);
            }
        }
    }
}

