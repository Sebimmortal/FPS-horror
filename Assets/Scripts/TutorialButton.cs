using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    private bool pressed;
    public Animator anim;
    public bool lookingAtSelf;

    void Start()
    {
        pressed = false;
    }

    void OnTriggerEnter()
    {
        lookingAtSelf = true;
    }

    void OnTriggerExit()
    {
        lookingAtSelf = false;
    }

    void OnTriggerStay ()
    {
        if (lookingAtSelf)
        {
            if (Input.GetKeyDown("E"))
            {
                pressed = true;
                anim.SetBool("Pressed", true);
            }
        }
    }
}