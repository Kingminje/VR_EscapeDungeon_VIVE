using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이펙트 매니저, 순서대로 껏다 켰다 해주는 역활
public class ParticleManager : MonoBehaviour
{
    public GameObject[] particles;

    public void RegenParticleOff()
    {
        particles[0].gameObject.SetActive(false);
        particles[1].gameObject.SetActive(false);
    }

    public void NextParticleStart()
    {
        particles[2].gameObject.SetActive(true);
    }
}