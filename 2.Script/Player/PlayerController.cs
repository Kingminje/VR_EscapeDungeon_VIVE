using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HTC.UnityPlugin.Vive;

// 플레이어 캐릭터에 대한 전반적인 조작을 담당함.
public partial class PlayerController : MonoBehaviour
{
    public static Transform playerCmera = null;

    public Camera mainCamera;
    public RayCastEye rayCastEye;

    public TestLinerender tl; // 추가
    public GameManager gameManager;
    public EventManager eventManager;
    public JointController jointControllor;
    public HeroineEventSetting heroineEventSetting;

    private float adjustment = 1; // 오더 트랜스폼 값 위로 올라가는거 없애줌
    public int jointPoint; // 매달리는 무브 포인트 값 저장
    public int MoveBoxPoint; // 무브 포인트 플레이어 무브에서 사용함

    private bool time = false;

    public bool testMod = false;

    private void Awake()
    {
        if (playerCmera == null)
        {
            playerCmera = transform.GetComponentInChildren<Camera>().transform;
            mainCamera = FindObjectOfType<Camera>();
            rayCastEye = transform.GetComponent<RayCastEye>();
            tl = FindObjectOfType<TestLinerender>();
            gameManager = FindObjectOfType<GameManager>();
            rc = transform.GetComponent<Rigidbody>();
            eventManager = FindObjectOfType<EventManager>();
        }
    }

    // 인보크로 실행중 사유: pc에서 시뮬레이터로 돌릴때는 문제가 없지만
    // vr 컨트롤러로 호출시 텍스트가 2개씩 넘어가서 제한
    private void TimeFalse()
    {
        time = false;
    }

    private void FixedUpdate()
    {
        if (moving == false && lineMoving == false)
        {
            rayCastEye.EyeRayCast();
        }

        if (lineMoving == true)
        {
            tl.OneHandLinePosSet(rayCastEye.currentTargetTr);
        }

        if (moving == true)
        {
            MoveTo(rayCastEye.currentTargetTr.transform.position, 5f);
        }

        // 텍스트매니저가 퍼즈 상태가 아니라면..
        if (Input.GetMouseButtonDown(0) && TextManger.Condition_Pause == false && time == false
            || ViveInput.GetPressDownEx(HandRole.RightHand, ControllerButton.Trigger) && TextManger.Condition_Pause == false && time == false)
        {
            time = true;
            Invoke("TimeFalse", 0.5f);

            var tmp = TextOutput.cnt;
            Debug.Log(tmp);
            eventManager.TextToNext();
        }
    }

    // 기둥에 플레이어를 매달릴때 리지드바디 프리즈 포지션과 로테이션을 조정함.
    public void JointOn(GameObject player, Rigidbody rc = null)
    {
        rc = this.rc;
        rc.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        jointControllor.JointOn(player, rc);
    }

    // 기둥에 플레이어를 매달릴때 리지드바디 프리즈 포지션과 로테이션을 조정한 값을 원래대로 돌림.
    public void JointOff(GameObject player)
    {
        rc.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
}