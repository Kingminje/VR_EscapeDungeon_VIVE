using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public GameObject Gate;

    // Update is called once per frame
    private void Update()
    {
        Gate.transform.position = Vector3.Lerp(Gate.transform.position, new Vector3(Gate.transform.position.x, -20f, Gate.transform.position.z), Time.deltaTime / 10f);
    }
}