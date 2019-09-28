using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 이벤트 총괄 관리 매니저
public class EventManager : MonoBehaviour
{
    public Transform player;
    public Transform moveBOx_MovePoint;
    public Transform MoveBox_Tr;

    public Camera mainCamera = null;

    public float maxStreet = 10f;

    public Material outLineMat;
    public GameObject knife;

    public bool eventing = false;
    public static bool playerStop = false;
    public FadeInOut fadeinout;
    public TextManger textManger;
    public GenerationManager generationManager;
    public HeroineEventSetting heroineEventSetting;
    public GameManager gameManager;
    public ParticleManager particleManager;
    public UIManger uIManger;

    public AnimationManager animationManager;

    public ParticleSystem itemParticle;

    public GameObject itemtext;
    public GameObject arrow;
    private Color arrowColor;
    private bool itemUiActive = false;
    public bool TestMod = false;

    public GameObject bigGate;

    public static int stack = -1;

    private void Awake()
    {
        knife = GameObject.Find("knife_unity");
        fadeinout = FindObjectOfType<FadeInOut>();
        mainCamera = FindObjectOfType<Camera>();
        gameManager = transform.GetComponent<GameManager>();
        heroineEventSetting = FindObjectOfType<HeroineEventSetting>();
        particleManager = transform.GetComponent<ParticleManager>();
        animationManager = transform.GetComponent<AnimationManager>();
        itemtext = GameObject.Find("SphereItem 1").transform.Find("ItemCanvas").transform.Find("PickUpText").gameObject;//
        arrow = GameObject.Find("SphereItem 1").transform.Find("ItemCanvas").transform.Find("Arrow").gameObject;//
        arrowColor = arrow.GetComponent<SpriteRenderer>().color;
        uIManger = FindObjectOfType<UIManger>();
    }

    private void Start()
    {
        if (TestMod == true)
        {
            StartCoroutine("GateDown", bigGate);
        }
    }

    private void FixedUpdate()
    {
        if (eventing == true)
        {
            RayCastEye.rayLength = 10f;

            JointTransfom(MoveBox_Tr);

            if (playerStop == true)
            {
                PlayerToStop(player, JointTransfom(MoveBox_Tr));
            }
        }

        if (itemUiActive == true)
        {
            ItemUiActive();
        }
    }

    private Vector3 JointTransfom(Transform moveBoxTr)
    {
        var tmp = moveBoxTr.position;
        tmp.y += 1.5f;
        moveBOx_MovePoint.position = tmp;
        return tmp;
    }

    // 플레이어 멈춤
    private void PlayerToStop(Transform player, Vector3 position)
    {
        player.transform.position = position;
    }

    // 플레이어 무브에서 쓰이며, 각각 무브포인트에 값을 전달하여 해당 무브포인트 위치에 이벤트 여부 확인
    public void EventCheck(int movePointNum)
    {
        gameManager.MoveEventCheck(movePointNum);
        textManger.TextEventCheck(movePointNum);
        heroineEventSetting.HeroinEventCheck(movePointNum);
        uIManger.ShowUIBoard(movePointNum);

        if (movePointNum == 0)
        {
            ChngeEffet();
        }
        //이벤트 판정
    }

    public void AnimationEventCheck(int movePointNum, int Textcnt)
    {
        animationManager.AnimationEventCheck(movePointNum, Textcnt);
    }

    // 이펙트 매니저 함수 사용
    private void ChngeEffet()
    {
        particleManager.RegenParticleOff();
        particleManager.NextParticleStart();
    }

