using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent agent;
    private bool followPlayer;

    private float distanceToPlayer;

    void Start()
    {
        transform.Rotate(0, 90, 0);
        anim.SetBool("Jumpscare", false);
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, GameManager.instance.player.transform.position);
        Sprint();
        if(followPlayer)
            agent.SetDestination(GameManager.instance.player.transform.position);
    }

    void Sprint()
    {
        if(distanceToPlayer <= 5)
        {
            anim.SetBool("isFollowingPlayer", false);
            followPlayer = false;
            Jumpscare();
            GameManager.instance.player.Jumpscare();
        }
        else
        {
            anim.SetBool("isFollowingPlayer", true);
            followPlayer = true;
        }
    }

    void Jumpscare()
    {
        anim.SetBool("Jumpscare", true);
        transform.LookAt(GameManager.instance.player.transform.position);
    }
}
