using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Animator anim;
    
    void Start()
    {
        transform.Rotate(0, 90, 0);
    }

    void Sprint()
    {
        anim.SetBool("isFollowingPlayer", true);
    }
}
