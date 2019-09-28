using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextOutput : MonoBehaviour
{
    // 변경 변수
    public float delay;

    public float Skip_Delay;
    public static int cnt;

    public GameObject textPanel;

    private string purple = "<color=\"purple\">";

    // 타이핑 효과 변수
    public string[] fullText = null;

    public int dialog_cnt;

    private string currentText;

    // 타이핑 확인 변수
    public bool text_exit;

    public bool text_full;
    public bool text_cut;

    private void Start()
    {
        textPanel = GameObject.Find("Panel");
    }

    private void Update()
    {
        if (text_exit == true)
        {
            gameObject.SetActive(false);
        }
    }

    public void End_Typing()
    {
        if (text_full == true)
        {
            cnt++;
            text_full = false;
            text_cut = false;
            StartCoroutine(ShowText(fullText));
        }
        else
        {
            text_cut = true;
        }
    }

    //텍스트 시작 호출
    public void Get_Typing(int _dialog_count, string[] _fullText)
    {
        // 재사용을 위한 변수초기화
        text_exit = false;
        text_full = false;
        text_cut = false;
        cnt = 0;

        // 변수 불러오기
        dialog_cnt = _dialog_count;
        fullText = new string[dialog_cnt];
        fullText = _fullText;

        StartCoroutine(ShowText(fullText));
    }

    private IEnumerator ShowText(string[] _fullText)
    {
        // 모든 텍스트 종료
        if (cnt >= dialog_cnt)
        {
            text_exit = true;
            StopCoroutine("ShowText");
        }
        else
        {
            // 초기화
            currentText = "";

            // 출력 시작
            for (int i = 0; i < _fullText[cnt].Length; i++)
            {
                //타이핑 중도 탈출
                if (text_cut == true)
                {
                    break;
                }
                // 단어 출력
                currentText = _fullText[cnt].Substring(0, i + 1);
                currentText = "";
                this.GetComponent<TextMeshProUGUI>().text = currentText;
                yield return new WaitForSeconds(delay);
            }

            //탈출 시 모든 문자 출력
            Debug.Log("Typing 종료");
            this.GetComponent<TextMeshProUGUI>().text = _fullText[cnt];
            yield return new WaitForSeconds(Skip_Delay);

            // 스킵 지연 후 종료
            Debug.Log("Enter 대기");
            text_full = true;
        }
    }

    public void End_NowText()
    {
        if (text_full == true)
        {
            if (cnt >= fullText.Length)
            {
                text_exit = true;
            }

            cnt++;
            text_full = false;
            text_cut = false;
            this.GetComponent<TextMeshProUGUI>().text = fullText[cnt];

            PanelSizeChange(fullText[cnt]);
            text_full = true;
            //StartCoroutine(Mod_ShowText(fullText));
        }
        else
        {
            text_cut = true;
        }
    }

    private void PanelSizeChange(string text)
    {

        string changeT = text.Replace(purple, "");

        if (changeT.Length > 12)
        {
            RectTransform panelRect = textPanel.GetComponent<RectTransform>();

            int a = changeT.Length - 12;
            float change = 0.24f + (0.015f * a);

            textPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(change, panelRect.sizeDelta.y);
        }
        else
        {
            textPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0.24f, 0.18f);
        }

        
    }

    public void Get_NowText(int _dialog_count, string[] _fullText)
    {
        // 재사용을 위한 변수초기화
        text_exit = false;
        text_full = false;
        text_cut = false;
        cnt = 0;

        // 변수 불러오기
        dialog_cnt = _dialog_count;
        fullText = new string[dialog_cnt];
        fullText = _fullText;

        StartCoroutine(Mod_ShowText(fullText));
    }

    private IEnumerator Mod_ShowText(string[] fullText)
    {
        if (cnt >= dialog_cnt)
        {
            text_exit = true;
            StopCoroutine("Mod_ShowText");
        }
        else
        {
            currentText = "";

            this.GetComponent<TextMeshProUGUI>().text = fullText[cnt];
            PanelSizeChange(fullText[cnt]);
        }
        yield return new WaitForSeconds(Skip_Delay);

        text_full = true;
    }
}