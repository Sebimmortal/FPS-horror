using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent agent;

    private float distanceToPlayer;
    public bool jumpscared;
    public GameObject monsterKill;

    void Start()
    {
        transform.Rotate(0, 90, 0);
        jumpscared = false;
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, GameManager.instance.player.transform.position);
        Sprint();
        if (!jumpscared)
        {
            agent.SetDestination(GameManager.instance.player.transform.position);
        }
    }

    void Sprint()
    {
        if(!jumpscared)
        {
            if (distanceToPlayer <= 5)
            {
                GameManager.instance.player.Jumpscare();
                Jumpscare();
            }
            else
            {
                anim.SetBool("isFollowingPlayer", true);
            }
        }
    }

    void Jumpscare()
    {
        jumpscared = true;
        Instantiate(monsterKill, transform.position, Quaternion.identity, GameManager.instance.player.transform);
        Destroy(gameObject);
    }
}
