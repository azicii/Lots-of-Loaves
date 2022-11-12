using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.AI;

public class Death : MonoBehaviour
{
    public bool isDead;

    public void StopMovement()
    {
        isDead = true;
        Debug.Log($"{this.gameObject.name} was slain");
        if (GetComponent<NavMeshAgent>() != null) 
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }
        if (GetComponent<EnemyAI>() != null)
        {
           GetComponent<EnemyAI>().enabled = false;
        }
    }
}
