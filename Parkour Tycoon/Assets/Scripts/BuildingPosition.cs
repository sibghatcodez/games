using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingPosition : MonoBehaviour
{
    [SerializeField] List<GameObject> buildingPrefabs;
    [SerializeField] Transform player;
    void Update()
    {
        ChangePositionOfBuilding ();
    }
    void ChangePositionOfBuilding () {
        foreach (var item in buildingPrefabs)
        {
            float Distance = player.position.z - item.transform.position.z;
            //Debug.Log("Building: " + item.name.Split("_")[1] + " | Distance: " + Distance);
            if(Distance > 70 && Distance < 80) {
                float x = item.transform.position.x, y = item.transform.position.y, z = item.transform.position.z;
                item.transform.position = new Vector3(x,y,z+180);
                //Debug.Log("CHANGED THE POSITION OF: " + item.gameObject.name);
            }
        }
    }
}