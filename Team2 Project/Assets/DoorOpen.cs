using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 전환을 위해 추가
using TMPro; // TextMeshPro 사용을 위해 추가

public class DoorOpen : MonoBehaviour
{
    public float interactionDistance = 3.0f; // 플레이어와 문 사이의 상호작용 거리
    public KeyCode interactionKey = KeyCode.E; // 상호작용 키
    public GameObject pressButtonUI; // "OPEN THE DOOR [E]" UI
    public TMP_Text insufficientItemsText; // 아이템 부족 시 표시할 TextMeshPro 텍스트 UI
    public int requiredSlotCount = 5; // 필요한 슬롯 개수 조건

    [SerializeField]
    private Inventory playerInventory; // 플레이어의 인벤토리

    private float messageDisplayDuration = 2f; // 메시지를 표시할 시간
    private Coroutine hideMessageCoroutine; // 메시지를 숨기는 코루틴을 추적

    void Start()
    {
        // 플레이어 인벤토리가 설정되지 않았을 경우 경고 메시지 출력
        if (playerInventory == null)
        {
            Debug.LogWarning("Inventory가 설정되지 않았습니다. Inspector에서 설정해주세요.");
        }

        // 초기 TextMeshPro 텍스트 UI 숨기기
        if (insufficientItemsText != null)
        {
            insufficientItemsText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(Camera.main.transform.position, transform.position);

        // 플레이어가 문에 가까이 있을 때
        if (distance <= interactionDistance)
        {
            pressButtonUI.SetActive(true);

            // 상호작용 키 입력
            if (Input.GetKeyDown(interactionKey))
            {
                TryOpenDoor();
            }
        }
        else
        {
            pressButtonUI.SetActive(false);
        }
    }

    private void TryOpenDoor()
    {
        // Inventory가 설정되지 않은 경우 처리
        if (playerInventory == null)
        {
            Debug.LogError("Inventory가 설정되지 않았습니다. 문을 열 수 없습니다.");
            return;
        }

        // Inventory의 currentSlotCount 값 확인
        if (playerInventory.currentSlotCount >= requiredSlotCount)
        {
            // 씬 전환
            SceneManager.LoadScene("spaceShip");
            Debug.Log("문이 열렸습니다! 다음 씬으로 이동합니다.");
        }
        else
        {
            Debug.Log($"문을 열기 위해 {requiredSlotCount}개의 슬롯이 사용 중이어야 합니다.");
            ShowInsufficientItemsMessage($"비행에 필요한 아이템이 {requiredSlotCount - playerInventory.currentSlotCount}개 더 필요하다.");
        }
    }

    private void ShowInsufficientItemsMessage(string message)
    {
        // TextMeshPro UI에 메시지 설정
        if (insufficientItemsText != null)
        {
            insufficientItemsText.text = message;
            insufficientItemsText.gameObject.SetActive(true);

            // 기존 코루틴이 실행 중이라면 중지
            if (hideMessageCoroutine != null)
            {
                StopCoroutine(hideMessageCoroutine);
            }

            // 일정 시간 후에 텍스트 숨기기
            hideMessageCoroutine = StartCoroutine(HideMessageAfterDelay());
        }
    }

    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDisplayDuration);
        if (insufficientItemsText != null)
        {
            insufficientItemsText.gameObject.SetActive(false);
        }
    }
}
