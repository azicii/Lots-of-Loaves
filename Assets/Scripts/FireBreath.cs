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
            if (!item.CompareTag("Player"))
            {
                if (item.CompareTag("Enemy"))
                {
                    Debug.Log("Enemy slain");
                    affectedObjects.Remove(item);
                    Destroy(item);
                }
                else if (item.CompareTag("Flammable"))
                {
                    ApplyFlames(item);
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
}
