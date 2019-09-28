using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointNum : MonoBehaviour
{
    // movePoint에 순서대로 사용할수 있도록 초기화 해주는 코드 ) 인스펙터 순서대로 들어감
    [SerializeField]
    private int num = 0;

    public int MovePointNumber
    {
        get { return this.num; }
        set { num = value; }
    }
}