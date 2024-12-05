using UnityEngine;
using UnityEngine.UI;  // UI 관련 클래스 사용

public class CubeInteraction : MonoBehaviour
{
    public GameObject uiPanel;  // UI Panel GameObject (비활성화된 상태로 시작)
    public GameObject quizText;       // QUIZ TEXT1 UI Text 요소
    public Transform player;    // 플레이어 오브젝트 (또는 사용자 캐릭터)

    private bool isUIVisible = false;  // UI의 상태 (보임/숨김)
    private float interactionDistance = 1.5f;  // UI를 활성화할 거리
    public KeyCode interactionKey = KeyCode.E;  // 상호작용을 위한 키

    void Start()
    {
        // UI 비활성화 (처음에는 보이지 않도록 설정)
        uiPanel.SetActive(false);
        quizText.gameObject.SetActive(false);  // QUIZ TEXT1 비활성화
    }

    void Update()
    {
        // 큐브와 플레이어의 거리 계산
        float distance = Vector3.Distance(transform.position, player.position);

        // 거리 조건이 충족되었을 때만 "E" 키 입력 감지
        if (distance <= interactionDistance)
        {
            // 텍스트를 보여준다
            quizText.gameObject.SetActive(true);

            // "E" 버튼을 누르면 UI를 토글
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleUI();
            }
        }
        else
        {
            // 일정 거리 이상일 경우 텍스트 숨기기
            quizText.gameObject.SetActive(false);
        }
    }

    // UI를 토글하는 함수
    void ToggleUI()
    {
        isUIVisible = !isUIVisible;  // UI 상태 변경
        uiPanel.SetActive(isUIVisible);  // 상태에 맞게 UI 활성화/비활성화
    }
}
