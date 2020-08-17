using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] float attackRange = 30f;
    [SerializeField] ParticleSystem particle;

    public Waypoint baseWaypoint;

    Transform targetEnemy;

    void Update()
    {
        setTargetEnemy();
        if (targetEnemy)
        {
            float distance = Vector3.Distance(objectToPan.transform.position, targetEnemy.transform.position);
            var emisja = particle.emission;
            if (distance <= attackRange)
            {
                objectToPan.LookAt(targetEnemy);
                emisja.enabled = true;
            }
            else
            {
                emisja.enabled = false;
            }
        }
    }

    private void setTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyMovement>();
        if(sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;
        foreach(EnemyMovement testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }
        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform closestEnemy, Transform testEnemy)
    {
        float distanceToClosest = Vector3.Distance(transform.position, closestEnemy.transform.position);
        float distanceToTest = Vector3.Distance(transform.position, testEnemy.transform.position);
        if (distanceToClosest <= distanceToTest) return closestEnemy;
        return testEnemy;
    }
}
