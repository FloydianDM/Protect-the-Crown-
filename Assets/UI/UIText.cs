using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ProtectTheCrown
{
    public class UIText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI goldText;

        private CurrencySystem _currencySystem;
        
        private void Start()
        {
            _currencySystem = FindObjectOfType<CurrencySystem>();
        }
        
        private void Update()
        {
            goldText.text = $"Gold: {_currencySystem.CurrentBalance} ";
        }
    }
}

