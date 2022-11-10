using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using Component = UnityEngine.Component;

public class Freeze : MonoBehaviour
{
    [SerializeField] GameObject iceContainer;
    IceContainer iceScript;

    public float timeWhileFrozen = 2f;
    public bool isFreezable;

    void Start()
    {
        iceScript = iceContainer.GetComponent<IceContainer>();
    }

    public void FreezeSignal(float timeFrozen, List<Component> components)
    {
        StartCoroutine(FreezeGameObject(timeFrozen, components));
    }

    IEnumerator FreezeGameObject(float timeFrozen, List<Component> components)
    {
        //Debug.Log(components.Count);
        
        StopAllMovement(components);
        yield return new WaitForSeconds(timeFrozen);
        ResumeAllMovement(components);
    }

    void StopAllMovement(List<Component> components)
    {
        iceScript.EnableIceCube(true);
        foreach (Component component in components)
        {
            if (component is NavMeshAgent)
            {
                var nMA = component as NavMeshAgent;
                nMA.isStopped = true;
            }
            if (component is EnemyAI)
            {
                var eAI = component as EnemyAI;
                eAI.enabled = false;
            }
            if (component is Rigidbody)
            {
                var rb = component as Rigidbody;
                rb.freezeRotation = true;
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
            }
            if (component is Collider)
            {
                var collider = component as Collider;
                collider.isTrigger = true;
            }
            //Debug.Log($"{component.name} has a {component.GetType().Name}");
        }
    }

    void ResumeAllMovement(List<Component> components)
    {
        iceScript.EnableIceCube(false);
        foreach (Component component in components)
        {
            if (component is NavMeshAgent)
            {
                var nMA = component as NavMeshAgent;
                nMA.isStopped = false;
            }
            if (component is EnemyAI)
            {
                var eAI = component as EnemyAI;
                eAI.enabled = true;
            }
            if (component is Rigidbody)
            {
                var rb = component as Rigidbody;
                rb.freezeRotation = false;
                rb.isKinematic = false;
                rb.velocity = new Vector3(1, 1, 1);
            }
            if (component is Collider)
            {
                var collider = component as Collider;
                collider.isTrigger = false;
            }
            //Debug.Log($"{component.name} has a {component.GetType().Name}");
        }
    }
}
