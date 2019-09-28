using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform Obj;

    private void Update()
    {
        transform.LookAt(2 * transform.position - Obj.position);
    }
}