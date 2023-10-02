using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheCrown
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int enemyReward = 25;
        [SerializeField] private int enemyPenalty = 25;

        private CurrencySystem _currencySystem;
        
        void Start()
        {
            _currencySystem = FindObjectOfType<CurrencySystem>();
        }

        public void RewardGold()
        {
            if (_currencySystem == null)
                return;
            
            _currencySystem.Deposit(enemyReward);
        }

        public void PenaltyGold()
        {
            if (_currencySystem == null)
                return;
            
            _currencySystem.Withdraw(enemyPenalty);
        }
    }
}

