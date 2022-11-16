using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickup : MonoBehaviour
{
    [SerializeField] GameObject ability;
    [SerializeField] float rotationSpeed = 0.5f;
    [SerializeField] float explosionForce = 500f;
    [SerializeField] float explosionRadius = 10f;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject moose;

    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }
    //When the player makes contact with the pickup, they will access the 
    //abilities array in the abilityswitcher script attached to the player.
    //The abilities array is replaced with a new version of itself that has 
    //an additional element. The additional element will be the 'ability' serialized above.
    //the player will also create an explosion and destroy this pickup in 5 seconds. 

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            moose.GetComponent<AbilitySwitcher>().AddNewAbility(ability);
            Explosion();
            GetComponentInChildren<Light>().enabled = false;
            GetComponent<Renderer>().enabled = false;
            GetComponent<MeshCollider>().enabled = false;
            Destroy(this.gameObject, 5);
        }
    }

    void Explosion()
    {
        Debug.Log("AbilityAqcuired!");
        ParticleSystem[] particleSystems = explosion.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem explosion in particleSystems)
        {
            explosion.Play();
        }

        Collider[] colliders = Physics.OverlapSphere(this.gameObject.transform.position, explosionRadius);
        foreach (Collider item in colliders)
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb != null && !item.gameObject.CompareTag("Player"))
            {
                rb.isKinematic = false;
                rb.velocity = Vector3.zero;
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
