using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DoorButton : MonoBehaviour
{
    public GameObject leftDoor; // 왼쪽 문 오브젝트
    public Vector3 leftDoorOpenRotation; // 왼쪽 문 열릴 때 회전값
    public Vector3 closedRotation = Vector3.zero; // 닫힌 상태 회전값
    public float openCloseSpeed = 2f; // 문 열고 닫는 속도
    public float interactionDistance = 3.0f; // 플레이어와 문 사이의 상호작용 거리
    public KeyCode interactionKey = KeyCode.E; // 상호작용 키
    public GameObject pressButtonUI; // "OPEN THE DOOR [E]" UI
   

    private bool isDoorOpen = false;
    private bool isPlayerNearby = false;

    void Update()
    {
        float distance = Vector3.Distance(Camera.main.transform.position, transform.position);

        // 플레이어가 문에 가까이 있을 때
        if (distance <= interactionDistance)
        {
            pressButtonUI.SetActive(true);

            // E 키를 눌렀을 때
            if (Input.GetKeyDown(interactionKey) && !isDoorOpen)
            {
                StartCoroutine(OpenAndCloseDoor());
            }
        }
        else
        {
            pressButtonUI.SetActive(false);
        }
    }

    private IEnumerator OpenAndCloseDoor()
    {
        isDoorOpen = true;

        // 문 열기
        StartCoroutine(RotateDoor(leftDoor, Quaternion.Euler(leftDoorOpenRotation)));

        yield return new WaitForSeconds(3f); // 3초 대기

        // 문 닫기
        StartCoroutine(RotateDoor(leftDoor, Quaternion.Euler(closedRotation)));

        yield return new WaitForSeconds(openCloseSpeed);
        isDoorOpen = false;
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
