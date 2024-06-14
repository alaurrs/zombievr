using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    private Transform target;

    private NavMeshAgent agent;

    private Animator m_Animator;
    
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }
    
    protected void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            // Face the target
            FaceTarget();
            agent.SetDestination(target.position);
            m_Animator.SetBool("IsFollowing", true);
            if (distance <= agent.stoppingDistance)
            {             
                GetComponent<Rigidbody>().isKinematic = true;
                m_Animator.SetBool("IsFollowing", false);
            }
            else
            {
                GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
            m_Animator.SetBool("IsFollowing", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
