using UnityEngine;
using UnityEngine.UI;

public class KeypadController : MonoBehaviour
{
    public GameObject keypadUI; // 확대된 키패드 UI 패널
    public Text inputDisplay; // 입력된 비밀번호를 표시할 Text
    public string correctPassword = "1234"; // 올바른 비밀번호 설정
    private string enteredPassword = ""; // 현재 입력된 비밀번호

    public GameObject door; // 열릴 문 오브젝트 (doord_v2_left)

    void Start()
    {
        keypadUI.SetActive(false); // 처음에는 키패드 UI 비활성화
    }

    // 키패드 클릭 시 UI 표시
    void OnMouseDown()
    {
        keypadUI.SetActive(true); // 키패드를 클릭하면 UI가 활성화되고 확대됨
        enteredPassword = ""; // 비밀번호 초기화
        inputDisplay.text = ""; // 입력 표시 초기화
    }

    // 각 버튼이 호출할 함수
    public void AddDigit(string digit)
    {
        if (enteredPassword.Length < 4)
        {
            enteredPassword += digit;
            inputDisplay.text = enteredPassword; // 입력된 숫자 표시
        }
    }

    // Enter 버튼이 호출할 함수
    public void EnterPassword()
    {
        if (enteredPassword == correctPassword)
        {
            OpenDoor(); // 올바른 비밀번호이면 문 열기
        }
        else
        {
            Debug.Log("Incorrect Password"); // 틀린 비밀번호 알림
        }
        keypadUI.SetActive(false); // 비밀번호 입력 후 UI 닫기
    }

    // 문 열기 함수
    void OpenDoor()
    {
        door.transform.Rotate(0, 90, 0); // 예시로 문을 90도 회전시켜서 열기
        Debug.Log("Door is now open!");
    }
}
