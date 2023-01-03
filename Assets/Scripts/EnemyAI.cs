using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 1f;
    [SerializeField] float attackForce = 50f;
    [SerializeField] float upwardsAttackForce = 5f;

    Animator animator;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    public bool isDead = false;
    Vector3 direction;
    Collider player;
    Collider _collider;
    ParticleSystem dizzyVFX;
    AudioSource audioSource;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = FindObjectOfType<Player>().GetComponent<Collider>();
        _collider = GetComponent<Collider>();
        dizzyVFX = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Physics.IgnoreCollision(_collider, player);
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
    }

    void EngageTarget()
    {
        if (distanceToTarget > navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            FaceTarget();
            AttackTarget();
        }

        if (distanceToTarget > chaseRange)
        {
            StopChasing();   
        }
    }

    void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);

        animator.SetBool("isProvoked", true);
        animator.SetBool("isAttacking", false);
    }

    void StopChasing()
    {
        navMeshAgent.isStopped = true;
        animator.SetBool("isProvoked", false);
    }

    void AttackTarget()
    {
        animator.SetBool("isAttacking", true);
    }

    //This method will be called whenever the animation
    //event set on the enemy attack animation is called.
    //It applies a force to the player character and pushes them back. 
    void AttackHitEvent()
    {
        if (target == null) { return; }

        target.GetComponentInParent<Player>().TakeDamage(new Vector3(direction.x,
                                                                     direction.y + upwardsAttackForce,
                                                                     direction.z) * attackForce);
        Debug.Log("bamgobamgoBAMGO");
    }

    public void Die()
    {
        if (isDead) return;

        Debug.Log($"{this.gameObject.name} has died");

        isDead = true;
        animator.enabled = false;

        Debug.Log($"{this.gameObject.name} was slain");
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;
        dizzyVFX.Play();
        audioSource.Play();
    }

    void FaceTarget()
    {
        //direction variable of type vector3 gives us the direction that we want this gameobject to rotate with magnitude of 1 (.normalized)
        direction = (target.position - transform.position).normalized;
        //lookRotation variable of type quaternion gives us the rotation that we want this gameobject to make. where do we need to rotate essentially. 
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //Quaternion.Slerp lets us rotate smoothly (spherical interpolation) between 2 vectors. 
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
