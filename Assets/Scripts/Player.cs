using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public CharacterController controller;
    public float staminaPercent;
    public float curStamina;
    public int maxStamina;
    public bool doSprint;
    public Image staminaFill;
    public Color[] colorAray;
    public float timeAtLastColorUpdate;
    public int curColor;

    public AudioClip[] audioClips;
    public AudioSource audioSource;

    public bool inBeta;

    public Transform cam;
    public float lookSensitivity;
    public float minXRot;
    public float maxXRot;
    private float curXRot;

    public Light light;
    public int nextIntensity;

    public bool doMove;

    public GameObject monster;
    private int monsterNums = 1;
    private int monstersSpawned = 0;

    public bool jumpscared;

    public RaycastHit hit;
    public int buttonSet1AmountPressed;
    public int buttonSet2AmountPressed;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        light.intensity = 0;
    }

    void Update()
    {
        if (jumpscared == false)
        {
            Move();
            Look();
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 dir = transform.right * x + transform.forward * z;
        dir.Normalize();

        Sprint();

        dir *= moveSpeed * Time.deltaTime;
        if (doMove)
            controller.Move(dir);
        else
            dir = transform.position;
            doMove = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(SceneManager.GetActiveScene().name == "Scene1")
        {
            SceneManager.LoadScene("Scene2");
        }

        if(SceneManager.GetActiveScene().name == "Scene2")
        {
            if(monstersSpawned != monsterNums)
            {
                Instantiate(monster, new Vector3(-67.5f, 0, 22.5f), Quaternion.identity);
                monstersSpawned += 1;
            }
        }
    }

    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) == false && doSprint)
        {
            moveSpeed = 5;
            curStamina += (0.5f * Time.deltaTime);

            if (curStamina > maxStamina)
            {
                curStamina = maxStamina;
                audioSource.Stop();
            }
        }

        if (doSprint == false)
        {
            curStamina += (0.25f * Time.deltaTime);
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
                if(inBeta == false)
                    Breath();
            }
        }

        staminaPercent = curStamina / maxStamina;
        staminaFill.fillAmount = staminaPercent;
        
    }

    void Breath()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }

    void Look()
    {
        float x = Input.GetAxis("Mouse X") * lookSensitivity;
        float y = Input.GetAxis("Mouse Y") * lookSensitivity;

        transform.eulerAngles += Vector3.up * x;

        curXRot += y;
        curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);

        cam.localEulerAngles = new Vector3(-curXRot, 0.0f, 0.0f);
        if (Input.GetMouseButtonDown(0))
        {
            light.intensity = nextIntensity;
            if (light.intensity == 0)
                nextIntensity = 10;
            else
                nextIntensity = 0;
            
            if(inBeta == true)
                audioSource.PlayOneShot(audioClips[1]);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (1 == 0)
            {
                if (Physics.Raycast(transform.position, cam.forward, out hit, 4))
                    Debug.DrawRay(transform.position, cam.forward * hit.distance, Color.red);
            }
        }
    }
    
    void buttonSet1() 
    {
        buttonSet1AmountPressed++;
        if(buttonSet1AmountPressed == 4);
        {
            buttonSet1AmountPressed = 4;
        }
    }

    public void Jumpscare ()
    {
        jumpscared = true;
        transform.LookAt(GameManager.instance.monster.transform.position);
        transform.Rotate(0, 180, 0);
    }
}
    //         void OnTriggerStay ()
    // {
    //     if (lookingAtSelf)
    //     {
    //         if (Input.GetKeyDown("E"))
    //         {
    //             pressed = true;
    //             anim.SetBool("Pressed", true);
    //         }
    //     }
    // }
