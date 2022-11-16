using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] GameObject x;
    [SerializeField] GameObject numberOfItems;

    [SerializeField] ParticleSystem hoorayVFX;
    [SerializeField] float rotationSpeed = 0.5f;

    Player player;
    bool aqcuired = false;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (!aqcuired)
        {
            transform.Rotate(0, rotationSpeed, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (item.activeSelf == false && x.activeSelf == false)
            {
                item.SetActive(true);
                x.SetActive(true);
            }
            aqcuired = true;
            hoorayVFX.Play();
            DisableItem();
            AddItem();
            Destroy(this.gameObject, 5);
        }
    }

    //prevents the player from accessing the item after being aqcuired
    private void DisableItem()
    {
        GetComponentInChildren<Light>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
    }

    //adds the item to the "player inventory". Updates item canvas. 
    void AddItem()
    {
        player.itemNumber++;

        if (numberOfItems.activeSelf == false)
        {
            numberOfItems.SetActive(true); ;
        }

        player.numberOfItems.text = player.itemNumber.ToString();
    }
}
