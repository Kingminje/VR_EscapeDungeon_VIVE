using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 눈 깜박거리는 이벤트
public class FadeInOut : MonoBehaviour
{
    private Color _fadeColor;

    public Image _fadeImage;
    public float _fadeTime;
    public float _waitTime;

    public bool _fadeIn = false;
    public bool _fadeOut = false;

    public CameraShake cameraShake;
    public float shakeamount, shakedurtion = 0f;

    public PlayerController pCont;

    private GameManager gameManager;
    private GameObject textObj;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        pCont = gameObject.GetComponent<PlayerController>();
    }

    // 어두운 화면 다시 되돌림
    private IEnumerator FadeIn(float fadeTime)
    {
        ChangeSetting();

        _fadeIn = false;

        yield return new WaitForSeconds(_waitTime);

        StartCoroutine(F_in(fadeTime));

        pCont.enabled = true;

        pCont.gameManager.SoundStop();

        TextManger.Condition_Pause = false;
        pCont.eventManager.textManger.TextNext();
    }

    // 어두운 화면 으로 만듬
    private IEnumerator FadeOut(float fadeTime)
    {
        _fadeOut = false;
        _fadeIn = true;
        yield return new WaitForSeconds(0f);

        pCont.enabled = false;

        StartCoroutine(F_out(fadeTime));

        if (_fadeIn == true)
            StartCoroutine(FadeIn(_fadeTime));
    }

    private void ChangeSetting()
    {
        // 이전 래이캐스트 코드에 묶인 손 풀때 쓰던 코드를 이쪽으로 옮겨서 페이드인 할때 작동.
        gameManager.holdObj.SetActive(false);

        gameManager.startPoint.SetActive(true);
        GameObject item = GameObject.FindGameObjectWithTag("Item 1");
        Destroy(item);
    }

    private IEnumerator Blink()
    {
        pCont.enabled = false;
        yield return new WaitForSeconds(1f);
        StartCoroutine(F_in(1f));
        yield return new WaitForSeconds(1f);
        StartCoroutine(F_out(1f));
        yield return new WaitForSeconds(1f);
        StartCoroutine(F_in(1f));

        pCont.enabled = true;
        StopCoroutine(Blink());
    }

    public IEnumerator F_out(float fadeTime)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
            _fadeColor.a = Mathf.Clamp01(elapsedTime / fadeTime);
            _fadeImage.color = _fadeColor;
        }
    }

    private IEnumerator F_in(float fadeTime)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
            _fadeColor.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            _fadeImage.color = _fadeColor;
        }
    }

    public void DoFadeInOut()
    {
        if (_fadeOut == true)
        {
            _fadeColor = new Color(0f, 0f, 0f, 0f);
            _fadeImage.color = _fadeColor;
            StartCoroutine(FadeOut(_fadeTime));
        }
    }

    public void BlinkEye()
    {
        StartCoroutine(Blink());
    }
}