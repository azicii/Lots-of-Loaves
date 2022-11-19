using Narrate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterDialogueSelector : MonoBehaviour
{
    [SerializeField] GameObject subtitlesGameObject;
    [SerializeField] GameObject character;

    void Update()
    {
        if (subtitlesGameObject.activeSelf)
        {
            character.SetActive(true);
        }
        else
        {
            character.SetActive(false);
        }
    }

}
