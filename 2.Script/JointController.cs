using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 매달리는 장면에서 사용하는 컨트롤러
public class JointController : MonoBehaviour
{
    public int rc_ForcePower = 30; // 이 힘에 따라서 앞뒤로 흔드는 힘이 달라짐

    // 매달리는거 활성화
    public void JointOn(GameObject player, Rigidbody rc)
    {
        player.AddComponent<SpringJoint>();
        rc.AddForce(Vector3.forward * rc_ForcePower);
        Debug.Log("joint On 작동 했다");
    }

    // 매달리는거 풀림
    public void JointOff(GameObject player)
    {
        SpringJoint name = player.GetComponent<SpringJoint>();
        Destroy(name);

        Debug.Log("joint Off 작동 했다");
    }
}