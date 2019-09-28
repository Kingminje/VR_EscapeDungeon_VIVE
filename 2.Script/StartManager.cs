using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 스타트 씬 관리하는 매니저
public class StartManager : MonoBehaviour
{
    public Canvas UICanvas;

    public Camera mainCamera = null;

    public static float rayLength = 500f;

    public Image loadImg;
    public Image pointImg;

    public Image dot;

    public bool isPoint = false;

    public bool scenecheck = false;

    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        loadImg = GameObject.FindWithTag("Shootable").GetComponent<Image>();
        pointImg = GameObject.FindWithTag("Finish").GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        StartRay();
        StartCoroutine(PointLoading());

        if (scenecheck == true)
        {
            StopAllCoroutines();
            scenecheck = false;
            Debug.Log("sss");
            SceneManager.LoadScene(1);
        }
    }

    public void StartRay()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            DotShow(hit);

            var hit_tag = hit.transform.tag;

            if (hit_tag == "Start")
            {
                isPoint = true;
            }
            else
            {
                isPoint = false;
            }
        }
    }

    private IEnumerator NowLoading()
    {
        float timer = 0.0f;

        while (isPoint == true)
        {
            yield return null;
            timer += Time.deltaTime;

            loadImg.fillAmount = Mathf.Lerp(loadImg.fillAmount, 1f, timer);
            if (loadImg.fillAmount >= 1.0f)
            {
                scenecheck = true;
            }
        }
        while (isPoint == false)
        {
            yield return null;
            timer += Time.deltaTime;

            loadImg.fillAmount = Mathf.Lerp(loadImg.fillAmount, 0f, timer);
        }
    }

    private IEnumerator PointLoading()
    {
        float timer = 0.0f;

        while (isPoint == true)
        {
            yield return null;
            timer += Time.deltaTime * 0.5f;

            pointImg.fillAmount = Mathf.Lerp(loadImg.fillAmount, 1f, timer);
            if (pointImg.fillAmount >= 1f)
            {
                StartCoroutine(NowLoading());
            }
        }
        while (isPoint == false)
        {
            yield return null;
            pointImg.fillAmount = 0f;
        }
    }

    public void DotShow(RaycastHit hit)
    {
        var playerTr = mainCamera.transform.position;

        var tmpImageSizeRe = playerTr - UICanvas.transform.position;

        tmpImageSizeRe.y *= 10;
        var rectTr = dot.gameObject.GetComponent<RectTransform>();

        UICanvas.transform.LookAt(playerTr);
        UICanvas.transform.position = hit.point;
        dot.enabled = true;
    }
}