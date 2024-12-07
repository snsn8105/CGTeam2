using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DoorButton : MonoBehaviour
{
    public GameObject leftDoor; // 왼쪽 문 오브젝트
    public GameObject rightDoor;
    public Vector3 leftDoorOpenRotation; // 왼쪽 문 열릴 때 회전값
    public Vector3 rightDoorOpenRotation; // 오른쪽 문 열릴 때 회전값
    public Vector3 leftDoorClosedRotation; // 왼쪽 문 닫힌 상태 회전값
    public Vector3 rightDoorClosedRotation; // 오른쪽 문 닫힌 상태 회전값
    public float openCloseSpeed = 2f; // 문 열고 닫는 속도
    public float interactionDistance = 2.5f; // 플레이어와 문 사이의 상호작용 거리
    public KeyCode interactionKey = KeyCode.E; // 상호작용 키
    public GameObject pressButtonUI; // "OPEN THE DOOR [E]" UI

    private bool isDoorOpen = false;

    void Start()
    {
        // pressButtonUI 연결 확인
        if (pressButtonUI == null)
        {
            Debug.LogError("Press Button UI가 연결되지 않았습니다.");
        }
    }

    void Update()
    {
        if (Camera.main == null)
        {
            Debug.LogError("Main Camera가 설정되지 않았습니다.");
            return;
        }

        float distance = Vector3.Distance(Camera.main.transform.position, transform.position);

        // 플레이어가 문에 가까이 있고 장애물이 없을 때
        if (distance <= interactionDistance && IsDirectLineOfSight())
        {
            if (pressButtonUI != null)
            {
                pressButtonUI.SetActive(true);
            }

            // E 키를 눌렀을 때
            if (Input.GetKeyDown(interactionKey) && !isDoorOpen)
            {
                StartCoroutine(OpenAndCloseDoor());
            }
        }
        else
        {
            if (pressButtonUI != null)
            {
                pressButtonUI.SetActive(false);
            }
        }
    }

    private IEnumerator OpenAndCloseDoor()
    {
        isDoorOpen = true;

        // 문 열기
        StartCoroutine(RotateDoor(leftDoor, Quaternion.Euler(leftDoorOpenRotation)));
        StartCoroutine(RotateDoor(rightDoor, Quaternion.Euler(rightDoorOpenRotation)));
        yield return new WaitForSeconds(3f); // 3초 대기

        // 문 닫기
        StartCoroutine(RotateDoor(leftDoor, Quaternion.Euler(leftDoorClosedRotation)));
        StartCoroutine(RotateDoor(rightDoor, Quaternion.Euler(rightDoorClosedRotation)));

        yield return new WaitForSeconds(openCloseSpeed);
        isDoorOpen = false;
    }

    private IEnumerator RotateDoor(GameObject door, Quaternion targetRotation)
    {
        if (door == null)
        {
            Debug.LogError("Door 오브젝트가 설정되지 않았습니다.");
            yield break;
        }

        Quaternion initialRotation = door.transform.localRotation;
        float elapsedTime = 0f;

        while (elapsedTime < openCloseSpeed)
        {
            door.transform.localRotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / openCloseSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        door.transform.localRotation = targetRotation;
    }

    // 플레이어와 문 사이에 장애물이 없는지 확인
    private bool IsDirectLineOfSight()
    {
        if (Camera.main == null) return false;

        Vector3 directionToPlayer = Camera.main.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // 거리 0일 경우 방어 처리
        if (distanceToPlayer <= 0.01f) return false;

        // 레이캐스트로 장애물 확인 (Default 레이어를 기본으로 사용)
        if (Physics.Raycast(transform.position, directionToPlayer.normalized, distanceToPlayer))
        {
            // 장애물이 있으면 false
            return false;
        }

        // 장애물이 없으면 true
        return true;
    }
}
