using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent agent;
    private bool followPlayer;

    Collider[] hitColliders;

    public float radius;

    void Start()
    {
        transform.Rotate(0, 90, 0);
    }

    void Update()
    {
        hitColliders = Physics.OverlapSphere(transform.position, radius, 4);
        Sprint();
        if(followPlayer)
            agent.SetDestination(GameManager.instance.player.transform.position);
    }

    void Sprint()
    {
        if(hitColliders.length > 0)
        {
            anim.SetBool("isFollowingPlayer", false);
            followPlayer = false;
        }
        else
        {
            anim.SetBool("isFollowingPlayer", true);
            followPlayer = true;
        }
    }
}
