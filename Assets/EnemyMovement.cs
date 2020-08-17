using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] int pkt_zycia = 5;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathParticlePrefab;
    [Range(0.1f, 6f)]
    [SerializeField] float timeBetweenTwoWaypoints = 1f;
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeathSFX;
    GameObject parent;
    EnemyObject enemyObject;
    AudioSource myAudioSource;
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        enemyObject = FindObjectOfType<EnemyObject>();
        AddNonTriggerBoxCollider();
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }
    
    private void AddNonTriggerBoxCollider()
    {
        Collider c = enemyObject.gameObject.AddComponent<BoxCollider>();
        c.isTrigger = false;
    }

    public void CollisionDetected(EnemyObject enemyObject)
    {
        if (pkt_zycia <= 0)
        {
            KillEnemy();
        }
        else
        {
            pkt_zycia--;
            hitParticlePrefab.Play();
            myAudioSource.PlayOneShot(enemyHitSFX);
        }
    }

    private void KillEnemy()
    {
        ParticleSystem fx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        fx.Play();
        float deathDelay = fx.main.duration;
        parent = GameObject.Find("SpawnedAtRuntime");
        fx.transform.parent = parent.transform;
        Destroy(fx.gameObject, deathDelay);
        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        
        foreach (Waypoint waypoint in path)
        {
            Vector3 delta = waypoint.transform.position - transform.position;
            float timePassed = 0f;
            while(timePassed <= timeBetweenTwoWaypoints)
            {
                timePassed += Time.deltaTime;
                transform.position += delta * Time.deltaTime * (1/timeBetweenTwoWaypoints);
                yield return null;
            }
            transform.position = waypoint.transform.position;
        }
        KillEnemy();
    }
}
