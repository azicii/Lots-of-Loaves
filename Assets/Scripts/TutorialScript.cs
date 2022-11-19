using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    //text that displays on the tutorial text attached to the canvas
    //gameobject on moose rig
    public string tutorialText = "Use WASD to move around!";
    Tutorial tutorial;

    void Start()
    {
        tutorial = FindObjectOfType<Tutorial>().GetComponent<Tutorial>();
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject, tutorial.tutorialDisplayTime);
    }
}
