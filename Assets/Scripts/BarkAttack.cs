using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BarkAttack : MonoBehaviour
{
    [SerializeField] float barkForce = 700f;
    [SerializeField] float explosionRadius = 15f;
    [SerializeField] float upwardsForce = 2f;
    [SerializeField] ParticleSystem forceSmoke;

    List<GameObject> affectedObjects = new();

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
        foreach (GameObject item in affectedObjects.ToList())
        {
            if (!item.CompareTag("Player"))
            {
                if (item.CompareTag("Enemy"))
                {
                    item.GetComponent<Death>().StopMovement();
                    ApplyBarkForce(item);
                }

                if (item.CompareTag("Brick"))
                {
                    item.GetComponent<Rigidbody>().isKinematic = false;
                }
                ApplyBarkForce(item);
            }
        }
    }

    void ApplyBarkForce(GameObject item)
    {
        Rigidbody rb = item.GetComponent<Rigidbody>();
        var forceModifier = item.GetComponent<ForceModifier>();

        //Check to see if item in affectedObjects has a forceModifier script attached, if not just just default barkForce
        float forwardForce = forceModifier != null ? barkForce * forceModifier.forwardMod : barkForce;
        float upForce = forceModifier != null ? upwardsForce * forceModifier.upwardsMod : upwardsForce;

        rb.AddExplosionForce(
            forwardForce,
            transform.position,
            explosionRadius,
            upForce
                            );
    }
}
