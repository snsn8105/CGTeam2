using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleCursor : MonoBehaviour
{
    private bool isCursorVisible = false;

    public GameObject gamePanel;

    void Start()
    {
        // 게임 시작 시 커서를 숨기고 잠급니다.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        // ESC 키를 눌렀을 때 ESCPanel을 토글합니다.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isCursorVisible = !isCursorVisible;
            gamePanel.SetActive(!isCursorVisible);

            if (isCursorVisible)
            {
                // 커서를 보이게 하고 잠금을 해제합니다.
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                // 커서를 숨기고 잠급니다.
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    // UI 버튼 클릭 후에도 커서를 유지하도록 설정
    public void OnButtonClick()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
