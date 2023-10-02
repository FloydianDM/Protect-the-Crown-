using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheCrown
{
    public class CurrencySystem : MonoBehaviour
    {
        [SerializeField] private int startingBalance = 50;
    
        public int CurrentBalance { get; private set; }

        private void Awake()
        {
            CurrentBalance = startingBalance;
        }

        public void Deposit(int amount)
        {
            CurrentBalance += Mathf.Abs(amount);
        }

        public void Withdraw(int amount)
        {
            CurrentBalance -= Mathf.Abs(amount);
        }
    }
}


