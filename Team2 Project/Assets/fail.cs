using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fail : MonoBehaviour
{
    public void GameScenesCtrlsss()
    {
        LoadingSceneController.Instance.LoadScene("Title");

        Debug.Log("Game Scenes Go");
    }

}
