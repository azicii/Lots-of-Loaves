using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    [SerializeField] GameObject eventObjects;

    public bool isTriggered;

    void Start()
    {
        isTriggered = false;
    }

    private void Update()
    {
        if (isTriggered)
        {
            BeginEvent();
        }
    }

    void BeginEvent()
    {
        eventObjects.SetActive(true);
    }
}
