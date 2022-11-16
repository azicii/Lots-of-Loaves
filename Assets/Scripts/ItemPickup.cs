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

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
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
            hoorayVFX.Play();
            GetComponentInChildren<Light>().enabled = false;
            GetComponent<Renderer>().enabled = false;
            GetComponent<MeshCollider>().enabled = false;
            AddItem();
            Destroy(this.gameObject, 5);
        }
    }
    
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
