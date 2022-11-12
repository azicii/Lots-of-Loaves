using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using Component = UnityEngine.Component;

public class Flammable : MonoBehaviour
{
    public bool isFlammable;
    public bool isEnflamed;

    public void FireSignal(List<Component> components)
    {
        EnflameGameObject(components);
    }

    void EnflameGameObject(List<Component> components)
    {
        //Debug.Log(components.Count);
        HandleFlames(components, true);
        isEnflamed = true;
    }

    void HandleFlames(List<Component> components, bool isOnFire)
    {
        foreach (Component component in components)
        {

            if (component is NavMeshAgent) 
            {
                var nMA = component as NavMeshAgent;
            }
            if (component is EnemyAI)
            {
                var eAI = component as EnemyAI;
            }
            if (component is Rigidbody)
            {
                var rb = component as Rigidbody;
            }
            if (component is Collider)
            {
                var collider = component as Collider;
            }
            if (component is Transform)
            {
                var tf = component as Transform;
            }
            //Debug.Log($"{component.name} has a {component.GetType().Name}");
        }
    }
}
