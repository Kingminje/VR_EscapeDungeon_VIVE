using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject obj;

    private void Update()
    {
        transform.eulerAngles = new Vector3(transform.rotation.x, 0.0f, transform.rotation.z);
        transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - 1, obj.transform.position.z);
    }
}