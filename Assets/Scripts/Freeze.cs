using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Freeze : MonoBehaviour
{
    public float timeWhileFrozen = 2f;

    public void FreezeSignal(float timeFrozen)
    {
        StartCoroutine(FreezeGameObject(timeFrozen));
    }

    IEnumerator FreezeGameObject(float timeFrozen)
    {
        if (TryGetComponent(out NavMeshAgent navMeshAgent) &&
            TryGetComponent(out EnemyAI enemyAI) &&
            TryGetComponent(out Rigidbody rigidBody))
        {
            StopAllMovement(navMeshAgent, enemyAI, rigidBody);
            RenderIceCube(true);

            yield return new WaitForSeconds(timeFrozen);

            ResumeAllMovement(navMeshAgent, enemyAI, rigidBody);
            RenderIceCube(false);
        }
    }

    void StopAllMovement(NavMeshAgent navMeshAgent, EnemyAI enemyAI, Rigidbody rigidBody)
    {
        Debug.Log($"{gameObject.name} is frozen");
        navMeshAgent.isStopped = true;
        enemyAI.enabled = false;
        rigidBody.freezeRotation = true;
        rigidBody.velocity = Vector3.zero;
    }

    void ResumeAllMovement(NavMeshAgent navMeshAgent, EnemyAI enemyAI, Rigidbody rigidBody)
    {
        Debug.Log($"{gameObject.name} is unfrozen");
        navMeshAgent.isStopped = false;
        enemyAI.enabled = true;
        rigidBody.freezeRotation = false;
        rigidBody.velocity = new Vector3(1, 1, 1);
    }

    void RenderIceCube(bool isFrozen)
    {



    }
}
