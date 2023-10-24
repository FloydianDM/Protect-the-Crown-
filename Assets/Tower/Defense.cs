using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ProtectTheCrown
{
    public class Defense : MonoBehaviour
    {
        [SerializeField] private int towerCost = 15;
        [SerializeField] private float buildTime = 1f;

        private void Start()
        {
            StartCoroutine(BuildTower());
        }

        private IEnumerator BuildTower()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
                
                foreach (Transform grandChild in child)
                {
                    grandChild.gameObject.SetActive(false);
                }
            }

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
                
                yield return new WaitForSeconds(buildTime);
                
                foreach (Transform grandChild in child)
                {
                    grandChild.gameObject.SetActive(true);
                }
            }
        }

        public bool CreateTower(Defense tower, Vector3 position)
        {
            CurrencySystem currencySystem = FindObjectOfType<CurrencySystem>();

            if (currencySystem == null)
            {
                return false;
            }
            
            if (currencySystem.CurrentBalance >= towerCost)
            {
                currencySystem.Withdraw(towerCost);
                Debug.Log("Tower");
                Instantiate(tower, position, Quaternion.identity);
                return true;
            }

            Debug.Log("false");
            return false;
        }
    }
}

