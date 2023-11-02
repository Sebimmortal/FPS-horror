using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    private bool pressed:
    public animator anim;
    public bool lookingAtSelf;

    void Start()
    {
        pressed = false;
    }

    void OnTriggerEnter()
    {
        lookingAtSelf = true;
    }

    voic OnTriggerStay()
    {
        if(lookingAtSelf)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                pressed = true;
                anim.SetBool("Pressed", True);
                yield return new WaitForSeconds(1);
                anim.SetBool("DoneAnim", True);
            }
        }
    }
}