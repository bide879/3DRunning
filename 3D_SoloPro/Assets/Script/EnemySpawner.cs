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
  

        StartCoroutine(SpawnCoroutine());   // SpawnCoroutine 코루틴 실행하기
    }

    IEnumerator SpawnCoroutine()
    {
        while (true) // 무한 반복
        {
            yield return new WaitForSeconds(interval);  // interval만큼 기다린 후
            Spawn();                                    // Spawn 실행
        }
    }

    protected virtual void Spawn()
    {
        //GameObject obj = Instantiate(emenyPrefab, GetSpawnPosition(), Quaternion.identity); // 생성
        //obj.transform.SetParent(transform); // 부모 설정
        //obj.name = $"Enemy_{spawnCounter}"; // 게임 오브젝트 이름 바꾸기
        //spawnCounter++;

        ///Factory.Instance.GetBigTrash(GetSpawnPosition());
        //Wave enemy = Factory.Instance.GetEnemyWave(GetSpawnPosition());
        //enemy.transform.SetParent(transform);
    }

    /// <summary>
    /// 스폰할 위치를 리턴하는 함수
    /// </summary>
    /// <returns>스폰할 위치</returns>
    protected Vector3 GetSpawnPosition()
    {
        Vector3 pos = transform.position;
        pos.y += Random.Range(MinX, MaxX);  // 현재 위치에서 높이만 (-4 ~ +4) 변경

        return pos;
    }

}
