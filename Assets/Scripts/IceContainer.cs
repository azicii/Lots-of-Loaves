using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceContainer : MonoBehaviour
{
    bool isFrozen = false;

    void Start()
    {
        EnableIceCube(isFrozen);
    }

    void Update()
    {
        IgnoreColliders();
    }

    void IgnoreColliders()
    {
        if (this.GetComponent<Collider>() != null)
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(),
                                GetComponentInParent<Collider>());
        }
        else { return; }
    }

    public void EnableIceCube(bool isFrozen)
    {
        GetComponent<MeshCollider>().enabled = isFrozen;
        GetComponent<Rigidbody>().isKinematic = !isFrozen;
        GetComponent<MeshRenderer>().enabled = isFrozen;
    }
}
