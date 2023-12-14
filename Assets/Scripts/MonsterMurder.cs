using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMurder : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        transform.position -= tranform.forward * -1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
