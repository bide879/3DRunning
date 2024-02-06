using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float interval = 0.5f;

    protected const float MinX = -4.0f;
    protected const float MaxX = 4.0f;

    private void Start()
    {
  

        StartCoroutine(SpawnCoroutine());   // SpawnCoroutine �ڷ�ƾ �����ϱ�
    }

    IEnumerator SpawnCoroutine()
    {
        while (true) // ���� �ݺ�
        {
            yield return new WaitForSeconds(interval);  // interval��ŭ ��ٸ� ��
            Spawn();                                    // Spawn ����
        }
    }

    protected virtual void Spawn()
    {
        //GameObject obj = Instantiate(emenyPrefab, GetSpawnPosition(), Quaternion.identity); // ����
        //obj.transform.SetParent(transform); // �θ� ����
        //obj.name = $"Enemy_{spawnCounter}"; // ���� ������Ʈ �̸� �ٲٱ�
        //spawnCounter++;

        ///Factory.Instance.GetBigTrash(GetSpawnPosition());
        //Wave enemy = Factory.Instance.GetEnemyWave(GetSpawnPosition());
        //enemy.transform.SetParent(transform);
    }

    /// <summary>
    /// ������ ��ġ�� �����ϴ� �Լ�
    /// </summary>
    /// <returns>������ ��ġ</returns>
    protected Vector3 GetSpawnPosition()
    {
        Vector3 pos = transform.position;
        pos.y += Random.Range(MinX, MaxX);  // ���� ��ġ���� ���̸� (-4 ~ +4) ����

        return pos;
    }

}
