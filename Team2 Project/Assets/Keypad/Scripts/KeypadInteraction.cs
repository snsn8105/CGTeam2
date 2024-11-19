using UnityEngine;

public class KeypadInteraction : MonoBehaviour
{
    [Header("References")]
    public GameObject keypadUI;         // 확대된 키패드 UI(Canvas)
    public Transform player;            // 플레이어 Transform
    public GameObject pressButtonUI;    // "Press the button (G)" UI
    public float interactionDistance = 10.0f; // 상호작용 가능한 거리
    public KeyCode interactionKey = KeyCode.G; // 키패드 활성화 키

    private bool isKeypadActive = false;

    void Update()
    {
        // 키패드와 플레이어 거리 확인
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= interactionDistance)
        {
            // "Press the button (G)" 표시
            pressButtonUI.SetActive(true);

            if (Input.GetKeyDown(interactionKey))
            {
                ActivateKeypadUI();
            }
        }
        else
        {
            pressButtonUI.SetActive(false);
        }
    }

    private void ActivateKeypadUI()
    {
        Debug.Log("Keypad UI Activated"); // 확인용 디버그 메시지
        isKeypadActive = true;
        keypadUI.SetActive(true);       // 키패드 UI 활성화
        pressButtonUI.SetActive(false); // "Press the button" 숨김

        Cursor.lockState = CursorLockMode.None; // 마우스 커서 활성화
        Cursor.visible = true;
    }

    public void DeactivateKeypadUI()
    {
        isKeypadActive = false;
        keypadUI.SetActive(false);      // 키패드 UI 숨김

        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 비활성화
        Cursor.visible = false;
    }
}
