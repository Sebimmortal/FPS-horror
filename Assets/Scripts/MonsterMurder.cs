using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterMurder : MonoBehaviour
{
    public Animator anim;
    public Vector3 rotationVector;
    public Vector3 transformVector;

    public AudioSource source;
    public AudioClip clip;
    
    public Scene scene;
    private string[] scenePaths;

    private void Start()
    {
        Vector3 rotationVector = transform.rotation.eulerAngles;
        rotationVector.x = 45;
        rotationVector.y = 180;
        transform.localRotation = Quaternion.Euler(rotationVector);

        transformVector = new Vector3(2.03f, -2.78f, 11.28f);
        transform.localPosition = transformVector;
        source.PlayOneShot(clip, 6f);
    }
    public void OnAnimationPunch()
    {
        SceneManager.LoadScene("LVL-1");
    }
}
