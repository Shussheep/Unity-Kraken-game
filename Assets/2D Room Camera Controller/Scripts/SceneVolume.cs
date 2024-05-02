using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneVolume : MonoBehaviour
{
    [SerializeField] private int loadScene = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");
        if (collision.tag == "Player") 
        {
            SceneManager.LoadScene(loadScene);
        }
    }
}
