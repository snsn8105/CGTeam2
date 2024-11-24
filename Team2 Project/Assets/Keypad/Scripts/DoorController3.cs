using UnityEngine;
using System.Collections;

public class DoorController3 : MonoBehaviour
{
    [Header("Door References")]
    public Transform doorLeft;     // 왼쪽 문 Transform

    [Header("Rotation Settings")]
    public float rotationSpeed = 2f; // 문 회전 속도
    public float leftDoorTargetY = 270f; // 왼쪽 문 목표 Y 회전값

    private bool doorsOpened = false; // 문이 열렸는지 여부 확인

    public void OpenDoors()
    {
        if (!doorsOpened)
        {
            doorsOpened = true;
            StartCoroutine(RotateDoor(doorLeft, leftDoorTargetY)); // 왼쪽 문 회전
        }
    }

    private IEnumerator RotateDoor(Transform door, float targetY)
    {
        // 초기 회전값
        Quaternion startRotation = door.rotation;
        // 목표 회전값
        Quaternion targetRotation = Quaternion.Euler(door.eulerAngles.x, targetY, door.eulerAngles.z);

        float elapsedTime = 0f;
        while (elapsedTime < 1f / rotationSpeed)
        {
            elapsedTime += Time.deltaTime * rotationSpeed;
            // 부드럽게 회전
            door.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime);
            yield return null;
        }

        // 최종 회전값 설정
        door.rotation = targetRotation;
    }
}
