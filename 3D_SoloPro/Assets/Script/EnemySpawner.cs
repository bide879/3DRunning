using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float bigTrashInterval = 0.5f;
    public float lightPlaneInterval = 1.2f;

    protected const float MinX = -9.0f;
    protected const float MaxX = 2.0f;

    private void Start()
    {
        StartCoroutine(SpawnBigTrashCoroutine());
        //StartCoroutine(LightPlaneCoroutine());
    }

    IEnumerator SpawnBigTrashCoroutine()
    {
        int bigTrashCount = 0;
        while (true) // ���� �ݺ�
        {
            yield return new WaitForSeconds(bigTrashInterval);  // interval��ŭ ��ٸ� ��
            SpawnBigTrash();
            bigTrashCount++;
            if (bigTrashCount > 2) 
            {
                yield return new WaitForSeconds(lightPlaneInterval);
                SpawnLightPlane();
                bigTrashCount = 0;
            }
                 
        }
    }


    protected virtual void SpawnBigTrash()
    {
        Factory.Instance.GetBigTrash(GetSpawnPosition());
    }

    protected virtual void SpawnLightPlane()
    {
        Factory.Instance.GetLightPlane(GetSpawnPosition());
    }

    /// <summary>
    /// ������ ��ġ�� �����ϴ� �Լ�
    /// </summary>
    /// <returns>������ ��ġ</returns>
    protected Vector3 GetSpawnPosition()
    {
        Vector3 pos = transform.position;
        pos.x += Random.Range(MinX, MaxX);  // ���� ��ġ���� �翷 �� (-4 ~ +4) ����

        return pos;
    }

}
