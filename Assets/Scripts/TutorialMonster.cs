using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialMonster : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        transform.Rotate(0, 0, 0);
        anim.SetBool("isFollowingPlayer", false);
    }
}
