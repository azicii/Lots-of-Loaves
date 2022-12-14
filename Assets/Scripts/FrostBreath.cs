using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class FrostBreath : MonoBehaviour
{
    [SerializeField] ParticleSystem frostVFX;
    [SerializeField] GameObject iceEmblem;
    public List<GameObject> affectedObjects = new();

    void OnTriggerEnter(Collider other)
    {
        Freeze freeze = other.GetComponent<Freeze>();
        if (freeze != null 
            && !other.CompareTag("Ice") 
            && !affectedObjects.Contains(other.gameObject))
        {
            affectedObjects.Add(other.gameObject);
            freeze.isFreezable = true;
            //Debug.Log(affectedObjects.Count);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        Freeze freeze = other.GetComponent<Freeze>();
        if (rb != null && freeze !=null)
        {
            affectedObjects.Remove(other.gameObject);
            freeze.isFreezable = false;
            //Debug.Log(affectedObjects.Count);
        }
    }

    void Update()
    {
        RemoveAffectedObjects();
        if (Input.GetMouseButtonDown(0))
        {
            ShootFrost(affectedObjects);
        }
    }

    public void ShootFrost(List<GameObject> frozenObjects)
    {
        Debug.Log("FREEZE");
        frostVFX.Play();
        foreach (GameObject item in frozenObjects.ToList())
        {
            if (!item.CompareTag("Player"))
            {
                ApplyFreeze(item);
            }
        }
    }

    void ApplyFreeze(GameObject item)
    {
        List<Component> components = new();

        Freeze freeze = item.GetComponent<Freeze>();
        
        //Collect components from gameobject "item"
        //and store in "components" list
        NavMeshAgent navMeshAgent = item.GetComponent<NavMeshAgent>();
        EnemyAI enemyAI = item.GetComponent<EnemyAI>();
        Rigidbody rigidBody = item.GetComponent<Rigidbody>();
        Collider collider = item.GetComponent<Collider>();
        Transform transform = item.GetComponent<Transform>();
        Animator animator = item.GetComponent<Animator>();

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
        if(transform != null)
        {
            components.Add(transform);
        }
        if(animator != null)
        {
            components.Add(animator);
        }

        if (freeze.isFreezable && !freeze.isFrozen)
        {
            freeze.FreezeSignal(components);
        }
        else
        {
            components.Clear();
        }
    }

    void RemoveAffectedObjects()
    {
        foreach (GameObject item in affectedObjects.ToList())
        {
            if (item == null)
            {
                affectedObjects.Remove(item);
            }
        }
    }

    //Enables/disables the associated UI emblem that appears on screen
    void OnEnable()
    {
        iceEmblem.SetActive(true);
    }

    void OnDisable()
    {
        iceEmblem.SetActive(false);
    }
}
