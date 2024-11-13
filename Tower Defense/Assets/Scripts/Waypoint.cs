using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
     [SerializeField] GameObject Tower;
     [SerializeField] GameObject TowersPool;

    void Start()
    {
    }
    void OnMouseDown() {
        if(gameObject.tag.Equals("Untagged")) {
            if(TowersPool.transform.childCount < PlayerPrefs.GetInt("towersAllowed")){
                GameObject obj = Instantiate(Tower, transform.position, Quaternion.identity);
                obj.transform.SetParent(TowersPool.transform);
                gameObject.tag = "Obstacle";
            }
        }
    }
}