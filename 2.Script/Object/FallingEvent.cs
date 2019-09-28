using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 프리팹에 붙어서 돌이 떨어지는 순간 사운드 재생
public class FallingEvent : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody rc;

    private bool eventing = true;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        rc = transform.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (eventing == true)
        {
            if (rc.transform.localPosition.y < 0)
            {
                rc.useGravity = false;
                PlayerController.PowerZero(rc);

                var tmp = transform.GetComponent<FallingEvent>();
                gameManager.SoundToPlay((int)AudioClipName.rokCrash, false);

                eventing = false;

                Destroy(rc);
                Destroy(tmp, 1f);
            }
        }
    }
}