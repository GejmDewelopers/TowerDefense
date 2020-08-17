using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 20f)]
    [SerializeField] float secondsBetweenSpawns = 4f;
    [SerializeField] EnemyMovement enemy;
    [SerializeField] AudioClip spawnedEnemySFX;
    //[SerializeField] Text spawnedEnemies;
    public static int numOfEnemiesSpawned = 0;

    private void Start()
    {
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        for (int i = 0; i < 10; i++)
        {
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemySFX);
            numOfEnemiesSpawned++;
            EnemyMovement e = Instantiate(enemy, transform.position, Quaternion.identity);
            e.transform.parent = this.transform;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

}
