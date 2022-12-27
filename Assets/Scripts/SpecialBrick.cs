using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBrick : MonoBehaviour
{
    Event eventScript;
    Rigidbody rb;

    void Start()
    {
        eventScript = GetComponent<Event>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!rb.isKinematic)
        {
            eventScript.isTriggered = true;
        }
    }
}
