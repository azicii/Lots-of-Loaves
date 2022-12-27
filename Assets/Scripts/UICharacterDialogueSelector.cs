using Narrate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterDialogueSelector : MonoBehaviour
{
    [SerializeField] GameObject subtitlesGameObject;
    [SerializeField] GameObject character;

    [SerializeField] bool insideDistanceArea;
    
    //in order for this script to function correctly, need to make sure 
    //that a collider is attached to this gameobject surrounding the character. The 
    //collider determines whether to activate the character UI model in the narrationsystem. 
    //The "subtitles" gameobject under the narration system and the respective character
    //this gameobject references which is also a child of the narration system should also be serialized 
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

