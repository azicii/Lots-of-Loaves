using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Tutorial : MonoBehaviour
{
    TutorialScript currentTutorial;

    [SerializeField] Collider movementTutorial;
    [SerializeField] Canvas canvas;
    public float tutorialDisplayTime = 3f;
    [SerializeField] TextMeshProUGUI _tutorialText;

    //triggering the tutorialscript colliders will enable the canvas to show
    //and display the tutorial provided by the triggers. Tutorial display
    //turns off after set number of seconds serialized above.
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TutorialScript>() != null) 
        {
            currentTutorial = other.GetComponent<TutorialScript>();

            StartCoroutine(DisplayTutorial());
        }
    }

    IEnumerator DisplayTutorial()
    {
        canvas.gameObject.SetActive(true);
        _tutorialText.text = currentTutorial.tutorialText;

        yield return new WaitForSeconds(tutorialDisplayTime);

        canvas.gameObject.SetActive(false);
    }
}