    // 테스트 아웃풋 카운트 값 가져와서 판단
    public void TextToNext()
    {
        Debug.LogFormat("{0} 스택", stack);
        Color arrowColor = arrow.GetComponent<SpriteRenderer>().color;

        switch (PlayerController.MovePointNum)
        {
            case -1:

                switch (TextOutput.cnt)
                {
                    // 복)이미 검은화면인 상태
                    //case 0:

                    case 1:
                        fadeinout.BlinkEye();
                        textManger.TextNext();

                        // 복)눈 깜박깜박? 검은화면에서 다시 되돌아옴
                        return;

                    case 2:
                        textManger.TextNext();
                        StartCoroutine("RokTimeEvent");// 돌떨어지는 이벤트 시작
                        heroineEventSetting.HeroinMove(gameManager.MovePointfind(0), 3);

                        return;

                    case 5:
                        // 텍스트 매니저 퍼즈 걸어서 텍스트 멈춤.
                        // 플레이어 컨트롤러에 조건 퍼즈 상태 체크 넣음.
                        // 엠프티매니저에 초기화 코드 넣음
                        if (stack == -1)
                        {
                            //복) 기둥 UI출력
                            TextManger.Condition_Pause = true;
                            gameManager.MovePointNext(-1);
                        }
                        else
                            textManger.TextNext();

                        return;

                    case 6:
                        if (EventManager.stack == 0)
                        {
                            TextManger.Condition_Pause = false;
                            //textManger.TextNext();
                            EventManager.stack = 1;
                            itemParticle.Play();
                            itemUiActive = true;
                        }
                        else
                        {
                            textManger.TextNext();
                            TextManger.Condition_Pause = true;

                            //??) 물체 반짝반짝
                        }
                        return;

                    case 8:
                        // 그래 저거면 밧줄을 잘를수 있을거야
                        //복) 포커싱 아이템 UI
                        heroineEventSetting.HeroinMove(GameObject.FindGameObjectWithTag("Item 1").transform, 0);
                        textManger.TextNext();
                        return;

                    case 10:
                        if (EventManager.stack == 1)
                        {
                            animationManager.AnimationEventCheck(PlayerController.MovePointNum, TextOutput.cnt);
                            textManger.TextNext();
                        }
                        if (EventManager.stack == 2 && GameObject.FindGameObjectWithTag("Item 1").activeSelf == true)
                        {
                            TextManger.Condition_Pause = true;
                            return;
                        }
                        else
                        {
                            textManger.TextNext();
                        }

                        return;

                    case 13:
                        if (EventManager.stack == 3)
                        {
                            heroineEventSetting.HeroinMove(gameManager.MovePointfind(0), 0, false);
                            TextManger.Condition_Pause = true;
                        }
                        else
                        {
                            EventManager.stack = 3;
                        }

                        return;

                    case 14:
                        EventManager.stack = 4;
                        TextManger.Condition_Pause = true;
                        return;

                    default:
                        textManger.TextNext();
                        return;
                }

            case 0:
                switch (TextOutput.cnt)
                {
                    case 0:
                        //돌떨어지는것 멈춤
                        StopCoroutine("RokTimeEvent");

                        textManger.TextNext();
                        return;

                    case 3:
                        textManger.TextNext();
                        // 괴물 소리 사운드
                        heroineEventSetting.HeroinMove(gameManager.MovePointfind(2), 1);
                        return;

                    case 4:
                        TextManger.Condition_Pause = true;
                        gameManager.MovePointNext(PlayerController.MovePointNum);
                        return;

                    default:
                        textManger.TextNext();
                        break;
                }
                break;

            case 2:
                //룩앳을 하든.
                gameManager.MovePointNext(PlayerController.MovePointNum);
                heroineEventSetting.HeroinMove(gameManager.MovePointfind(4), 1);
                TextManger.Condition_Pause = true;// 대화창 끄기
                return;

            case 6:
                switch (TextOutput.cnt)
                {
                    case 6:
                        StartCoroutine("GateDown", bigGate);

                        //임시 트랜스폼 생성
                        TextManger.Condition_Pause = true;

                        //룩앳을 하든.
                        return;

                    default:
                        textManger.TextNext();
                        break;
                }
                break;

            case 11:
                switch (TextOutput.cnt)
                {
                    case 0:
                        TextManger.Condition_Pause = true;
                        gameManager.MovePointNext(PlayerController.MovePointNum);
                        //복) UI 첫번째 점프 UI 출력
                        return;

                    default:
                        break;
                }
                return;

            case 12:
                switch (TextOutput.cnt)
                {
                    case 0:
                        TextManger.Condition_Pause = true;
                        gameManager.MovePointNext(PlayerController.MovePointNum);
                        heroineEventSetting.HeroinMove(gameManager.MovePointfind(13), 3);
                        return;

                    default:
                        break;
                }
                return;

            case 13:
                switch (TextOutput.cnt)
                {
                    case 0:
                        //복)UI이벤트 이단점프 시작
                        TextManger.Condition_Pause = true;
                        gameManager.MovePointNext(PlayerController.MovePointNum);
                        heroineEventSetting.HeroinMove(gameManager.MovePointfind(15), 3);
                        return;

                    default:
                        break;
                }
                return;

            case 15:
                switch (TextOutput.cnt)
                {
                    case 0:
                        TextManger.Condition_Pause = true;
                        gameManager.MovePointNext(PlayerController.MovePointNum);
                        heroineEventSetting.HeroinMove(gameManager.MovePointfind(17), 3);
                        return;

                    default:
                        break;
                }
                return;

            case 16:
                switch (TextOutput.cnt)
                {
                    case 0:
                        //복)UI이벤트 붕대감고 올라가봐 갈고리!
                        TextManger.Condition_Pause = true;
                        gameManager.MovePointNext(PlayerController.MovePointNum);
                        return;

                    default:
                        break;
                }
                return;

            case 17:
                switch (TextOutput.cnt)
                {
                    case 1:
                        TextManger.Condition_Pause = true;
                        heroineEventSetting.HeroinMove(gameManager.MovePointfind(18), 3);
                        gameManager.MovePointNext(PlayerController.MovePointNum);
                        return;

                    default:
                        textManger.TextNext();
                        break;
                }
                return;

            case 18:
                switch (TextOutput.cnt)
                {
                    case 2:
                        TextManger.Condition_Pause = true;
                        gameManager.MovePointNext(PlayerController.MovePointNum);
                        heroineEventSetting.HeroinMove(gameManager.MovePointfind(19), 3);
                        return;

                    default:
                        textManger.TextNext();
                        break;
                }
                return;

            case 19:
                switch (TextOutput.cnt)
                {
                    case 1:
                        TextManger.Condition_Pause = true;
                        gameManager.MovePointNext(PlayerController.MovePointNum);
                        heroineEventSetting.HeroinMove(gameManager.MovePointfind(21), 3);
                        return;

                    default:
                        textManger.TextNext();
                        break;
                }
                return;

            case 21:
                switch (TextOutput.cnt)
                {
                    case 1:
                        TextManger.Condition_Pause = true;
                        gameManager.MovePointNext(PlayerController.MovePointNum);
                        heroineEventSetting.HeroinMove(gameManager.MovePointfind(24), 3);
                        return;

                    default:
                        textManger.TextNext();
                        break;
                }
                return;

            case 24:
                switch (TextOutput.cnt)
                {
                    case 6:
                        TextManger.Condition_Pause = true;

                        StartCoroutine(fadeinout.F_out(2f));

                        StartSceneReset();
                        return;

                    default:
                        textManger.TextNext();
                        break;
                }
                break;

            default:
                textManger.TextNext();
                break;
        }
    }

