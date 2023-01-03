using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MenuTracker : MonoBehaviour
{
    [SerializeField] GameObject musicPlayer;
    [SerializeField] GameObject ambientSoundPlayer;

    void Start()
    {
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        musicPlayer.SetActive(false);
        ambientSoundPlayer.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    //void OnLevelWasLoaded(int level)
    //{
    //    if (level == 0)
    //    {
    //        Time.timeScale = 1;
    //        this.gameObject.SetActive(false);
    //    }
    //}
}