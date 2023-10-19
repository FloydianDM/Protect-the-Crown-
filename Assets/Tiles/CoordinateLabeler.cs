using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

namespace ProtectTheCrown
{
    /// <summary>
    /// Move the script to Editor folder while building the project
    /// </summary>
    
    [RequireComponent(typeof(TextMeshPro))]
    [ExecuteAlways]
    public class CoordinateLabeler : MonoBehaviour
    {
        private TextMeshPro _label;
        private Vector2Int _coordinates;
        private Waypoints _waypoints;
        private Color _defaultColor = Color.white;
        private Color _changedColor = Color.gray;
        
        private void Awake()
        {
            _label = GetComponent<TextMeshPro>();
            _waypoints = GetComponentInParent<Waypoints>();
            
            _label.enabled = false;
           
            DisplayCoordinates();
        }

        private void Update()
        {
            if (!Application.isPlaying)
            {
                DisplayCoordinates();
                DisplayObjectName();
                _label.enabled = true;
            }

            SetLabelColor();
            ToggleLabels();
        }

        private void ToggleLabels()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                _label.enabled = !_label.IsActive();
            }
        }

        private void SetLabelColor()
        {
            if (!_waypoints.GetIsPlaceable())
            {
                _label.color = _changedColor;
            }
            else
            {
                _label.color = _defaultColor;
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


