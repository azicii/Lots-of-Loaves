using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class FireBreath : MonoBehaviour
{
    [SerializeField] GameObject fireVFX;
    List<GameObject> affectedObjects = new();

    void OnTriggerEnter(Collider other)
    {
        Flammable fire = other.GetComponent<Flammable>();
        if (fire != null
            && !affectedObjects.Contains(other.gameObject))
        {
            affectedObjects.Add(other.gameObject);
            fire.isFlammable = true;
            Debug.Log(affectedObjects.Count);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Flammable fire = other.GetComponent<Flammable>();
        if (fire != null)
        {
            affectedObjects.Remove(other.gameObject);
            fire.isFlammable = false;
            //Debug.Log(affectedObjects.Count);
        }
    }

    void Update()
    {
        RemoveAffectedObjects();
        if (Input.GetMouseButtonDown(0))
        {
            ShootFlames(affectedObjects);
        }
    }

    public void ShootFlames(List<GameObject> enflamedObjects)
    {
        Debug.Log("BURNNN");
        foreach (ParticleSystem fireFX in fireVFX.GetComponentsInChildren<ParticleSystem>())
        {
            fireFX.Play();
        }

        foreach (GameObject item in enflamedObjects.ToList())
        {
            //if the object to burn is not a "trigger" or the playable character. Trigger 
            //collider is active when the object gets frozen. This prevents frozen objects
            //from getting burned and instead become unfrozen. 
            if (!item.CompareTag("Player"))
            {
                if (item.CompareTag("Enemy") && !item.GetComponent<Collider>().isTrigger)
                {
                    Debug.Log($"{item.name} slain");
                    affectedObjects.Remove(item);
                    Destroy(item);
                }
                if (item.CompareTag("Flammable"))
                {
                    ApplyFlames(item);
                }
                if (item.CompareTag("Ice"))
                {
                    //eventually need to come back and fix this. right now you can "unfreeze" things with firebreath but
                    //they will still be kinematic triggers
                    //item.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
                    item.transform.GetChild(0).parent = null;

                    Destroy(item);

                }
            }
        }
    }
    void ApplyFlames(GameObject item)
    {
        List<Component> components = new();

        Flammable fire = item.GetComponent<Flammable>();

        //Collect components from gameobject "item"
        //and store in "components" list
        NavMeshAgent navMeshAgent = item.GetComponent<NavMeshAgent>();
        EnemyAI enemyAI = item.GetComponent<EnemyAI>();
        Rigidbody rigidBody = item.GetComponent<Rigidbody>();
        Collider collider = item.GetComponent<Collider>();
        Transform transform = item.GetComponent<Transform>();

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
        if (transform != null)
        {
            components.Add(transform);
        }

        if (fire.isFlammable && !fire.isEnflamed)
        {
            fire.FireSignal(components);
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
}
