using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMovement : MonoBehaviour
{
    [SerializeField] GameObject[] path;  // Path for enemy to follow

    int towersAllowed = 3;
    int enemiesToKill = 0;
    int totalEnemies = 0;
    
    void OnEnable()
    {
        StartCoroutine(MoveEnemy());
    }
    IEnumerator MoveEnemy()
    {
        foreach (GameObject tile in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = tile.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * 1f;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return null;
            }
        }
    }
}