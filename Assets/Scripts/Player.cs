using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public CharacterController controller;
    public float staminaPercent;
    public float curStamina;
    public int maxStamina;
    public bool doSprint;
    public Image staminaFill;

    public AudioClip[] AudioClip;
    public AudioSource audioSource;

    public Transform cam;
    public float lookSensitivity;
    public float minXRot;
    public float maxXRot;
    private float curXRot;

    public GameObject monster;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        Look();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 dir = transform.right * x + transform.forward * z;
        dir.Normalize();

        Sprint();

        dir *= moveSpeed * Time.deltaTime;

        controller.Move(dir);



    }

    void OnTriggerEnter(Collider other)
    {
        Instantiate(monster, new Vector3(-67.5f, 0, 22.5f), Quaternion.identity);
    }

    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) == false && doSprint)
        {
            moveSpeed = 5;
            curStamina += (1 * Time.deltaTime);

            if (curStamina > maxStamina)
            {
                curStamina = maxStamina;
                audioSource.Stop();
            }
        }

        if (doSprint == false)
        {
            moveSpeed = 2.5f;
            curStamina += (0.5f * Time.deltaTime);
            if (curStamina >= maxStamina)
            {
                curStamina = maxStamina;
                doSprint = true;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (curStamina > 0 && doSprint == true)
            {
                curStamina -= (1 * Time.deltaTime);
                moveSpeed = 10;
            }
            if (curStamina <= 0)
            {
                doSprint = false;
                Breath();
            }
        }

        staminaPercent = curStamina / maxStamina;
        staminaFill.fillAmount = staminaPercent;
        
    }

    void Breath()
    {
        audioSource.PlayOneShot(AudioClip[0]);
    }

    void Look()
    {
        float x = Input.GetAxis("Mouse X") * lookSensitivity;
        float y = Input.GetAxis("Mouse Y") * lookSensitivity;

        transform.eulerAngles += Vector3.up * x;

        curXRot += y;
        curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);

        cam.localEulerAngles = new Vector3(-curXRot, 0.0f, 0.0f);
    }
}
