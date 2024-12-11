using System.Collections;
using UnityEngine;
using TMPro;

public class EnergyCellCombiner : MonoBehaviour
{
    public GameObject energyCell; // 에너지 셀 객체
    public float activationDistance = 3.0f; // 버튼과 플레이어 사이의 활성화 거리
    public Transform player; // 플레이어의 Transform
    public TMP_Text interactionText; // "에너지셀 결합하기" 메시지를 표시할 TextMeshPro 텍스트
    public KeyCode interactionKey = KeyCode.E; // 상호작용 키

    public float glowDuration = 1f; // 반짝이는 효과의 지속 시간
    public Color glowColor = Color.yellow; // 반짝임 효과의 색상
    public ParticleSystem activationEffect; // 파티클 효과

    private bool isPlayerInRange = false; // 플레이어가 버튼 근처에 있는지 여부
    private bool isEnergyCellCombined = false; // 에너지 셀이 결합되었는지 여부
    private Material energyCellMaterial; // 에너지 셀의 머티리얼

    void Start()
    {
        if (energyCell == null)
        {
            Debug.LogError("Energy Cell이 연결되지 않았습니다.");
        }
        else
        {
            // 에너지 셀 비활성화
            energyCell.SetActive(false);

            // 에너지 셀의 머티리얼 가져오기
            Renderer renderer = energyCell.GetComponent<Renderer>();
            if (renderer != null)
            {
                energyCellMaterial = renderer.material;
            }
            else
            {
                Debug.LogError("Energy Cell에 Renderer가 없습니다.");
            }
        }

        if (interactionText == null)
        {
            Debug.LogError("Interaction Text가 설정되지 않았습니다.");
        }
        else
        {
            // 상호작용 텍스트 비활성화
            interactionText.gameObject.SetActive(false);
        }

        if (player == null)
        {
            Debug.LogError("Player Transform이 설정되지 않았습니다.");
        }

        if (activationEffect == null)
        {
            Debug.LogError("Activation Particle Effect가 설정되지 않았습니다.");
        }
        else
        {
            // 파티클 효과를 초기 비활성화
            activationEffect.Stop();
        }
    }

    void Update()
    {
        // 플레이어와 버튼 사이의 거리 계산
        float distance = Vector3.Distance(player.position, transform.position);

        // 플레이어가 활성화 거리 내에 들어왔을 때
        if (distance <= activationDistance && !isEnergyCellCombined)
        {
            if (!isPlayerInRange)
            {
                ShowInteractionText();
            }

            isPlayerInRange = true;

            // E 키를 눌렀을 때 에너지 셀 결합
            if (Input.GetKeyDown(interactionKey))
            {
                CombineEnergyCell();
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
            interactionText.text = "E 키를 눌러 에너지셀 결합하기";
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

    private void CombineEnergyCell()
    {
        isEnergyCellCombined = true;
        isPlayerInRange = false;

        // 에너지 셀 활성화
        if (energyCell != null)
        {
            energyCell.SetActive(true);
            StartCoroutine(GlowEffect()); // 반짝임 효과 시작
        }

        // 파티클 효과 재생
        if (activationEffect != null)
        {
            activationEffect.transform.position = energyCell.transform.position; // 파티클 위치를 에너지 셀 위치로 설정
            activationEffect.Play(); // 파티클 효과 재생
        }

        // 결합 완료 메시지 표시
        if (interactionText != null)
        {
            interactionText.text = "에너지셀 결합 완료!";
            Invoke(nameof(HideInteractionText), 2f); // 2초 후 텍스트 숨기기
        }

        Debug.Log("에너지 셀이 성공적으로 결합되었습니다.");
    }

    private IEnumerator GlowEffect()
    {
        if (energyCellMaterial == null) yield break;

        float elapsedTime = 0f;
        Color originalColor = energyCellMaterial.GetColor("_EmissionColor");
        energyCellMaterial.EnableKeyword("_EMISSION");

        while (elapsedTime < glowDuration)
        {
            float lerp = Mathf.PingPong(elapsedTime * 2f, 1f);
            Color glow = Color.Lerp(originalColor, glowColor, lerp);
            energyCellMaterial.SetColor("_EmissionColor", glow);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 반짝임 효과 종료
        energyCellMaterial.SetColor("_EmissionColor", originalColor);
    }
}
