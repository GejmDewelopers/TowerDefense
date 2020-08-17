using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int healthPoints = 10;
    [SerializeField] int healthDecrease = 1;
    [SerializeField] Text healthText;
    [SerializeField] Text numOfEnemiesSpawned;
    [SerializeField] AudioClip baseHitSFX;

    private void Start()
    {
        healthText.text = healthPoints.ToString();
        numOfEnemiesSpawned.text = EnemySpawner.numOfEnemiesSpawned.ToString();
    }

    private void Update()
    {
        numOfEnemiesSpawned.text = EnemySpawner.numOfEnemiesSpawned.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().PlayOneShot(baseHitSFX);
        healthPoints = healthPoints - healthDecrease;
        healthText.text = healthPoints.ToString();
    }
}
