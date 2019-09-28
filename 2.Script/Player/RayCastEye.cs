using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using HTC.UnityPlugin.Vive;

// 플레이어 카메라에서 시선으로 컨트롤하는 모든 함수가 들어감
public class RayCastEye : MonoBehaviour
{
    public Camera mainCamera = null;
    public Transform currentTargetTr = null; // 레이 지정한 타겟 트렌스폼 변수 ,LBJ
    public PlayerController playerController;
    public GameManager gameManager;
    public ViveControllerAdvens viveControllerAdvens;

    public EventManager eventManager;

    public Renderer currentRenderer = null;
    public Material ChangeMaterial; // 시선으로 가리킬 시 메트리얼 변경하는
    public Material currentMateial; // 기존 메트리얼 저장

    public Material MovePointCrrentMaterial;
    public Material MovePointSelectMaterial;
    public Renderer tmpRenderer = null; //

    private bool movePointChngeCheck = false;

    public static float rayLength = 500f; // ray 거리 조절할때 쓰는 변수.

    public LayerMask exceptLayer;

    public GameObject hitObj = null;

    public string tagstr = null;

    private void Awake()
    {
        mainCamera = FindObjectOfType<Camera>(); // 추후 카메라 설정 변경해야함
        playerController = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
        eventManager = FindObjectOfType<EventManager>();
    }

    // 카메라를 통해서 레이져를 쏘는 함수
    public void EyeRayCast()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        // 레이져가 무브포인트 주변을 닿았을때 도트 생성 하고 메트리얼 변경해주는 역활
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            var hit_tag = hit.transform.tag;

