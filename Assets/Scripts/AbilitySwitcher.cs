using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class AbilitySwitcher : MonoBehaviour
{
    [SerializeField] int abilityIndex;

    void Start()
    {
        SetAbilityActive();
    }

    void Update()
    {
        int previousAbility = abilityIndex;

        ProcessKeyInput();
        ProcessScrollWheel();

        if (previousAbility != abilityIndex)
        {
            SetAbilityActive();
        }
    }

    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            abilityIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            abilityIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            abilityIndex = 2;
        }
    }

    private void ProcessScrollWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (abilityIndex >= transform.childCount - 1)
            {
                abilityIndex = 0;
            }
            else
            {
                abilityIndex++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (abilityIndex == 0)
            {
                abilityIndex = transform.childCount - 1;
            }
            else
            {
                abilityIndex--;
            }
        }
    }

    private void SetAbilityActive()
    {
        int currentAbility = 0;

        foreach (Transform ability in transform)
        {
            if (abilityIndex == currentAbility)
            {
                ability.gameObject.SetActive(true);
            }
            else
            {
                ability.gameObject.SetActive(false);
            }
            currentAbility++;
        }
    }
}