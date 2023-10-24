using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Animation anim;
    
    void Start()
    {
        transform.Rotate(0, 90, 0);
        anim.Play();
    }

    void Sprint()
    {
        anim.Play();
    }
}
