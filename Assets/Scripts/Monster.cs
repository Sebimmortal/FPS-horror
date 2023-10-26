using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent agent;

    public Vector3 playerPos = GetComponent<Player>.transform.Position;

    void Start()
    {
        transform.Rotate(0, 90, 0);
    }

    void FixedUpdate()
    {
        agent.SetDestination(Player.Transform.Position);
    }

    void Sprint()
    {
        anim.SetBool("isFollowingPlayer", true);
    }
}
