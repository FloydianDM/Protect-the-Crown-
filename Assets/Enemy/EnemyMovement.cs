using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheCrown
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private List<Waypoints> path = new();
        [SerializeField] [Range(0f, 5f)] private float speed = 1f;
        
        private void OnEnable()
        {
            FindPath();
            ReturnToStart();
            StartCoroutine(FollowPath());
        }

        private void FindPath()
        {
            path.Clear();
            
            GameObject[] pathWaypoints = GameObject.FindGameObjectsWithTag("Path");

            foreach (var waypoint in pathWaypoints)
            {
                path.Add(waypoint.GetComponent<Waypoints>());
            }
        }

        private void ReturnToStart()
        {
            transform.position = path[0].transform.position;
        }

        private IEnumerator FollowPath()
        {
            foreach (Waypoints waypoint in path)
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

            gameObject.SetActive(false); // return enemy object to object pool
        }
    }
}

