using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheCrown
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] [Range(0f, 5f)] private float speed = 1f;
        
        private List<Node> _path = new();
        private Enemy _enemy;
        private GridManager _gridManager;
        private Pathfinder _pathfinder;
        
        private void OnEnable()
        {
            ReturnToStart();
            FindPath(true);
        }

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
            _gridManager = FindObjectOfType<GridManager>();
            _pathfinder = FindObjectOfType<Pathfinder>();
        }

        private void FindPath(bool resetPath)
        {
            Vector2Int coordinates = new Vector2Int();

            if (resetPath)
            {
                coordinates = _pathfinder.StartingCoordinates;
            }
            else
            {
                coordinates = _gridManager.GetCoordinatesFromPosition(transform.position);
            }
            
            StopAllCoroutines();
            _path.Clear();
            _path = _pathfinder.GetNewPath(coordinates);
            StartCoroutine(FollowPath());
        }

        private void ReturnToStart()
        {
            transform.position = _gridManager.GetPositionFromCoordinates(_pathfinder.StartingCoordinates);
        }

        private IEnumerator FollowPath()
        {
            for (int i = 1; i < _path.Count; i++)
            {
                // LERP Function fpr smooth movement of enemy
                Vector3 startPosition = transform.position;
                Vector3 endPosition = _gridManager.GetPositionFromCoordinates(_path[i].coordinates);
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

