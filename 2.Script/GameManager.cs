using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 시선에 따라서 생성되는 도트 함수,  무브 포인트 끄고 켜는 함수, 컨트롤러 돌하고 라인렌더러 연결
// 게임 이용에 필요한 전반적인것을 사용
public class GameManager : MonoBehaviour
{
    //참조변수
    public Canvas UICanvas;

    public Image dot;

    public Transform itemhand;
    public GameObject itemprefab;
    public CapsuleCollider lineColl;

    public GameObject[] movePoint_Obejcts;
    public int MmovePointNum = 1;

    public LineRenderer holdLineRenderer;

    public GameObject holdObj;
    public GameObject handObj;

    public GameObject startPoint;
    public Transform nextMovePoint = null;

    public PlayerController playerController;
    public SoundManager soundManager;

    public bool hold_out = false;
    public bool isSound = false;

    private void Start()
    {
        MoveSet(movePoint_Obejcts);
    }

    private void Update()
    {
        RockHoldLine();
    }

    // 사운드 플레이
    public void SoundToPlay(int num, bool Loop)
    {
        if (Loop == false)
        {
            soundManager.SoundPlay(num);
        }
        else
        {
            soundManager.SoundPlayLoop(num);
        }
        isSound = true;
    }

    // 플레이 중인 사운드 종료
    public void SoundStop()
    {
        soundManager.StopSoundPlay();
        isSound = false;
    }

    // 도트 킴
    public void DotShow(RaycastHit hit)
    {
        var playerTr = PlayerController.playerCmera.transform.position;
        var tmpImageSizeRe = playerTr - UICanvas.transform.position;
        tmpImageSizeRe.y *= 10;
        var rectTr = dot.gameObject.GetComponent<RectTransform>();

        UICanvas.transform.LookAt(playerTr);
        UICanvas.transform.position = hit.point;
        dot.enabled = true;
    }

    // 도트 끔
    public void DotDisabled()
    {
        dot.enabled = false;
    }

    // 아이템 생성
    public void ActiveItem()
    {
        Instantiate(itemprefab, itemhand.position, itemhand.rotation).transform.parent = itemhand.transform;
    }

    // 무브포인트에 무브넘버 넣어줌
    public void MoveSet(GameObject[] Points)
    {
        MovePointNumSet(Points);
    }

    // 무브포인트 변수에 따른 넘버 찾아서 트랜스폼 정보 리턴해줌
    public Transform MovePointfind(int num)
    {
        return MovePointFindStart(num);
    }

    // 히로인에 사용하는 무브에 무브포인트 트랜스폼 리턴
    private Transform MovePointFindStart(int num)
    {
        int tmp = 0;
        foreach (var item in movePoint_Obejcts)
        {
            tmp = item.GetComponent<MovePointNum>().MovePointNumber;

            if (tmp == num)
            {
                return item.gameObject.transform;
            }
        }
        return null;
    }

    //이벤트매니저에서 사용하는 함수
    public void MoveEventCheck(int num)
    {
        switch (num)
        {
            case 0:
                return;

            case 2:
                return;

            case 6:
                return;

            case 11:
                return;

            case 12:
                return;

            case 13:
                return;

            case 15:
                return;

            case 16:
                return;

            case 17:
                return;

            case 18:
                return;

            case 19:
                return;

            case 21:
                return;

            case 24:
                return;

            default:
                MovePointNext(num);
                break;
        }
    }

    // 플레이어 무브에서 사용 다음 무브포인트 정보와 오브젝트 활성화
    public Transform MovePointNext(int num)
    {
        return MovePointProcessor(num);
    }

    private Transform MovePointProcessor(int num)
    {
        foreach (var item in movePoint_Obejcts)
        {
            var tmp = item.GetComponent<MovePointNum>().MovePointNumber;

            if (tmp == num + 1)
            {
                item.SetActive(true);
                nextMovePoint = item.transform;
                break;
            }
        }
        return nextMovePoint;
    }

    private void MovePointNumSet(GameObject[] gameObjects)
    {
        foreach (var item in gameObjects)
        {
            item.AddComponent<MovePointNum>().MovePointNumber = MmovePointNum;

            if (MmovePointNum != 0)
            {
                item.SetActive(false);
            }

            MmovePointNum += 1;
        }
    }

    // 돌 하고 플레이어 컨트롤러 연결함
    private void RockHoldLine()
    {
        Transform posRock = holdObj.GetComponent<Transform>();
        Transform posHand = handObj.GetComponent<Transform>();

        holdLineRenderer.SetPosition(0, posRock.position);

        holdLineRenderer.SetPosition(1, posHand.position);

        Vector3 center = (posRock.position + posHand.position) * 0.5F;

        lineColl.transform.position = center;
        lineColl.transform.LookAt(posHand.position);
    }
}