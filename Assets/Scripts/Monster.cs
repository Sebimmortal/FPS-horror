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
    public bool jumpscared;
    public GameObject monsterkill;

    void Start()
    {
        transform.Rotate(0, 90, 0);
        anim.SetBool("Jumpscare", false);
        jumpscared = false;
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, GameManager.instance.player.transform.position);
        Sprint();
        if (followPlayer)
        {
            if (jumpscared)
                agent.SetDestination(transform.position);
            else
                agent.SetDestination(GameManager.instance.player.transform.position);
        }
    }

    void Sprint()
    {
        if(jumpscared == false)
        {
            if (distanceToPlayer <= 5)
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
    }

    void Jumpscare()
    {
        anim.SetBool("Jumpscare", true);
        jumpscared = true;
        transform.position -= transform.forward * -1;
    }
}
