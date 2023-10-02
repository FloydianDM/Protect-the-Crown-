using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheCrown
{
    public class Defense : MonoBehaviour
    {
        [SerializeField] private int towerCost = 15;
        
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

