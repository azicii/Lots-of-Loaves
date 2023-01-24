using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    [SerializeField] GameObject eventObjects;
    [SerializeField] int timeUntilEventBegin = 2;

    public bool isTriggered;

    void Start()
    {
        isTriggered = false;
    }

    private void Update()
    {
        if (isTriggered)
        {
            StartCoroutine(BeginEvent());
        }
    }

    IEnumerator BeginEvent()
    {
        yield return new WaitForSeconds(timeUntilEventBegin);

        eventObjects.SetActive(true);
    }
}
