using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheCrown
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private List<Tile> path = new();
        [SerializeField] [Range(0f, 5f)] private float speed = 1f;

        private Enemy _enemy;
        
        private void OnEnable()
        {
            FindPath();
            ReturnToStart();
            StartCoroutine(FollowPath());
        }

        private void Start()
        {
            _enemy = GetComponent<Enemy>();
        }

        private void FindPath()
        {
            path.Clear();
            
            GameObject pathTilesParent = GameObject.FindGameObjectWithTag("Path");

            foreach (Transform child in pathTilesParent.transform)
            {
                Tile tile = child.GetComponent<Tile>();

                if (tile != null)
                {
                    path.Add(tile);
                }
            }
        }

        private void ReturnToStart()
        {
            transform.position = path[0].transform.position;
        }

        private IEnumerator FollowPath()
        {
            foreach (Tile waypoint in path)
            {
                // LERP Function fpr smooth movement of enemy
                Vector3 startPosition = transform.position;
                Vector3 endPosition = waypoint.transform.position;
                float travelPercent = 0f;
                
                transform.LookAt(endPosition);

                while (travelPercent < 1f)
                {
                    travelPercent += Time.deltaTime * speed;
                    transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                    
                    yield return new WaitForEndOfFrame();
                }
            }
            
            FinishPath(); // Reduce the gold from bank and set the enemy inactive
        }
        
        private void FinishPath()
        {
            StealGold();
            gameObject.SetActive(false); // return enemy object to object pool
        }
        
        private void StealGold()
        {
            if (_enemy == null)
            {
                return;
            }
            
            _enemy.PenaltyGold();
        }
    }
}