            if (hit_tag == "MoveEnd" || hit_tag == "Item 1" || hit_tag == "hold" || hit_tag == "PointJoin")
            {
                gameManager.DotShow(hit);

                if (hit_tag == "MoveEnd" || hit_tag == "PointJoin")
                {
                    if (movePointChngeCheck == true)
                    {
                        movePointChngeCheck = false;
                    }

                    SelectMaterialChenged(hit);
                }

                //Debug.Log(hit.transform.tag);

                if (hit_tag != "PointJoin")
                {
                    //Debug.Log("위가 맞어");
                    if (hit.transform.parent != null && hit.transform.parent.tag == "MovePoint" || hit.transform.parent != null && hit.transform.parent.tag == "JumpPoint")
                    {
                    }
                    else
                    {
                        ChengeMaterialOn(hit);
                    }

                    // 민 상속 못하면 어떻다? 이렇게 해야한다 ㅠㅠ
                    if (EventManager.stack == 1 && TextManger.Condition_Pause == true && hit_tag == "Item 1")
                    {
                        eventManager.animationManager.AnimationEventCheck(PlayerController.MovePointNum, TextOutput.cnt);
                        TextManger.Condition_Pause = false;
                    }
                }
            }
        }
        else
        {
            ChengeMaterialOff(hit);
            SelectMaterialChengedNull();
            gameManager.DotDisabled();
        }

        // 시선으로 그곳이 우브로 해야하는곳인지 점프하는곳인지 판정하고 실행
        if (Physics.Raycast(ray, out hit, rayLength, ~exceptLayer))
        {
            currentTargetTr = hit.transform; // Hit 트렌스폼 저장.

            // 텍스트매니저 퍼즈 상태 체크 추가
            if (Input.GetMouseButtonDown(0) && playerController.moving == false
                || ViveInput.GetPressDownEx(HandRole.RightHand, ControllerButton.Trigger) && playerController.moving == false)
            {
                //viveControllerAdvens.ControllerToVibration(0, 500); // vive컨트롤러 양손 진동 500마이크로초로 진동 작동 안함

                gameManager.DotDisabled(); // 이동중 카메라에 보이는 조준점을 끔.
                hitObj = hit.collider.gameObject;

                // 시선 레이에 맞은 게임 오브젝트 테그를 가져와서 스트링값 비교해서 맞는거에 함수 실행
                switch (hit.collider.gameObject.tag)
                {
                    case "MovePoint":
                        if (EventManager.stack != 4)
                        {
                            Debug.Log("stak 1로 변환 시작");
                            eventManager.AnimationEventCheck(PlayerController.MovePointNum, TextOutput.cnt);
                            //eventManager.textManger.TextNext();
                            return;
                        }

                        if (PlayerController.MovePointNum == playerController.jointPoint)
                            playerController.JointOff(transform.gameObject);

                        ChengeMaterialMoveOff();

                        playerController.lineMoving = true; // 라인렌더러 작동 시작.

                        //heroineEvent.PlayerSee();

                        break;

                    case "JumpPoint":
                        if (PlayerController.MovePointNum == playerController.jointPoint)
                            playerController.JointOff(transform.gameObject);
                        if (EventManager.playerStop == true)
                            EventManager.playerStop = false;

                        playerController.moving = true;
                        playerController.isJump = true;
                        break;

                    case "Item 1":
                        Debug.Log(EventManager.stack);
                        if (EventManager.stack != 2)
                        {
                            return;
                        }

                        Debug.Log("아이템1 집기");

                        EventManager.stack = 3;
                        TextManger.Condition_Pause = false;

                        playerController.isItem = true;
                        playerController.lineMoving = true;

                        break;

                    case "hold":
                        if (playerController.isItem == true)
                        {
                            if (EventManager.stack == 3 && TextOutput.cnt == 13)
                            {
                                playerController.gameManager.SoundToPlay((int)AudioClipName.SuckSuck, true);
                                gameObject.GetComponent<FadeInOut>()._fadeOut = true;
                                gameObject.GetComponent<FadeInOut>().DoFadeInOut();
                                playerController.isItem = false;
                                gameManager.hold_out = true;

                                TextManger.Condition_Pause = false;
                            }
                            // 묶인 손 푸는 부분에 페이드 인 아웃 기능 옮김. 그와 관련 오브젝트 활성/비활성 기능을 페이드인아웃 코드에 옮김.
                        }
                        break;
                }
            }
        }
    }

    // 메트리얼 변경
    private void ChengeMaterialOn(RaycastHit hit)
    {
        if (currentRenderer == null && hit.transform.parent != null)
        {
            var tmpRenderer = hit.transform.parent.transform.GetComponent<Renderer>();

            currentMateial = tmpRenderer.material;

            currentRenderer = tmpRenderer;

            tmpRenderer.material = ChangeMaterial;

            ChangeMaterial = currentMateial;
        }
        else if (currentRenderer == null)
        {
            var tmpRenderer = hit.transform.transform.GetComponent<Renderer>();

            currentMateial = tmpRenderer.material;

            currentRenderer = tmpRenderer;

            tmpRenderer.material = ChangeMaterial;

            ChangeMaterial = currentMateial;
        }
    }

    // 메트리얼 변경 되돌림
    private void ChengeMaterialOff(RaycastHit hit)
    {
        if (currentRenderer != null)
        {
            ChangeMaterial = currentRenderer.material;

            currentRenderer.material = currentMateial;

            currentRenderer = null;
        }
    }

    private void ChengeMaterialMoveOff()
    {
        if (currentRenderer != null)
        {
            ChangeMaterial = currentRenderer.material;
            currentRenderer = null;
        }
    }

    private void SelectMaterialChenged(RaycastHit hit)
    {
        if (hit.transform.parent == null && movePointChngeCheck == false)
        {
            Debug.Log("작동은 했는데요 부모 없는 무브조인트 오브젝트에");
            movePointChngeCheck = true;

            for (int i = 0; i < hit.transform.childCount; i++)
            {
                if (hit.transform.GetChild(i).tag == "MashChnge")
                {
                    Debug.Log("넣어드렸습니다.");
                    tmpRenderer = hit.transform.GetChild(i).transform.GetComponent<Renderer>();
                    tmpRenderer.material = MovePointSelectMaterial;
                    return;
                }
            }
        }
        else if (movePointChngeCheck == false)
        {
            Debug.Log("작동은 했는데요 부모 있는 무브조인트 오브젝트에");
            movePointChngeCheck = true;

            for (int i = 0; i < hit.transform.parent.childCount; i++)
            {
                if (hit.transform.parent.GetChild(i).tag == "MashChnge")
                {
                    Debug.Log("넣어드렸습니다.");
                    tmpRenderer = hit.transform.parent.GetChild(i).transform.GetComponent<Renderer>();
                    tmpRenderer.material = MovePointSelectMaterial;
                    return;
                }
            }
        }
    }

    private void SelectMaterialChengedNull()
    {
        if (tmpRenderer != null)
        {
            Debug.Log("죽어라");
            movePointChngeCheck = false;

            tmpRenderer.material = MovePointCrrentMaterial;

            tmpRenderer = null;
            return;
        }
    }
}