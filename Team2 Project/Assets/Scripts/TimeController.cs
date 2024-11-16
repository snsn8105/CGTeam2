using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    public bool isCountDown = true;          // true= 카운트 다운으로 시간 측정
    public float gameTime = 0;               // 게임의 최대 시간
    public bool isTimeOver = false;          // true= 타이머 정지
    public float displayTime = 0;            // 표시 시간
    public TextMeshProUGUI timerText;        // TextMeshProUGUI 컴포넌트

    private float times = 0;                 // 현재 시간

    void Start()
    {
        if (isCountDown)
        {
            // 카운트다운 초기화
            displayTime = gameTime;
        }
    }

    void Update()
    {
        if (!isTimeOver)
        {
            times += Time.deltaTime;

            if (isCountDown)
            {
                // 카운트다운
                displayTime = gameTime - times;
                if (displayTime <= 0.0f)
                {
                    displayTime = 0.0f;
                    isTimeOver = true;
                }
            }
            else
            {
                // 카운트업
                displayTime = times;
                if (displayTime >= gameTime)
                {
                    displayTime = gameTime;
                    isTimeOver = true;
                }
            }

            // TextMeshPro 업데이트
            UpdateTimerText();
            Debug.Log("TIMES: " + displayTime);
        }
    }

    void UpdateTimerText()
    {
        // 시간을 "00:00.0" 형식으로 표시
        int minutes = Mathf.FloorToInt(displayTime / 60);
        float seconds = displayTime % 60;
        timerText.text = $"{minutes:00}:{seconds:00.0}";
    }
}
