using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using Component = UnityEngine.Component;

public class Freeze : MonoBehaviour
{
    [SerializeField] GameObject iceContainer;
    [SerializeField] float icePositionOffset = 1f;
    IceContainer iceScript;

    public float timeWhileFrozen = 2f;
    public bool isFreezable;
    public bool isFrozen;

    void Start()
    {
        iceScript = iceContainer.GetComponent<IceContainer>();
    }

    public void FreezeSignal(List<Component> components)
    {
        StartCoroutine(FreezeGameObject(components));
    }

    IEnumerator FreezeGameObject(List<Component> components)
    {
        //Debug.Log(components.Count);
        RestrictMovement(components, true);
        isFrozen = true;
        yield return new WaitForSeconds(timeWhileFrozen);
        RestrictMovement(components, false);
        isFrozen = false;
    }

    void RestrictMovement(List<Component> components, bool isFrozen)
    {
        //iceScript.EnableIceCube(true);
        foreach (Component component in components)
        {
            if (component is NavMeshAgent)
            {
                var nMA = component as NavMeshAgent;
                nMA.isStopped = isFrozen;
            }
            if (component is EnemyAI)
            {
                var eAI = component as EnemyAI;
                eAI.enabled = !isFrozen;
            }
            if (component is Rigidbody)
            {
                var rb = component as Rigidbody;
                rb.freezeRotation = isFrozen;
                rb.isKinematic = isFrozen;
                rb.velocity = Vector3.zero;
            }
            if (component is Collider)
            {
                var collider = component as Collider;
                collider.isTrigger = isFrozen;
            }
            if (component is Transform)
            {
                var tf = component as Transform; 
                if (isFrozen)
                {
                    StartCoroutine(GenerateIceContainer(tf));
                }
            }
            //Debug.Log($"{component.name} has a {component.GetType().Name}");
        }
    }

    IEnumerator GenerateIceContainer(Transform transform)
    {
        Vector3 icePosition = new Vector3(transform.position.x,
                                          transform.position.y - icePositionOffset, 
                                          transform.position.z);

        GameObject myIceContainer = Instantiate(iceContainer, icePosition, transform.rotation);
        this.gameObject.transform.parent = myIceContainer.transform;

        yield return new WaitForSeconds(timeWhileFrozen);

        this.gameObject.transform.parent = null;
        Destroy(myIceContainer);
    }
}
