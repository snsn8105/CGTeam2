/*using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace NavKeypad
{
    public class Keypad : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private UnityEvent onAccessGranted;
        [SerializeField] private UnityEvent onAccessDenied;
        [Header("Combination Code (9 Numbers Max)")]
        [SerializeField] private int keypadCombo = 12345;

        public UnityEvent OnAccessGranted => onAccessGranted;
        public UnityEvent OnAccessDenied => onAccessDenied;

        [Header("Settings")]
        [SerializeField] private string accessGrantedText = "Granted";
        [SerializeField] private string accessDeniedText = "Denied";

        [Header("Visuals")]
        [SerializeField] private float displayResultTime = 1f;
        [Range(0, 5)]
        [SerializeField] private float screenIntensity = 2.5f;
        [Header("Colors")]
        [SerializeField] private Color screenNormalColor = new Color(0.98f, 0.50f, 0.032f, 1f); //orangy
        [SerializeField] private Color screenDeniedColor = new Color(1f, 0f, 0f, 1f); //red
        [SerializeField] private Color screenGrantedColor = new Color(0f, 0.62f, 0.07f); //greenish
        [Header("SoundFx")]
        [SerializeField] private AudioClip buttonClickedSfx;
        [SerializeField] private AudioClip accessDeniedSfx;
        [SerializeField] private AudioClip accessGrantedSfx;
        [Header("Component References")]
        [SerializeField] private Renderer panelMesh;
        [SerializeField] private TMP_Text keypadDisplayText;
        [SerializeField] private AudioSource audioSource;


        private string currentInput;
        private bool displayingResult = false;
        private bool accessWasGranted = false;

        private void Awake()
        {
            ClearInput();
            panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity);
        }


        //Gets value from pressedbutton
        public void AddInput(string input)
        {
            audioSource.PlayOneShot(buttonClickedSfx);
            if (displayingResult || accessWasGranted) return;
            switch (input)
            {
                case "enter":
                    CheckCombo();
                    break;
                default:
                    if (currentInput != null && currentInput.Length == 9) // 9 max passcode size 
                    {
                        return;
                    }
                    currentInput += input;
                    keypadDisplayText.text = currentInput;
                    break;
            }

        }
        public void CheckCombo()
        {
            if (int.TryParse(currentInput, out var currentKombo))
            {
                bool granted = currentKombo == keypadCombo;
                if (!displayingResult)
                {
                    StartCoroutine(DisplayResultRoutine(granted));
                }
            }
            else
            {
                Debug.LogWarning("Couldn't process input for some reason..");
            }

        }

        //mainly for animations 
        private IEnumerator DisplayResultRoutine(bool granted)
        {
            displayingResult = true;

            if (granted) AccessGranted();
            else AccessDenied();

            yield return new WaitForSeconds(displayResultTime);
            displayingResult = false;
            if (granted) yield break;
            ClearInput();
            panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity);

        }

        private void AccessDenied()
        {
            keypadDisplayText.text = accessDeniedText;
            onAccessDenied?.Invoke();
            panelMesh.material.SetVector("_EmissionColor", screenDeniedColor * screenIntensity);
            audioSource.PlayOneShot(accessDeniedSfx);
        }

        private void ClearInput()
        {
            currentInput = "";
            keypadDisplayText.text = currentInput;
        }

        private void AccessGranted()
        {
            accessWasGranted = true;
            keypadDisplayText.text = accessGrantedText;
            onAccessGranted?.Invoke();
            panelMesh.material.SetVector("_EmissionColor", screenGrantedColor * screenIntensity);
            audioSource.PlayOneShot(accessGrantedSfx);
        }

    }
} */
using UnityEngine;
using TMPro;

public class Keypad : MonoBehaviour
{
    [Header("Keypad Settings")]
    public string correctPassword = "1593"; // 정답 비밀번호
    private string currentInput = "";       // 현재 입력된 비밀번호
    public TMP_Text displayText;            // 입력값 표시 TextMeshPro

    [Header("Door Controller")]
    public DoorController doorController;  // 문 회전 컨트롤러

    public void AddInput(string input)
    {
        if (input == "enter")
        {
            CheckPassword(); // 비밀번호 확인
        }
        else
        {
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
            doorController.OpenDoors(); // 문 열기
        }
        else
        {
            Debug.Log("Access Denied!");
            currentInput = ""; // 입력값 초기화
            UpdateDisplay();
        }
    }
}
