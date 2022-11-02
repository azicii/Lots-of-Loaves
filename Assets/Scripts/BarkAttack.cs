using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarkAttack : MonoBehaviour
{
    [SerializeField] float barkForce = 700f;
    [SerializeField] float explosionRadius = 15f;
    [SerializeField] float upwardsForce = 2f;
    [SerializeField] ParticleSystem forceSmoke;

    List<GameObject> affectedObjects = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>(); 
        if (rb != null)
        {
            affectedObjects.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            affectedObjects.Remove(other.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootBark();
        }
    }



    void ShootBark()
    {
        Debug.Log("BARK");
        forceSmoke.Play();
        foreach (GameObject item in affectedObjects)
        {
            if (!item.CompareTag("Player"))
            {
                Rigidbody rb = item.GetComponent<Rigidbody>();
                rb.AddExplosionForce(
                    barkForce,
                    transform.position,
                    explosionRadius,
                    upwardsForce
                                    );
            }
        }
    }
}
