using Narrate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterDialogueSelector : MonoBehaviour
{
    [SerializeField] GameObject subtitlesGameObject;
    [SerializeField] GameObject character;

    public bool insideDistanceArea;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            insideDistanceArea = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            insideDistanceArea = false;
        }
    }

    void Update()
    {
        ActivateCharacterUI();
    }

    void ActivateCharacterUI()
    {
        if (subtitlesGameObject.activeSelf && insideDistanceArea == true)
        {
            character.SetActive(true);
        }
        else
        {
            character.SetActive(false);
        }
    }
}

