using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaundryKey : MonoBehaviour
{
    [SerializeField] GameObject key;
    [SerializeField] float rotationSpeed = 0.5f;
    [SerializeField] float explosionForce = 500f;
    [SerializeField] float explosionRadius = 10f;
    [SerializeField] float upForce = 5f;
    [SerializeField] GameObject explosion;

    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            key.gameObject.SetActive(true);
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider item in colliders)
        {
            if (item.gameObject.CompareTag("Player"))
            {
                return;
            }

            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upForce);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
