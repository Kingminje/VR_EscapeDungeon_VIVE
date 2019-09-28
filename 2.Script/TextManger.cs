using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// 텍스트 관련 이벤트 매니저 중요함.
public class TextManger : MonoBehaviour
{
    public HeroineEventSetting heroineEventSetting; // 임시

    //대사 본들~

    public string[] eventTexts_01;
    public string[] eventTexts_02;
    public string[] eventTexts_03_1;
    public string[] eventTexts_03_2;
    public string[] eventTexts_04;
    public string[] eventTexts_05;
    public string[] eventTexts_06_1;
    public string[] eventTexts_06_2;
    public string[] eventTexts_06_3;
    public string[] eventTexts_06_4;
    public string[] eventTexts_07_1;
    public string[] eventTexts_07_2;
    public string[] eventTexts_07_3;
    public string[] eventTexts_08;

    public GameObject panel;

    public TextOutput textOutput;

    private GameObject textObj;

    private static bool condition = false;

    public static bool Condition_Pause
    {
        set { condition = value; }
        get { return condition; }
    }

    private void Start()
    {
        textObj = GameObject.Find("TextMeshPro Text (1)"); // 텍스트 오브젝트
        textOutput.Get_NowText(eventTexts_01.Length, eventTexts_01);
    }

    private void LateUpdate()
    {
        if (TextManger.Condition_Pause)
        {
            TextEvent_Pause();
        }
        else
        {
            TextEvent_Restart();
        }
    }

    // 이벤트 매니저에서 사용하는 함수
    public void TextEventCheck(int movePointNum)
    {
        switch (movePointNum)
        {
            case 0:
                TextManger.Condition_Pause = false;
                TextEventProsser(eventTexts_02, eventTexts_02.Length);
                return;

            case 2:
                TextManger.Condition_Pause = false;
                TextEventProsser(eventTexts_03_1, eventTexts_03_1.Length);
                return;

            case 6:
                TextManger.Condition_Pause = false;
                TextEventProsser(eventTexts_03_2, eventTexts_03_2.Length);
                return;

            case 11:
                TextManger.Condition_Pause = false;
                TextEventProsser(eventTexts_04, eventTexts_04.Length);
                return;

            case 12:
                TextManger.Condition_Pause = false;
                TextEventProsser(eventTexts_05, eventTexts_05.Length);
                return;

            case 13:
                TextManger.Condition_Pause = false;
                TextEventProsser(eventTexts_06_1, eventTexts_06_1.Length);
                return;

            case 15:
                //2번 점프 뛴 후  // 이쪽이야.
                TextManger.Condition_Pause = false;
                TextEventProsser(eventTexts_06_2, eventTexts_06_2.Length);
                return;

            case 16:
                // 붕대 감고 올라가봐
                TextManger.Condition_Pause = false;
                TextEventProsser(eventTexts_06_3, eventTexts_06_3.Length);
                return;

            case 17:
                //우와아아앗 당황하지마 // 갈고리
                TextManger.Condition_Pause = false;
                TextEventProsser(eventTexts_06_4, eventTexts_06_4.Length);
                return;

            case 18:
                // 휴우,. 위험했어. // 착지 후
                TextManger.Condition_Pause = false;
                TextEventProsser(eventTexts_07_1, eventTexts_07_1.Length);
                return;

            case 19:
                //이번엔 타이밍 맞춰서 점프해야겠어 // moveBox 앞
                TextManger.Condition_Pause = false;
                TextEventProsser(eventTexts_07_2, eventTexts_07_2.Length);
                return;

            case 21:
                // 마피 저 앞에 출구가 보여!!/// moveBox건넌 뒤
                TextManger.Condition_Pause = false;
                TextEventProsser(eventTexts_07_3, eventTexts_07_3.Length);
                return;

            case 24:
                //드디어 도착했어 // 끝에지점
                TextManger.Condition_Pause = false;
                TextEventProsser(eventTexts_08, eventTexts_08.Length);
                return;

            default:
                return;
        }
    }

    private void TextEventProsser(string[] EventText, int lengh)
    {
        textOutput.gameObject.SetActive(true);
        textOutput.Get_NowText(lengh, EventText);
    }

    // 다음 대사 사용
    public void TextNext()
    {
        if (textOutput.text_exit == false)
        {
            // 나중에 지워라
            if (TextManger.Condition_Pause == false)
            {
                textOutput.End_NowText();
            }
        }
    }

    // 텍스트를 현재 상황에서 끔
    public void TextEvent_Pause()
    {
        textObj.SetActive(false);
    }

    // 텍스트를 현재 상황에서 켜서 진행하도록
    public void TextEvent_Restart()
    {
        textObj.SetActive(true);
    }
}