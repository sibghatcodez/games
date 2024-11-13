using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateManager : MonoBehaviour
{
    [SerializeField] TextMeshPro CoordinateLabel;
    Vector2Int coordinates = new Vector2Int();

    void Awake() {
        DisplayCoordinates();
    }

    void Update()
    {
       if(!Application.isPlaying)
       {
           DisplayCoordinates();
           UpdateObjectName();
       } 
    }

    void DisplayCoordinates() 
    {
        #if UNITY_EDITOR
        coordinates.x = Mathf.RoundToInt(transform.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.position.z / UnityEditor.EditorSnapSettings.move.z);
        #endif

        //CoordinateLabel.text = coordinates.x + "," + coordinates.y;
        CoordinateLabel.text = "";
    }

    void UpdateObjectName()
    {
        transform.name = coordinates.ToString();
    }
}
