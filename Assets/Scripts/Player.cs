using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Character Controller")]
    public float moveSpeed;
    public CharacterController controller;

    [Header("Stamina")]
    public float staminaPercent;
    public float curStamina;
    public int maxStamina;
    public bool doSprint;
    public Image staminaFill;
    public bool doMove;
    public Color[] colorAray;
    public float timeAtLastColorUpdate;
    public int curColor;
    public float updateRate;

    [Header("Audio")]
    public AudioClip[] audioClips;
    public AudioSource audioSource;


    public bool inBeta;

    [Header("Camera")]
    public Transform cam;
    public float lookSensitivity;
    public float minXRot;
    public float maxXRot;
    private float curXRot;

    [Header("Light")]
    public Light light;
    public int nextIntensity;

    [Header("Monster")]
    public GameObject monster;
    private int monsterNums = 1;
    private int monstersSpawned = 0;
    public bool jumpscared;

    [Header("Interactions")]
    public RaycastHit hit;
    public int buttonSet1AmountPressed;
    public int buttonSet2AmountPressed;

    [Header("Jumpscares")]
    public Vector3 rotationVector;

    [Header("Scene Management")]
    private Scene scene;
    private string[] scenePaths;

    void Start()
    {
        Screen.fullScreen = true;

        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            Cursor.lockState = CursorLockMode.None;
            jumpscared = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            jumpscared = false;
            light.intensity = 0;
        }

        if(staminaFill != null)
        {
            staminaFill.color = colorAray[colorAray.Length - 1];
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene("LVL-1");
    }

    public void GameClose()
    {
        Application.Quit();
    }

    void Update()
    {
        if (jumpscared == false)
        {
            Move();
            Look();
        }
        else
            audioSource.Stop();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 dir = transform.right * x + transform.forward * z;
        dir.Normalize();
        if(staminaFill != null)
            Sprint();

        dir *= moveSpeed * Time.deltaTime;
        if (doMove)
            controller.Move(dir);
        else
        {
            dir = transform.position;
            doMove = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if(SceneManager.GetActiveScene().name == "LVL-1")
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
            }
        }

        if (doSprint == false)
        {
            curStamina += (0.25f * Time.deltaTime);

            if(Time.time - timeAtLastColorUpdate >= updateRate)
            {
                staminaFill.color = colorAray[curColor];
                curColor++;

                timeAtLastColorUpdate = Time.time;
                if(curColor == 2)
                    curColor = 0;
            }

            if (curStamina >= maxStamina)
            {
                curStamina = maxStamina;
                doSprint = true;
                staminaFill.color = colorAray[colorAray.Length - 1];
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
                moveSpeed = 5.0f;
            }
        }

        staminaPercent = curStamina / maxStamina;
        
        staminaFill.fillAmount = staminaPercent;
        
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
            
            if(inBeta == false)
                audioSource.PlayOneShot(audioClips[1]);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (!inBeta)
            {
                if (Physics.Raycast(transform.position, cam.forward, out hit, 4))
                    Debug.DrawRay(transform.position, cam.forward * hit.distance, Color.red);
            }
        }
    }
    
    void buttonSet1() 
    {
        buttonSet1AmountPressed++;
        if(buttonSet1AmountPressed == 4)
        {
            buttonSet1AmountPressed = 4;
        }
    }

    public void Jumpscare ()
    {
        jumpscared = true;
        transform.LookAt(GameManager.instance.murderer.transform.position);
        Vector3 rotationVector = cam.transform.rotation.eulerAngles;

        rotationVector.x = -45;
        cam.transform.rotation = Quaternion.Euler(rotationVector);

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
