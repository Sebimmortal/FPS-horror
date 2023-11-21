using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    private bool pressed;
    public Animator anim;

    void Start()
    {
        pressed = false;
    }    

    void Press()
    {
        if(pressed == false)
        {
            pressed = true;
            anim.SetBool("pressed", true);
            GameManager.instance.Player.buttonSet1();
        }
    }
}