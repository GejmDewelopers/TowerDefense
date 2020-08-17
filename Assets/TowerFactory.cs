using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower tower;
    [SerializeField] Transform parent;

    Queue<Tower> tQueue = new Queue<Tower>();
    public void AddTower(Waypoint baseWaypoint)
    {
        if (tQueue.Count < towerLimit)
        {
            IntantiateNewTower(baseWaypoint);
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }
    }

   

    private void IntantiateNewTower(Waypoint baseWaypoint)
    {
        Tower t = Instantiate(tower, baseWaypoint.transform.position, Quaternion.identity);
        baseWaypoint.isPlaceable = false;
        t.baseWaypoint = baseWaypoint;
        tQueue.Enqueue(t);
        t.transform.parent = parent;
        
    }

    private void MoveExistingTower(Waypoint baseWaypoint)
    {
        var oldTower = tQueue.Dequeue();
        oldTower.baseWaypoint.isPlaceable = true;
        baseWaypoint.isPlaceable = false;
        oldTower.baseWaypoint = baseWaypoint;
        oldTower.transform.position = baseWaypoint.transform.position;

        tQueue.Enqueue(oldTower);
    }
}
