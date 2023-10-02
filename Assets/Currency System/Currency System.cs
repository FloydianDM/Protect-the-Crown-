using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheCrown
{
    public class CurrencySystem : MonoBehaviour
    {
        [SerializeField] private int startingBalance = 50;
    
        public int CurrentBalance { get; private set; }
        private GameManager _gameManager;

        private void Awake()
        {
            CurrentBalance = startingBalance;
        }

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        public void Deposit(int amount)
        {
            CurrentBalance += Mathf.Abs(amount);
        }

        public void Withdraw(int amount)
        {
            CurrentBalance -= Mathf.Abs(amount);

            if (CurrentBalance < 0)
            {
                _gameManager.LoseGame();
            }
        }
    }
}


