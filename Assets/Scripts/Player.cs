using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    RigidbodyFirstPersonController controller;
    Collider _collider;
    public bool isFlying;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<RigidbodyFirstPersonController>();
        _collider = GetComponent<Collider>();    
        
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

   
}