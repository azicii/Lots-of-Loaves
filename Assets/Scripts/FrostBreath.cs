using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostBreath : MonoBehaviour
{
    [SerializeField] ParticleSystem frostVFX;

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
            ShootFrost();
        }
    }

    void ShootFrost()
    {
        Debug.Log("FREEZE");
        frostVFX.Play();
        foreach (GameObject item in affectedObjects)
        {
            if (!item.CompareTag("Player"))
            {
                 if (item.GetComponent<Freeze>() != null)
                 {
                    item.GetComponent<Freeze>().FreezeGameObject();
                 }
            }
        }
    }
}
