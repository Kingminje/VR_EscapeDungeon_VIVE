using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrSetting : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        var tmp = transform.position;
        tmp.y = -1f;
        transform.position = tmp;
    }
}