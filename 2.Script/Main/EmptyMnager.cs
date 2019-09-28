using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EmptyMnager : MonoBehaviour
{
    private void Awake()
    {
        // 초기화 스태틱값
        PlayerController.MovePointNum = -1;
        TextOutput.cnt = 0;
        TextManger.Condition_Pause = false;
        EventManager.stack = -1;
        SceneManager.LoadScene(2);
    }
}