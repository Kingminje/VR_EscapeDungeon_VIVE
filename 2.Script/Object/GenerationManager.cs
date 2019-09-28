using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 생성 매니저, 돌 프리팹을 참조하여 처음 생성 위치 에서 돌을 랜덤한 위치로 뿌림
public class GenerationManager : MonoBehaviour
{
    public GameObject rokPrefab;

    private float width, height;

    public void RangeGenerationToObject(float x, float z, float y, GameObject Prefab, Transform pos)
    {
        var randomPositonX = Random.Range(-x, +x);
        var randomPositonZ = Random.Range(-z, +z);

        var tmpPos = pos.position;

        tmpPos.x = randomPositonX;
        tmpPos.y = y;
        tmpPos.z = randomPositonZ;

        Prefab.transform.position = tmpPos;

        Instantiate(Prefab, pos);
    }

    public void FalingRokEvent(float x, float z, float y, GameObject Prefab = null, Transform pos = null)
    {
        RangeGenerationToObject(x, z, y, rokPrefab, GameObject.Find("FallingStonePos").transform);
    }
}