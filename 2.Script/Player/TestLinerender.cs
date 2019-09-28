using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한손이나 두손 라인렌더러 실행
public class TestLinerender : MonoBehaviour
{
    public Transform[] hands = new Transform[2];
    public LineRenderer[] lr;
    public PlayerController playerController;
    public RayCastEye rayCastEye;
    public GameManager gm;
    //public Transform t;

    public float value;
    public float startTime;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void OneHandLinePosSet(Transform taget)
    {
        OneHandShot(hands[0], taget.transform.position);
    }

    // 한손으로만 목표 라인렌더러 위치로 날아감
    private void OneHandShot(Transform pos, Vector3 taget)
    {
        lr[0].SetPosition(0, pos.position);

        if (value <= 1)
        {
            value += 0.1f;
        }
        else if (playerController.isItem == false)
        {
            playerController.moving = true;
            playerController.lineMoving = false;

            lr[0].SetPosition(1, pos.position);

            value = 0;
        }
        else if (playerController.isItem == true)
        {
            playerController.moving = false;
            playerController.lineMoving = false;

            lr[0].SetPosition(1, pos.position);
            value = 0;

            rayCastEye.hitObj.SetActive(false);

            gm.ActiveItem();
        }

        lr[0].SetPosition(1, Vector3.Lerp(pos.position, taget, value));
    }

    // 양손일 시 아래 주석처리 사용

    //public void A(Transform taget)
    //{
    //    GameObject obj = taget.GetComponent<GameObject>();
    //    B(hands, taget.position);
    //}

    //private void B(Transform[] hand, Vector3 taget)
    //{
    //    for (int i = 0; i < hand.Length; i++)
    //    {
    //        lr[i].SetPosition(0, hand[i].position);
    //    }

    //    if (value <= 1)
    //    {
    //        value += 0.1f;
    //    }
    //    else if (playerController.isItem == false)
    //    {
    //        playerController.moving = true;
    //        playerController.lineMoving = false;
    //        for (int i = 0; i < lr.Length; i++)
    //        {
    //            lr[i].SetPosition(1, hand[i].position);
    //        }
    //        value = 0;

    //        Debug.Log("기둥");
    //    }
    //    else if (playerController.isItem == true)
    //    {
    //        playerController.moving = false;
    //        playerController.lineMoving = false;
    //        for (int i = 0; i < lr.Length; i++)
    //        {
    //            lr[i].SetPosition(1, hand[i].position);
    //        }
    //        value = 0;

    //        //playerController.isItem = false;

    //        rayCastEye.hitObj.SetActive(false);

    //        gm.ActiveItem();
    //    }

    //    for (int i = 0; i < hand.Length; i++)
    //    {
    //        lr[i].SetPosition(1, Vector3.Lerp(hand[i].position, taget, value));
    //    }
    //}
}