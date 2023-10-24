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
        private GridManager _gridManager;
        private Color _defaultColor = Color.white;
        private Color _blockedColor = Color.gray;
        private Color _exploredColor = Color.red;
        private Color _pathColor = Color.blue;
        
        private void Awake()
        {
            _label = GetComponent<TextMeshPro>();
            _gridManager = FindObjectOfType<GridManager>();
            
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
            if (_gridManager == null) {return;}

            Node node = _gridManager.GetNode(_coordinates); // temporary Node object for method only
            
            if (node == null) {return;}
            
            if (!node.isWalkable)
            {
                _label.color = _blockedColor;
            }
            else if (node.isPath)
            {
                _label.color = _pathColor;
            }
            else if (node.isExplored)
            {
                _label.color = _exploredColor;
            }
            else
            {
                _label.color = _defaultColor;
            }
        }

        private void DisplayCoordinates()
        {
            if (_gridManager == null) { return;}
            
            _coordinates.x = Mathf.RoundToInt(transform.parent.position.x / _gridManager.UnityGridSize);
            _coordinates.y = Mathf.RoundToInt(transform.parent.position.z / _gridManager.UnityGridSize);

            _label.text = $"{_coordinates.x} , {_coordinates.y}";
        }

        private void DisplayObjectName()
        {
            transform.parent.name = _coordinates.ToString();
        }
    }
}


