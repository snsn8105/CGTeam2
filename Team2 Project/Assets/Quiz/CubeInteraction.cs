using UnityEngine;
using UnityEngine.UI;  // UI 관련 클래스 사용

public class CubeInteraction : MonoBehaviour
{
    public GameObject uiPanel;        // UI Panel GameObject
    public GameObject pressButtonUI; // "Press [Key]" UI Text 요소
    public Transform player;          // 플레이어 오브젝트 (또는 사용자 캐릭터)
    private bool isUIVisible = false; // UI 활성화 상태
    public float interactionDistance = 1.5f; // UI를 활성화할 거리
    public KeyCode interactionKey = KeyCode.E; // 상호작용 키

    public playermovement playerController; // 플레이어 컨트롤 스크립트 참조
    public cameramove playerCameraController; // 카메라 컨트롤 스크립트 참조

    void Start()
    {
        // UI 초기화
        uiPanel.SetActive(false);      // UI Panel 비활성화
        pressButtonUI.SetActive(false); // "Press [Key]" UI 비활성화
    }

    void Update()
    {
        // 플레이어와 큐브 간 거리 계산
        float distance = Vector3.Distance(transform.position, player.position);

        // 플레이어가 상호작용 거리 내에 있을 때
        if (distance <= interactionDistance)
        {
            pressButtonUI.SetActive(!isUIVisible); // "Press [Key]" UI 표시 (UI가 보일 때는 숨김)

            // 상호작용 키 입력 시 UI 토글
            if (Input.GetKeyDown(interactionKey))
            {
                ToggleUIPanel();
            }
        }
        else
        {
            pressButtonUI.SetActive(false); // 거리 벗어나면 UI 숨김
        }
    }

    private void ToggleUIPanel()
    {
        if (isUIVisible)
        {
            DeactivateUIPanel();
        }
        else
        {
            ActivateUIPanel();
        }
    }

    private void ActivateUIPanel()
    {
        Debug.Log("UI Panel Activated");
        isUIVisible = true;
        uiPanel.SetActive(true);        // UI Panel 활성화
        pressButtonUI.SetActive(false); // "Press [Key]" UI 숨김

        // 커서 잠금 해제 및 표시
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // 플레이어 컨트롤 비활성화
        if (playerController != null)
        {
            playerController.enabled = false;
        }
        if (playerCameraController != null)
        {
            playerCameraController.enabled = false;
        }
    }

    private void DeactivateUIPanel()
    {
        Debug.Log("UI Panel Deactivated");
        isUIVisible = false;
        uiPanel.SetActive(false); // UI Panel 비활성화

        // 커서 잠금 설정 및 숨김
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 플레이어 컨트롤 활성화
        if (playerController != null)
        {
            playerController.enabled = true;
        }
        if (playerCameraController != null)
        {
            playerCameraController.enabled = true;
        }
    }
}
