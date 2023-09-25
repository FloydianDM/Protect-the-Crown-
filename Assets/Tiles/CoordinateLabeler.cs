using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

namespace ProtectTheCrown
{
    [ExecuteAlways]
    public class CoordinateLabeler : MonoBehaviour
    {
        private TextMeshPro _label;
        private Vector2Int _coordinates = new();
        
        private void Awake()
        {
            _label = GetComponent<TextMeshPro>();
            DisplayCoordinates();
        }

        private void Update()
        {
            if (!Application.isPlaying)
            {
                DisplayCoordinates();
                DisplayObjectName();
            }
        }

        private void DisplayCoordinates()
        {
            _coordinates.x = (int)(transform.parent.position.x / EditorSnapSettings.move.x);
            _coordinates.y = (int)(transform.parent.position.z / EditorSnapSettings.move.z);

            _label.text = $"{_coordinates.x} , {_coordinates.y}";
        }

        private void DisplayObjectName()
        {
            transform.parent.name = _coordinates.ToString();
        }
    }
}


