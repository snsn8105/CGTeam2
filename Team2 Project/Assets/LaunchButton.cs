using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LaunchButton : MonoBehaviour
{
    public GameObject energyCell; // 에너지 셀 객체 (활성화 상태 확인)
    public float activationDistance = 3.0f; // 버튼과 플레이어 사이의 상호작용 거리
    public Transform player; // 플레이어의 Transform
    public TMP_Text interactionText; // "집으로 출발!" 메시지를 표시할 TextMeshPro 텍스트
    public KeyCode interactionKey = KeyCode.E; // 상호작용 키
    public Renderer buttonRenderer; // 이륙 버튼의 Renderer
    public Light haloLight; // Halo 효과로 사용할 Light
    public Color glowColor = Color.yellow; // Halo 및 반짝임 효과 색상
    public float glowIntensity = 5.0f; // 반짝임 강도
    public float glowSpeed = 3.0f; // 반짝임 속도

    private bool isPlayerInRange = false; // 플레이어가 버튼 근처에 있는지 여부
    private Material buttonMaterial; // 버튼의 머티리얼
    private bool isGlowing = false; // 버튼이 반짝이고 있는지 여부

    void Start()
    {
        if (interactionText == null)
        {
            Debug.LogError("Interaction Text가 설정되지 않았습니다.");
        }
        else
        {
            interactionText.gameObject.SetActive(false); // 기본적으로 텍스트 비활성화
        }

        if (player == null)
        {
            Debug.LogError("Player Transform이 설정되지 않았습니다.");
        }

        if (energyCell == null)
        {
            Debug.LogError("Energy Cell이 연결되지 않았습니다.");
        }

        if (buttonRenderer == null)
        {
            Debug.LogError("Button Renderer가 설정되지 않았습니다.");
        }
        else
        {
            // 버튼의 머티리얼 가져오기
            buttonMaterial = buttonRenderer.material;
            buttonMaterial.EnableKeyword("_EMISSION");
        }

        if (haloLight == null)
        {
            Debug.LogError("Halo Light가 설정되지 않았습니다.");
        }
        else
        {
            haloLight.enabled = false; // Halo Light 초기 비활성화
        }
    }

    void Update()
    {
        // 에너지 셀이 활성화된 상태인지 확인
        if (energyCell != null && energyCell.activeSelf && !isGlowing)
        {
            StartGlowEffect();
        }

        // 플레이어와 버튼 사이의 거리 계산
        float distance = Vector3.Distance(player.position, transform.position);

        // 플레이어가 활성화 거리 내에 들어왔을 때
        if (distance <= activationDistance && energyCell.activeSelf)
        {
            if (!isPlayerInRange)
            {
                ShowInteractionText();
            }

            isPlayerInRange = true;

            // E 키를 눌렀을 때 success 씬으로 이동
            if (Input.GetKeyDown(interactionKey))
            {
                Launch();
            }
        }
        else
        {
            if (isPlayerInRange)
            {
                HideInteractionText();
            }

            isPlayerInRange = false;
        }
    }

    private void ShowInteractionText()
    {
        if (interactionText != null)
        {
            interactionText.text = "E 키를 눌러 집으로 출발!";
            interactionText.gameObject.SetActive(true);
        }
    }

    private void HideInteractionText()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    private void Launch()
    {
        Debug.Log("집으로 출발!");
        // success 씬으로 이동
        LoadingSceneController.Instance.LoadScene("success");
    }

    private void StartGlowEffect()
    {
        isGlowing = true;

        if (haloLight != null)
        {
            haloLight.enabled = true; // Halo Light 활성화
            StartCoroutine(HaloEffect());
        }

        StartCoroutine(GlowEffect());
    }

    private IEnumerator GlowEffect()
    {
        float elapsedTime = 0f;

        while (true)
        {
            // PingPong으로 Emission Color 반짝임 구현
            float intensity = Mathf.PingPong(elapsedTime * glowSpeed, glowIntensity);
            Color glow = glowColor * intensity;
            buttonMaterial.SetColor("_EmissionColor", glow);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator HaloEffect()
    {
        float elapsedTime = 0f;

        while (true)
        {
            // Halo Light의 강도를 반짝이도록 변경
            haloLight.intensity = Mathf.PingPong(elapsedTime * glowSpeed, glowIntensity);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
