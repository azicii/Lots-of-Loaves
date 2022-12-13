using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    //text that displays on the tutorial text attached to the canvas
    //gameobject on moose rig
    public string tutorialText = "Use WASD to move around!";
    public float tutorialTime = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, tutorialTime);
        }
    }
}
