using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class PlayerController : MonoBehaviour
{
    public Transform currentTransform = null;
    public Transform lastTransform = null;

    public bool moving = false; // 플레이어가 움직이고 있는 상태인지 확인
    public bool lineMoving = false; // 라인렌더러 활성화 상태
    public bool isItem = false; // 칼아이템 활성화 여부 상태
    public bool isJump = false; // 점프 상태인지 판정
    //public bool isHold = true; // 플레이어 멈춰야 하는 상태인지 판정

    public Image blurImg;// 이동시 사용하는 이미지

    public static int MovePointNum = -1; // 스크립트 전반에 걸쳐서 사용하는 무브 포인트 ( -1로 시작하는 이유는 플레이어가 이벤트 판정에 수월함때문)

    public Rigidbody rc;

    // 플레이어 이동시 사용하는 함수
    private void MoveTo(Vector3 endPos, float time)
    {
        blurImg.color = new Color(blurImg.color.r, blurImg.color.g, blurImg.color.b, 0.5f); // 이동할때 블러 이미지 반투명화.LBJ

        if (isJump == true)
        {
            if (gameManager.isSound == false)
            {
                gameManager.SoundToPlay((int)AudioClipName.Jump, false);
            }

            rc.velocity = Vector3.zero;
            rc.angularVelocity = Vector3.zero;

            Vector3 center = (transform.position + endPos) * 0.5F;
            center -= new Vector3(0, 5f, 0);

            Vector3 riseRelCenter = transform.position - center;
            Vector3 setRelCenter = endPos - center;

            transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, Time.deltaTime * time);
            transform.position += center;
        }
        else
        {
            endPos.y -= adjustment;
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * time);
        }
    }

    // 플레이어가 무브포인트에 도착했을때 작동하는 함수
    private void OnTriggerStay(Collider other) // 플레이어가 "MoveEnd"테그에 부딪치면 멈춤.
    {
        if (other.transform.CompareTag("MoveEnd") && moving == true)
        {
            blurImg.color = new Color(blurImg.color.r, blurImg.color.g, blurImg.color.b, 0f); // 도착하면 블러 이미지 투명화.LBJ

            MovePointNum = other.transform.parent.GetComponent<MovePointNum>().MovePointNumber;

            if (MovePointNum != 17)
            {
                other.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                for (int i = 0; i < other.transform.parent.childCount; i++)
                {
                    if (other.transform.parent.GetChild(i).name == "Cube")
                    {
                        other.transform.parent.GetChild(i).gameObject.SetActive(false);
                    }
                    if (other.transform.parent.GetChild(i).name == "PointGuide")
                    {
                        other.transform.parent.GetChild(i).gameObject.SetActive(false);
                        other.transform.parent.GetComponent<CapsuleCollider>().enabled = false;
                        if (other.transform.parent.GetComponent<MeshRenderer>() != null)
                        {
                            other.transform.parent.GetComponent<MeshRenderer>().enabled = false;
                        }

                        JointOn(transform.gameObject);
                    }
                }
            }

            //other 의

            moving = false;
            isJump = false;

            var tmpy = other.transform.position;
            tmpy.y -= adjustment;
            transform.position = tmpy;
            PowerZero(rc);

            eventManager.EventCheck(MovePointNum);

            if (MovePointNum == 0)
            {
                GameObject.Find("Rock").transform.Find("BigRock").gameObject.SetActive(true); // 큰돌 활성화 부분 이쪽으로 이동.
            }

            if (MovePointNum == MoveBoxPoint - 1) // 움직이는 이벤트는 무브포인트 1개 전에서 움직여야댐
            {
                eventManager.eventing = true;

                // 다음 번호 무브포인트 조작
                // 그 무브 포인트를 일정 위치마다 조작
                // 이벤트 중 표시해서
            }

            // 플레이어 박스무브이벤트
            if (MovePointNum == MoveBoxPoint && eventManager.eventing == true)
            {
                PlayerStop();
            }

            if (MovePointNum == MoveBoxPoint + 1)
            {
                eventManager.eventing = false;
                RayCastEye.rayLength = 50f;
            }

            if (gameManager.isSound == true)
            {
                gameManager.isSound = false;
            }
        }
    }

    private void PlayerStop() // 트랜스폼 들어감
    {
        EventManager.playerStop = true;
    }

    // 플레이어에 물리적 혹시 있을 물리힘을 초기화함.
    public static void PowerZero(Rigidbody rc)
    {
        rc.velocity = Vector3.zero;
        rc.angularVelocity = Vector3.zero;
        rc.Sleep();
    }
}