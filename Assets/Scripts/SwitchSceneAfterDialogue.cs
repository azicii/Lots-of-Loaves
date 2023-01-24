using Narrate;
using System.Collections;
using UnityEngine;

public class SwitchSceneAfterDialogue : MonoBehaviour
{
    [SerializeField] int sceneNumber;
    [SerializeField] int timeUntilSceneSwitch = 3;

    void OnEnable()
    {
        StartCoroutine(SwitchScene());
    }

    IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(timeUntilSceneSwitch);

        FindObjectOfType<SceneSwitcher>().SwitchScene(sceneNumber);
    }
}
