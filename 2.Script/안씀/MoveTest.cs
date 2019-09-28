using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour {

    public float speed = 1000f;
    public Rigidbody rig;
    public Vector3 move;

    public void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
    }

    public void Move(float h, float v)
    {
        move.Set(h, 0, v);
        move = move.normalized * speed * Time.deltaTime;

        rig.MovePosition(transform.position + move);
    }

}
