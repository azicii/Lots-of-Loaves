using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayAbility : MonoBehaviour
{
    [SerializeField] Canvas abilityCanvas;

    void Start()
    {
        abilityCanvas.enabled = true;
    }

    void Update()
    {
        EnableCanvas();
    }

    void EnableCanvas()
    {
        this.enabled = true;
    }

}
