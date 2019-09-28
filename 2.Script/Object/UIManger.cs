using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 점프 구간에서 이미지를 켜서 참고해서 살수 있도록 돕는 매니저
public class UIManger : MonoBehaviour
{
    public Renderer[] UIRenderers;

    public int maxCount = 0;
    public int count = 0;

    public int[] showEventNum;

    private void Awake()
    {
        maxCount = 0;
        count = 0;
        var tmpCount = transform.childCount;
        if (UIRenderers.Length == 0)
        {
            UIRenderers = new Renderer[tmpCount];

            for (int i = 0; i < tmpCount; i++)
            {
                UIRenderers[i] = transform.GetChild(i).transform.GetComponent<Renderer>();
            }
        }
        maxCount = tmpCount;
    }

    private void Start()
    {
        Initialization_Process();
    }

    private void Initialization_Process()
    {
        foreach (var item in UIRenderers)
        {
            var tmpColor = item.material.color;
            tmpColor.a = 0;
            item.material.color = tmpColor;
        }
    }

    public bool ShowUIBoard(int showNum)
    {
        foreach (var item in showEventNum)
        {
            if (showNum == item || showNum == item + 1)
            {
                if (showNum == item && count != maxCount)
                {
                    var tmpColor = UIRenderers[count].material.color;
                    tmpColor.a = 1;
                    UIRenderers[count].material.color = tmpColor;
                    ++count;
                    return true;
                }
                else
                {
                    if (count == maxCount)
                    {
                        return false;
                    }
                    var tmpColor = UIRenderers[count].material.color;
                    tmpColor.a = 0;
                    UIRenderers[count].material.color = tmpColor;
                }
            }
        }

        return false;
    }
}