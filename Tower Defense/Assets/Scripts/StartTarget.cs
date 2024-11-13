using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTarget : MonoBehaviour
{
    [SerializeField] public ParticleSystem ps;
    Transform target;
    void Update()
    {
        FindClosestTarget();
    }
    void FindClosestTarget()
    {
        StartMovement[] enemies = FindObjectsOfType<StartMovement>();
        
        if (enemies.Length > 0)
        {
            var emissionModule = ps.emission;
            emissionModule.enabled = true;
            Transform closestTarget = null;
            float maxDistance = Mathf.Infinity;

            foreach (StartMovement enemy in enemies)
            {
                float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

                if (targetDistance < maxDistance)
                {
                    closestTarget = enemy.transform;
                    maxDistance = targetDistance;
                }
            }
            target = closestTarget;
            transform.LookAt(target);
        }
        else
        {
            var emissionModule = ps.emission;
            emissionModule.enabled = false;
        }
    }
}