using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 히로인 이벤트 세팅해주는 매니저
public class HeroineEventSetting : MonoBehaviour
{
    // 참조 변수
    public GameObject heroine; // 히로인 오브젝트

    //public Image TokImage; // 지금 사용안함
    //public GameObject NextButton;
    //public GameObject textBord;

    public Transform player; // 플레이어 위치 참조

    private Transform crrentTransfom = null;

    public GameManager gameManager;

    // 변경 변수
    private Vector3 crrentMovePoint;

    public int[] eventNums;

    private bool toking = false;
    private bool playerMove = false;

    private int dupliccteCheck = -1;

    public int lookSpeed = 0;

    public enum Move_Vector
    {
        //왼쪽 = 0, 오른쪽 = 1, 뒤쪽 =
    }

    private void Awake()
    {
        if (heroine == null)
        {
            heroine = transform.gameObject;
            gameManager = FindObjectOfType<GameManager>();
        }
    }

    private void Start()
    {
        player = gameManager.playerController.mainCamera.transform;
    }

    private void FixedUpdate()
    {
        PlayerSee();
    }

    // player Move에서 호춣 =>
    public void HeroinEventCheck(int movePointNum)
    {
        switch (movePointNum)
        {
            case 4:
                HeroinMove(crrentTransfom = gameManager.MovePointfind(6), 2);
                return;

            case 7:
                HeroinMove(crrentTransfom = gameManager.MovePointfind(12), 3);
                return;

            default:
                break;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="taget"></param>
    /// <param name="num">(왼쪽)1, X -= 2f  (오른쪽)2, z Z -= 4f 3. X += 2f 그외에는 숫자(정면)Z += -2f</param>
    public void HeroinMove(Transform taget, int num, bool late = true)
    {
        if (late == true)
        {
            StartCoroutine(HeroinLateMove(taget, num));
        }
        else
        {
            HeroinMove(taget, num);
        }
    }

    // 히로인 일정 시간 지난 후 이동하도록 하는 코루틴
    private IEnumerator HeroinLateMove(Transform taget, int num)
    {
        yield return new WaitForSeconds(0.5f);
        crrentMovePoint = MovePointAdjustment(taget.position, num);
        heroine.transform.position = crrentMovePoint;
    }

    // 플레이어 위치를 참조한 플레이어 게임오브젝트 트랜스폼 위치 참조하여 그 위치를 바라봄
    public void PlayerSee()
    {
        var lookPos = player.transform.position - transform.position;

        lookPos.y = 0;

        var rotation = Quaternion.LookRotation(lookPos);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * lookSpeed);
    }

    private Vector3 MovePointAdjustment(Vector3 position, int num)
    {
        var tmpVector3 = position;

        if (num == 1)
        {
            tmpVector3.x -= 2f;
        }
        else if (num == 2)
        {
            tmpVector3.z -= 4f;
        }
        else if (num == 3)
        {
            tmpVector3.x += 2f;
        }
        else
        {
            tmpVector3.z += -2f;
        }

        //tmpVector3.z += 1f;

        return tmpVector3;
    }
}

//private void CanvasShow()
//{
//    TokImage.enabled = true;
//    NextButton.SetActive(true);
//}

//private void CanvasOff()
//{
//    TokImage.enabled = false;
//    NextButton.SetActive(false);
//}

//public void TextNext()
//{
//    //textOutput.End_Typing();
//    //if (TextCount <= 2 && playerPointNum == -1)
//    //{
//    //    textOutput.End_Typing();
//    //}

//    //if (TextCount == 4 && playerPointNum == -1)
//    //{
//    //}
//    //var tmpNum = PlayerController.MovePointNum
//    //if (TextOutput.cnt == 5 && gameManager.hold_out && tmpNum == -1)
//    //{
//    //    HeroinMove(crrentTransfom = gameManager.MovePointNext(tmpNum), tmpNum);
//    //    heroine.transform.position = MovePointAdjustment(gameManager.startPoint.transform.position, tmpNum);
//    //    //CanvasOff();
//    //}
//}

//private void MoveStartPointOn()
//{
//    gameManager.startPoint.SetActive(true);
//}

//public void OverTextNext(int TextOutPut_cnt, int Next_cnt)
//{
//    if (TextOutPut_cnt != Next_cnt)
//    {
//        textOutput.End_Typing();
//        textOutput.cnt = (Next_cnt - 1);
//        textOutput.End_Typing();
//    }
//}