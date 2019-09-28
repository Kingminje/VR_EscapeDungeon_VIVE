using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 움직이는 발판에 사용.
public class DirectMovement : MonoBehaviour
{
    private float time = 0f;
    public float speed = 2f;
    public float compensate;
    public Vector3 MovePoint;

    public Transform player;

    public float min, max;

    private void Awake()
    {
        player = transform;
        //compensate = player.parent.transform.position.z;
        Debug.Log(player.position);
    }

    private void FixedUpdate()
    {
        DirectMove(transform);
    }

    // 지정된 범위 내에서 앞 뒤로 움직임
    private void DirectMove(Transform moveTaget)
    {
        //time += Time.deltaTime;
        if (player.transform.localPosition.z > min/* + compensate*/)
        {
            MovePoint = new Vector3(0f, 0f, -1f);
        }
        else if (player.transform.localPosition.z < max/* + compensate*/)
        {
            MovePoint = new Vector3(0f, 0f, 1f);
        }
        moveTaget.Translate(MovePoint * speed * Time.deltaTime);
    }
}