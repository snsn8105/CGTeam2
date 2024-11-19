using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class twotothree : MonoBehaviour
{
    public void GameScenesCtrls()
    {
        SceneManager.LoadScene("Show");
        Debug.Log("Game Scenes Go");
    }
}
