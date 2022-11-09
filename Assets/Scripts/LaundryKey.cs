using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaundryKey : MonoBehaviour
{
    [SerializeField] GameObject key;
    [SerializeField] float rotationSpeed = 0.5f;
    [SerializeField] float explosionForce = 500f;
    [SerializeField] float explosionRadius = 10f;
    [SerializeField] GameObject explosion;


    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            key.SetActive(true);
            Explosion();
            GetComponent<Renderer>().enabled = false;
            GetComponent<MeshCollider>().enabled = false;
        }
    }

    void Explosion()
    {
        Debug.Log("BOOM!");
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
