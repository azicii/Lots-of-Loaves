using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Tutorial : MonoBehaviour
{
    TutorialScript currentTutorial;

    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI _tutorialText;

    //triggering the tutorialscript colliders will enable the canvas to show
    //and display the tutorial provided by the triggers. Tutorial display
    //turns off after set number of seconds serialized above.
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TutorialScript>() != null) 
        {
            currentTutorial = other.GetComponent<TutorialScript>();

            StartCoroutine(DisplayTutorial(currentTutorial));
        }
    }

    IEnumerator DisplayTutorial(TutorialScript script)
    {
        canvas.gameObject.SetActive(true);
        _tutorialText.text = script.tutorialText;

        yield return new WaitForSeconds(script.tutorialTime);

        canvas.gameObject.SetActive(false);
    }
}
