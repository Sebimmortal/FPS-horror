using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioClip[] footstepClips;
    public AudioSource audioSource;

    public CharacterController controller;

    public float footstepThreshold;
    public float footstepRate;
    private float lastFootstepTime;
    public float playerMagnitude;
    public int nextFootstep;

    void Update()
    {
        if (GameManager.instance.player.doSprint && Input.GetKey(KeyCode.LeftShift))
            footstepRate = 0.3f;
        if (GameManager.instance.player.doSprint && !Input.GetKey(KeyCode.LeftShift))
            footstepRate = 0.5f;
        if (!GameManager.instance.player.doSprint && !Input.GetKey(KeyCode.LeftShift))
            footstepRate = 0.5f;


        playerMagnitude = controller.velocity.magnitude;

        if (controller.velocity.magnitude > footstepThreshold)
        {
            if (Time.time - lastFootstepTime > footstepRate)
            {
                lastFootstepTime = Time.time;
                audioSource.PlayOneShot(footstepClips[nextFootstep]);
                nextFootstep = 1 - nextFootstep;
            }
        }
    }
}
