using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnder : MonoBehaviour
{
    public Scene scene;
    private string[] scenePaths;
    public string levelToGoTo;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            scene = SceneManager.GetActiveScene();
            int CurrentScene = scene.buildIndex;
            SceneManager.LoadScene(levelToGoTo);
        }
    }
}