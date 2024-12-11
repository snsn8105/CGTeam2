using UnityEngine;

public class MouseOn : MonoBehaviour
{
    void Start()
    {
        // 마우스 커서 활성화
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("마우스 커서가 활성화되었습니다.");
    }
}
