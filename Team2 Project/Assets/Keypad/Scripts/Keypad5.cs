
using UnityEngine;
using TMPro;

public class Keypad5 : MonoBehaviour
{
    [Header("Keypad Settings")]
    private string correctPassword = "0479"; // 정답 비밀번호
    private string currentInput = "";       // 현재 입력된 비밀번호
    public TMP_Text displayText;            // 입력값 표시 TextMeshPro

    [Header("Door Controller")]
    public DoorController5 doorController;  // 문 회전 컨트롤러

   public void AddInput(string input)
{
    if (input == "enter")
    {
        CheckPassword(); // 비밀번호 확인
    }
    else
    {
        // 입력값이 9자리보다 크면 추가 입력 제한
        if (currentInput.Length >= 9) return;

        currentInput += input;
        UpdateDisplay();
    }
}


    private void UpdateDisplay()
    {
        displayText.text = currentInput;
    }

   private void CheckPassword()
{
    if (currentInput == correctPassword)
    {
        Debug.Log("Access Granted!");
        displayText.text = "ACCESS GRANTED"; // 성공 메시지 표시
        doorController.OpenDoors(); // 문 열기
    }
    else
    {
        Debug.Log("Access Denied!");
        displayText.text = "ACCESS DENIED"; // 실패 메시지 표시
        currentInput = ""; // 입력값 초기화
        Invoke(nameof(ClearDisplay), 1.5f); // 1.5초 후 UI 초기화
    }
}

private void ClearDisplay()
{
    currentInput = ""; // 입력값 초기화
    UpdateDisplay();   // 화면에 초기화된 값 표시
}

}
