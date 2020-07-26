using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;

    Animator enemyAnim;
    bool enemyNowMoving = false;
    bool enemyCombat = false;

    

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        enemyAnim = GetComponent<Animator>();
        CharacterCombat.instance.opponentStats = target.GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            enemyNowMoving = true;
            EnemyAnimMove();
            if (distance <= agent.stoppingDistance)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                enemyNowMoving = false;
                enemyCombat = true;
                if (targetStats != null && enemyCombat == true)
                {
                    enemyAnim.SetInteger("Conditions", 2);
                }
                FaceTargert();
            }
        }
    }

    void EnemyAnimMove()
    {
        if (enemyNowMoving == true)
        {
            enemyAnim.SetInteger("Conditions", 1);
        } else if (enemyNowMoving == false)
        {
            enemyAnim.SetInteger("Conditions", 0);
        }
    }

    void FaceTargert()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
