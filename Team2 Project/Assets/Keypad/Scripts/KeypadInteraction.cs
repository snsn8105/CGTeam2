using UnityEngine;

public class KeypadInteraction : MonoBehaviour
{
    public GameObject keypadUI;           // 키패드 UI
    public Transform player;              // 플레이어 Transform
    public GameObject pressButtonUI;      // "Press Button" UI
    public float interactionDistance = 1.5f; // 상호작용 거리
    public KeyCode interactionKey = KeyCode.G; // 상호작용 키
    private bool isKeypadActive = false;      // 키패드 활성화 여부
    public playermovement playerController;   // 플레이어 이동 제어 스크립트
    public cameramove playerController2;      // 카메라 이동 제어 스크립트

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        // 플레이어가 키패드에 가까이 있고 벽 등에 가려지지 않았을 때만 UI 표시
        if (distance <= interactionDistance && !isKeypadActive && IsDirectLineOfSight())
        {
            pressButtonUI.SetActive(true);

            // 상호작용 키를 눌렀을 때
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
        Debug.Log("Keypad UI Activated");
        isKeypadActive = true;
        keypadUI.SetActive(true);
        pressButtonUI.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // 플레이어 컨트롤 비활성화
        if (playerController != null)
        {
            playerController.enabled = false;
        }
        if (playerController2 != null)
        {
            playerController2.enabled = false;
        }
    }

    public void DeactivateKeypadUI()
    {
        Debug.Log("Keypad UI Deactivated");
        keypadUI.SetActive(false);
        isKeypadActive = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 플레이어 컨트롤 활성화
        if (playerController != null)
        {
            playerController.enabled = true;
        }
        if (playerController2 != null)
        {
            playerController2.enabled = true;
        }
    }

    public void OnCloseButtonPressed()
    {
        DeactivateKeypadUI();
    }

    private bool IsDirectLineOfSight()
    {
        // 플레이어와 키패드 사이에 장애물이 없는지 확인
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // 레이캐스트를 통해 장애물이 있는지 확인
        int layerMask = ~LayerMask.GetMask("Ignore Raycast"); // Ignore Raycast 레이어 제외
        if (Physics.Raycast(transform.position, directionToPlayer.normalized, out RaycastHit hit, distanceToPlayer, layerMask))
        {
            // 레이캐스트가 플레이어에 맞는지 확인
            if (hit.collider != null && hit.collider.transform == player)
            {
                return true;
            }
            else
            {
                Debug.Log($"Raycast hit: {hit.collider.gameObject.name}");
                return false;
            }
        }

        // 장애물이 없으면 true
        return true;
    }
}
