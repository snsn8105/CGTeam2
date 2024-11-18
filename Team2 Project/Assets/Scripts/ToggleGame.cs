using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGame : MonoBehaviour
{
    public GameObject panel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panel != null)
            {
                bool isActive = !panel.activeSelf;
                panel.SetActive(isActive);

                // 패널이 활성화되면 게임을 일시 정지하고, 비활성화되면 게임을 재개합니다.
                Time.timeScale = isActive ? 0 : 1;
            }
        }
    }
}
