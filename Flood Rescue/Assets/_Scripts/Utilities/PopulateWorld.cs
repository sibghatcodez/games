using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateWorld : MonoBehaviour
{
    [SerializeField] private List<BoxCollider> streetColliders;
    [SerializeField] private GameObject[] logs;
    [SerializeField] private GameObject[] beings;
    [SerializeField] private Transform referencePoint;
    [SerializeField] private GameObject[] items; // Array for items to be placed randomly
    private int numberOfPlayers = 9;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private BoxCollider boxCollider;
    private int timesUsed;

    private void Start()
    {
        PreSortColliders();
        GenerateWorld();
    }

    public void GenerateWorld()
    {
        spawnedObjects.Clear();
        DisableEverything();
        Populate();
    }

    private void Populate()
    {
        PlaceObjects(20);   // Place logs
        PlaceBeings(15);    // Place beings (e.g., characters, NPCs)
        PlaceRandomItems(10); // Place random items
    }

    private void PreSortColliders()
    {
        streetColliders.Sort((a, b) =>
            (referencePoint.position - a.transform.position).sqrMagnitude
            .CompareTo((referencePoint.position - b.transform.position).sqrMagnitude));
    }

    public void DisableEverything()
    {
        foreach (var log in logs) log.SetActive(false);
        foreach (var being in beings) being.SetActive(false);
    }

    public void PlaceBeings(int beingsToPlace)
    {
        HashSet<int> occupiedIndices = new HashSet<int>();

        for (int i = 0; i < beingsToPlace && i < beings.Length; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, spawnedObjects.Count);
            } while (occupiedIndices.Contains(randomIndex));

            occupiedIndices.Add(randomIndex);
            beings[i].transform.position = spawnedObjects[randomIndex].transform.position + Vector3.up;
            StartCoroutine(ActivateBeing(beings[i], 1f)); // Activate after 1 second
        }
    }

    private IEnumerator ActivateBeing(GameObject being, float delay)
    {
        yield return new WaitForSeconds(delay);
        being.SetActive(true);
    }

    public void PlaceObjects(int objectsToPlace)
    {
        int objectsPlaced = 0;

        foreach (var collider in streetColliders)
        {
            if (objectsPlaced >= objectsToPlace) break;

            Vector3 spawnPosition = GetRandomPointInBox(collider);

            if ((spawnPosition - referencePoint.position).sqrMagnitude < 1f)
                continue;

            logs[objectsPlaced].transform.position = new Vector3(spawnPosition.x, 0.8f, spawnPosition.z);
            logs[objectsPlaced].SetActive(true);
            spawnedObjects.Add(logs[objectsPlaced]);

            objectsPlaced++;
        }

        if (objectsPlaced < objectsToPlace)
        {
            UnityEngine.Debug.LogWarning($"Only placed {objectsPlaced}/{objectsToPlace} objects. Not enough valid positions.");
        }
    }

    // New method to place random items in the world
    public void PlaceRandomItems(int itemsToPlace)
    {
        int itemsPlaced = 0;

        foreach (var collider in streetColliders)
        {
            if (itemsPlaced >= itemsToPlace) break;

            Vector3 spawnPosition = GetRandomPointInBox(collider);

            if ((spawnPosition - referencePoint.position).sqrMagnitude < 1f)
                continue;

            int randomItemIndex = Random.Range(0, items.Length);  // Pick a random item
            GameObject item = items[randomItemIndex];  // Get the corresponding item

            item.transform.position = new Vector3(spawnPosition.x, 2f, spawnPosition.z); // Adjust height if necessary
            item.SetActive(true);
            spawnedObjects.Add(item);

            itemsPlaced++;
        }

        if (itemsPlaced < itemsToPlace)
        {
            UnityEngine.Debug.LogWarning($"Only placed {itemsPlaced}/{itemsToPlace} items. Not enough valid positions.");
        }
    }

    private Vector3 GetRandomPointInBox(BoxCollider box)
    {
        // Define the local variables for x, y, and z
        float x, y, z;

        // Check if boxCollider is null, if so, initialize it
        if (boxCollider == null)
        {
            Vector3 extents = box.size * 0.5f;
            x = Random.Range(-extents.x, extents.x);
            y = Random.Range(-extents.y, extents.y);
            z = Random.Range(-extents.z, extents.z);
            boxCollider = box;
        }
        else
        {
            if (timesUsed == 3)
            {
                timesUsed = 0;
                boxCollider = null;
                // Reset the position after 3 uses and generate a new random point
                Vector3 extents = box.size * 0.5f;
                x = Random.Range(-extents.x, extents.x);
                y = Random.Range(-extents.y, extents.y);
                z = Random.Range(-extents.z, extents.z);
            }
            else
            {
                timesUsed++;
                // Continue generating random points within the same collider
                Vector3 extents = boxCollider.size * 0.5f;
                x = Random.Range(-extents.x, extents.x);
                y = Random.Range(-extents.y, extents.y);
                z = Random.Range(-extents.z, extents.z);
            }
        }
        return box.transform.TransformPoint(box.center + new Vector3(x, y, z));
    }
}