using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class success : MonoBehaviour
{
    public void GameScenesCtrlss()
    {
        LoadingSceneController.Instance.LoadScene("Title");

        Debug.Log("Game Scenes Go");
    }

}
