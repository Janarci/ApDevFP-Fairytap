using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonPathfind : MonoBehaviour
{

    private Transform player;
    private HealthHandler damage;
    private AudioSource deathSfx;

    public float attackRadius = 3f;
    Transform interactionTransform;
    private bool isDead = false;
    private Animator animator;
    private NavMeshAgent agent;
    private bool isClose;


    private void Awake()
	{
        player = GameObject.Find("Player").GetComponent<Transform>();
        damage = GameObject.Find("HealthManager").GetComponent<HealthHandler>();
        deathSfx = this.gameObject.GetComponent<AudioSource>();

    }

    // Start is called before the first frame update
    void Start()
    {
        interactionTransform = this.gameObject.transform;
        agent = this.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        setDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract(player, interactionTransform))
        {
            StartCoroutine(attackCycle());
        }
    }

    IEnumerator attackCycle()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(3f);
        animator.ResetTrigger("Attack");
        if (!isDead)
        {
            
            StartCoroutine(attackCycle());
        }
    }

    private void setDestination()
    {
        Vector3 TargetVec = player.transform.position;
        agent.SetDestination(TargetVec);
    }
    public bool canInteract(Transform playerPos, Transform objectPos)
    {
        float distance = Vector3.Distance(playerPos.position, objectPos.position);
        if (distance < attackRadius)
        {
            agent.isStopped = true;
            return true;
        }
        else
            return false;
    }
    public void animDead()
    {
        deathSfx.Play();

        animator.SetBool("Die", true);
        agent.isStopped = true;
        this.isDead = true;
        StopAllCoroutines();

    }
    public void damagePlayer()
    {
        damage.getHitBoss();
    }

}
