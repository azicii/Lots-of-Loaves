using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Freeze : MonoBehaviour
{
    [SerializeField] float timeWhileFrozen = 2f;

    public void FreezeGameObject()
    {
        if (TryGetComponent(out NavMeshAgent navMeshAgent))
        {
            navMeshAgent.isStopped = true;
        }

        if (TryGetComponent(out EnemyAI enemyAI))
        {
            enemyAI.enabled = false;
        }

        if (TryGetComponent(out Rigidbody rigidBody))
        {
            rigidBody.freezeRotation = true;
            rigidBody.velocity = Vector3.zero;
        }
    }

}
