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
    [SerializeField] float timeWhileFrozen = 2f;

    public bool isFreezable;
    public bool isFrozen;

    public void FreezeSignal(List<Component> components)
    {
        StartCoroutine(FreezeGameObject(components));
    }

    IEnumerator FreezeGameObject(List<Component> components)
    {
        //Debug.Log(components.Count);
        HandleFreeze(components, true);
        isFrozen = true;

        yield return new WaitForSeconds(timeWhileFrozen);

        HandleFreeze(components, false);
        isFrozen = false;
    }

    void HandleFreeze(List<Component> components, bool isFrozen)
    {
        foreach (Component component in components)
        {
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

            if (component.gameObject.GetComponent<EnemyAI>() != null)
            {
                if (!component.gameObject.GetComponent<EnemyAI>().isDead)
                {
                    if (component is NavMeshAgent)
                    {
                        var nMA = component as NavMeshAgent;
                        nMA.enabled = !isFrozen;
                    }
                    if (component is EnemyAI)
                    {
                        var eAI = component as EnemyAI;
                        eAI.enabled = !isFrozen;
                    }
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

        GameObject myIceContainer = Instantiate(iceContainer, icePosition, Quaternion.identity);
        this.gameObject.transform.parent = myIceContainer.transform;

        yield return new WaitForSeconds(timeWhileFrozen);

        this.gameObject.transform.parent = null;
        Destroy(myIceContainer);
    }
}
