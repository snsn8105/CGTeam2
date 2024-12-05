using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 전환을 위해 추가

public class DoorOpen : MonoBehaviour
{

    public float openCloseSpeed = 2f; // 문 열고 닫는 속도
    public float interactionDistance = 3.0f; // 플레이어와 문 사이의 상호작용 거리
    public KeyCode interactionKey = KeyCode.E; 
    public GameObject pressButtonUI; // "OPEN THE DOOR [G]" UI

    private bool isDoorOpen = false;
    private bool isPlayerNearby = false;

    void Update()
    {
        float distance = Vector3.Distance(Camera.main.transform.position, transform.position);

        // 플레이어가 문에 가까이 있을 때
        if (distance <= interactionDistance)
        {
            pressButtonUI.SetActive(true);

            // G 키를 눌렀을 때
            if (Input.GetKeyDown(interactionKey))
            {
                // 씬 전환
                SceneManager.LoadScene("spaceShip");
            }
        }
        else
        {
            pressButtonUI.SetActive(false);
        }
    }

    
}
