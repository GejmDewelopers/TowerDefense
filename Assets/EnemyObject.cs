using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        transform.parent.GetComponent<EnemyMovement>().CollisionDetected(this);
    }
}
