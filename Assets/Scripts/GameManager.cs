using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public Monster monster;
    public MonsterMurder murderer;
    public Door door1;
    public Door door2;

    public GameObject controls;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Debug.Log("Instance already exists, Destroying object");
            Destroy(this);
        }
    }

    public void OnControlsOpen ()
    {
        controls.SetActive(true);
    }

    public void OnControlsClose ()
    {
        controls.SetActive(false);
    }
}