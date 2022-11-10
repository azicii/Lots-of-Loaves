using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using Component = UnityEngine.Component;

public class Freeze : MonoBehaviour
{
    [SerializeField] GameObject iceContainer;

    public float timeWhileFrozen = 2f;
    IceContainer iceScript;

    void Start()
    {
        iceScript = iceContainer.GetComponent<IceContainer>();
    }

    public void FreezeSignal(float timeFrozen)
    {
        StartCoroutine(FreezeGameObject(timeFrozen));
    }

    IEnumerator FreezeGameObject(float timeFrozen)
    {
        List<Component> components = new();

        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        EnemyAI enemyAI = GetComponent<EnemyAI>();
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        Collider collider = GetComponent<Collider>();

        if (navMeshAgent != null)
        {
            components.Add(navMeshAgent);
        }
        if (enemyAI != null)
        {
            components.Add(enemyAI);
        }
        if (rigidBody != null)
        {
            components.Add(rigidBody);
        }
        if (collider != null)
        {
            components.Add(collider);
        }

        Debug.Log(components.Count);
        StopAllMovement(components);
        iceScript.EnableIceCube(true);

        yield return new WaitForSeconds(timeFrozen);

        ResumeAllMovement(components);
        iceScript.EnableIceCube(false);
    }

    void StopAllMovement(List<Component> components)
    {
        Debug.Log($"{gameObject.name} is frozen");
        foreach (Component component in components)
        {
            if (component.GetType().Name == "NavMeshAgent")
            {
                var nMA = component as NavMeshAgent;
                nMA.isStopped = true;
            }
            if (component.GetType().Name == "EnemyAI")
            {
                var eAI = component as EnemyAI;
                eAI.enabled = false;
            }
            if (component.GetType().Name == "RigidBody")
            {
                var rb = component as Rigidbody;
                rb.freezeRotation = true;
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
            }
            if (component.GetType().Name == "BoxCollider")
            {
                var boxCollider = component as Collider;
                boxCollider.enabled = false;
            }

            Debug.Log($"{component.name} has a {component.GetType().Name}");
        }
    }

    void ResumeAllMovement(List<Component> components)
    {
        Debug.Log($"{gameObject.name} is unfrozen");
        foreach (Component component in components)
        {
            if (component.GetType().Name == "NavMeshAgent")
            {
                var nMA = component as NavMeshAgent;
                nMA.isStopped = false;
            }
            if (component.GetType().Name == "EnemyAI")
            {
                var eAI = component as EnemyAI;
                eAI.enabled = true;
            }
            if (component.GetType().Name == "RigidBody")
            {
                var rb = component as Rigidbody;
                rb.freezeRotation = false;
                rb.isKinematic = false;
                rb.velocity = new Vector3(1, 1, 1);
            }
            if (component.GetType().Name == "Box Collider")
            {
                var boxCollider = component as Collider;
                boxCollider.enabled = true;
            }

            Debug.Log($"{component.name} has a {component.GetType().Name}");
        }
    }
}
