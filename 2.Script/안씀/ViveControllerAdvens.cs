using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HTC.UnityPlugin.Vive;

/// <summary>
/// 0이면 왼쪽 1이면 오른쪽 2면 양쪽
/// </summary>
public class ViveControllerAdvens : MonoBehaviour
{
    public HandRole leftHand;
    public HandRole rigthHand;

    /// <summary>
    /// 입력받은 숫자로 어느 컨트롤러를 사용할지 알수 있다.
    /// </summary>
    /// <param name="num">0이면 양쪽, 1이면 왼손 2면 오른손이다.</param>
    /// <param name="time">마이크로초 단위 실행</param>
    public void ControllerToVibration(int num, int time)
    {
        switch (num)
        {
            case 0:
                ViveInput.TriggerHapticPulse(leftHand, (ushort)time);
                ViveInput.TriggerHapticPulse(rigthHand, (ushort)time);
                break;

            case 1:
                ViveInput.TriggerHapticPulse(leftHand, (ushort)time);
                break;

            case 2:
                ViveInput.TriggerHapticPulse(rigthHand, (ushort)time);
                break;
        }
    }
}

//public ViveInput rigthHandController
//{
//    get { return ViveInput.GetPressDown(rigthHand, ControllerButton.Trigger); }
//}

///// <summary>
///// 오른손이면 true, 왼손이면 fales
///// </summary>
///// <param name="handCheck"></param>
//public void ViveControllerTrigger(bool handCheck)
//{
//    if (handCheck)
//    {
//        ViveInput.GetPressDownEx(HandRole.RightHand, ControllerButton.Trigger);
//    }
//}