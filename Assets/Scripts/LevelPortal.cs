using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPortal : MonoBehaviour
{
    [SerializeField] int level;
    public int requiredItemNumber = 5;

    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<Player>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player.itemNumber == requiredItemNumber)
            {
                SceneManager.LoadScene(level);
                Debug.Log($"Welcome to Level {level + 1}");
            }
            else
            {
                Debug.Log($"Need {requiredItemNumber - player.itemNumber} more items!");
            }
        }
    }
}
