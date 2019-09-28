using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public bool animationPlay = false;

    public void AnimationEventCheck(int movenum, int cnt)
    {
        switch (movenum)
        {
            case -1:
                switch (cnt)
                {
                    case 5:
                        if (animationPlay == false)
                        {
                            animationPlay = true;
                            //Debug.Log("오른손으로 왼손 잡는 애니메이션");
                            // 애니메이션 재생 끝났다고 판단 하면 끝나면 아래 실행
                            EventManager.stack = 0;
                            TextManger.Condition_Pause = false;
                            animationPlay = false;
                            Debug.Log("stak 1로 변환 완료");
                        }

                        return;

                    case 7:
                        if (animationPlay == false)
                        {
                            // 애니메이션 재생
                            animationPlay = true;
                            //Debug.Log("조각을 향해 멀리 뻗은 손 애니메이션");
                            animationPlay = false;
                        }
                        return;

                    case 10:
                        if (animationPlay == false)
                        {
                            if (EventManager.stack == 1)
                            {
                                // 애니메이션 재생
                                animationPlay = true;
                                //Debug.Log("왼쪽손은 오른손을 잡고있다.");
                                EventManager.stack = 2;
                                animationPlay = false;
                            }
                        }

                        return;

                    default:
                        break;
                }
                return;

            default:
                return;
        }
    }
}