    // 돌 떨어지는 이벤트
    private IEnumerator RokTimeEvent()
    {
        generationManager.FalingRokEvent(10, 10, 20);
        yield return new WaitForSeconds(2f);

        StartCoroutine("RokTimeEvent");
    }

    // 커다란 문 여는 코루틴
    private IEnumerator GateDown(GameObject a)
    {
        gameManager.SoundToPlay((int)AudioClipName.doorDown, true);

        GameObject top = a.transform.Find("Top").gameObject;
        GameObject bottom = a.transform.Find("Bottom").gameObject;

        while (top.transform.position.x <= -36f)
        {
            Debug.Log("게이트 코루틴 시작2");
            yield return new WaitForSeconds(0.01f);

            top.transform.position = Vector3.Lerp(top.transform.position, new Vector3(-30f, top.transform.position.y, top.transform.position.z), 0.005f);
            bottom.transform.position = Vector3.Lerp(bottom.transform.position, new Vector3(-52f, bottom.transform.position.y, bottom.transform.position.z), 0.005f);
        }
        Debug.Log("게이트 코루틴 끝");

        // 대화창이 꺼져 있다면 // eventManger에서 관리
        heroineEventSetting.HeroinMove(gameManager.MovePointfind(7), 3);
        gameManager.MovePointNext(PlayerController.MovePointNum);

        gameManager.SoundStop();
        StopCoroutine("GateDown");
    }

    public void StartSceneReset()
    {
        SceneManager.LoadScene(0);
    }

    // 나이프에 아웃라인 생성
    public void ItemUiActive()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 500f))
        {
            var hit_tag = hit.transform.tag;

            if (hit_tag == "Item 1")
            {
                knife.GetComponent<MeshRenderer>().material = outLineMat;
            }
        }
    }
}