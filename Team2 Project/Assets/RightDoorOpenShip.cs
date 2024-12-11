using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RightDoorOpenShip : MonoBehaviour
{
    public GameObject rightDoor; // 오른쪽 문 오브젝트
    public Vector3 openRotation; // 문 열릴 때의 회전값
    public Vector3 closedRotation; // 문 닫힐 때의 회전값
    public float openCloseSpeed = 2f; // 문 열고 닫는 속도
    public float interactionDistance = 2.5f; // 플레이어와 문 사이의 상호작용 거리
    public KeyCode interactionKey = KeyCode.E; // 상호작용 키
    public GameObject pressButtonUI; // "OPEN THE DOOR [E]" UI

    private bool isDoorOpen = false;

    void Start()
    {
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

        // 플레이어가 문에 가까이 있을 때
        if (distance <= interactionDistance)
        {
            if (pressButtonUI != null)
            {
                pressButtonUI.SetActive(true);
            }

            // E 키를 눌렀을 때
            if (Input.GetKeyDown(interactionKey))
            {
                if (!isDoorOpen)
                {
                    OpenDoor();
                }
                else
                {
                    CloseDoor();
                }
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

    private void OpenDoor()
    {
        isDoorOpen = true;
        StartCoroutine(RotateDoor(rightDoor, Quaternion.Euler(openRotation)));
    }

    private void CloseDoor()
    {
        isDoorOpen = false;
        StartCoroutine(RotateDoor(rightDoor, Quaternion.Euler(closedRotation)));
    }

    private IEnumerator RotateDoor(GameObject door, Quaternion targetRotation)
    {
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
}
