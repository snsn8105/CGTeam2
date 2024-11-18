using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResetFocus : MonoBehaviour
{
    private Button backgroundButton;

    void Start()
    {
        // "BackgroundButton"이라는 이름의 버튼을 찾습니다.
        backgroundButton = transform.Find("BackgroundButton").GetComponent<Button>();

        // 버튼이 있다면, 클릭을 비활성화합니다.
        if (backgroundButton != null)
        {
            backgroundButton.interactable = false; // 버튼 클릭 비활성화
        }

        // 마우스 커서 항상 보이도록 설정
        Cursor.visible = true;
    }

    void Update()
    {
        // Update에서 커서가 숨겨지는 경우를 방지
        if (!Cursor.visible)
        {
            Cursor.visible = true;
        }
    }
}
