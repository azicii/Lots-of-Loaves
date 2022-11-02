using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarkAttack : MonoBehaviour
{
    [SerializeField] Transform nose;
    [SerializeField] float barkRange = 100f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootBark();
        }
    }

    void ShootBark()
    {
        RaycastHit hit;
        Physics.Raycast(nose.position, nose.forward, out hit, barkRange);
        Debug.Log($"Moose has barked at: {hit.transform.name}");
    }
}
