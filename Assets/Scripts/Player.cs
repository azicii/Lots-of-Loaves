using System.Collections;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    //The canvas element that displays the number of currently held items
    //Used in ItemPickup.cs
    public TextMeshProUGUI numberOfItems;

    Collider _collider;
    Rigidbody rb;
    RigidbodyFirstPersonController controller;
    public bool isFlying;

    //The number of items that the player currently has.
    //Used in ItemPickup.cs
    public int itemNumber;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<RigidbodyFirstPersonController>();
        _collider = GetComponent<Collider>();
        itemNumber = 0;
    }

    public void TakeDamage(Vector3 impactForce)
    {
        StartCoroutine(ApplyForce(impactForce));
    }

    IEnumerator ApplyForce(Vector3 impactForce)
    {
        //whenever enemies attack the player, this function applies a force to the 
        //rigidbody connected to the player. 
        //This function also disables the first eprson controller script until the time when 
        //the rigidbody of the player stops moving. 
        controller.enabled = false;
        rb.AddForce(impactForce, ForceMode.Impulse);
        isFlying = true;

        yield return new WaitForSeconds(0.1f);
        //yield return new WaitUntil(() => rb.velocity == Vector3.zero);

        controller.enabled = true;

        if (rb.velocity.y == 0)
        {
            isFlying = false;
        }
    }

    //use this code below whenever moose is bumping into some unknown object that cannot be seen in the scene or game view in the inspector. 
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"moose hit {collision.collider.name}");
    }
}
