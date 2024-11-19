using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class onetotwo : MonoBehaviour
{
    public void GameScenesCtrl()
    {
        SceneManager.LoadScene("2");
        Debug.Log("Game Scenes Go");
    }
}
