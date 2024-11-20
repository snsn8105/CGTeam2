using UnityEngine;
public class KeypadInteraction : MonoBehaviour
{
    public GameObject keypadUI;
    public Transform player;
    public GameObject pressButtonUI;
    public float interactionDistance = 1.5f;
    public KeyCode interactionKey = KeyCode.G;

    private bool isKeypadActive = false;
    public playermovement playerController; // 플레이어 제어 스크립트 참조
    public cameramove playerController2;

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= interactionDistance && !isKeypadActive)// 중복 활성화 방지
        {
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
                // 플레이어 컨트롤 비활성화
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
            playerController2.enabled = true; // 카메라 컨트롤 활성화
        }
    }

    public void OnCloseButtonPressed()
    {
        DeactivateKeypadUI();
    }
}
