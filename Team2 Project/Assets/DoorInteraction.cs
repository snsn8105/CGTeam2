using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위한 네임스페이스
using TMPro; // TextMeshPro를 사용하기 위한 네임스페이스

public class DoorInteraction : MonoBehaviour
{
    public string nextSceneName; // 이동할 씬 이름
    public TextMeshProUGUI interactionText; // TextMeshPro UI 텍스트 오브젝트
    private bool isPlayerNear = false; // 플레이어가 문 근처에 있는지 확인

    void Start()
    {
        // interactionText가 연결되지 않았다면 경고 메시지 출력
        if (interactionText == null)
        {
            Debug.LogError("Interaction Text가 설정되지 않았습니다.");
        }
        else
        {
            // 초기 상태에서 텍스트 비활성화
            interactionText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // 플레이어가 문 근처에 있고 "G" 키를 누르면 씬 전환
        if (isPlayerNear && Input.GetKeyDown(KeyCode.G))
        {
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName); // 씬 전환
            }
            else
            {
                Debug.LogError("다음 씬 이름이 설정되지 않았습니다.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 문 근처에 들어오면
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;

            // UI 텍스트 활성화
            if (interactionText != null)
            {
                interactionText.text = "Press 'G' to open the door";
                interactionText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 플레이어가 문 근처를 벗어나면
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;

            // UI 텍스트 비활성화
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
        }
    }
}
