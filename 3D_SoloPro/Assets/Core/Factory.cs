using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 오브젝트 풀을 사용하는 오브젝트의 종류
/// </summary>
public enum PoolObjectType
{
    Bigtrash,
    LightPlane,
    IceCream

}

public class Factory : Singleton<Factory>
{
    // 오브젝트 풀들
    BigTrashPool bigtrash;
    LightPlanePool lightPlane;
    IceCreamPool iceCream;

    /// <summary>
    /// 씬이 로딩 완료될 때마다 실행되는 초기화 함수
    /// </summary>
    protected override void OnInitialize()
    {
        base.OnInitialize();

        // GetComponentInChildren : 나와 내 자식 오브젝트에서 컴포넌트 찾음

        // 풀 컴포넌트 찾고, 찾으면 초기화하기

        bigtrash = GetComponentInChildren<BigTrashPool>();
        if(bigtrash != null)
            bigtrash.Initialize();

        lightPlane = GetComponentInChildren<LightPlanePool>();
        if (lightPlane != null)
            lightPlane.Initialize();

        iceCream = GetComponentInChildren<IceCreamPool>();
        if (iceCream != null)
            iceCream.Initialize();


    }

    /// <summary>
    /// 풀에 있는 게임 오브젝트 하나 가져오기
    /// </summary>
    /// <param name="type">가져올 오브젝트의 종류</param>
    /// <param name="position">오브젝트가 배치될 위치</param>
    /// <param name="angle">오브젝트의 초기 각도</param>
    /// <returns>활성화된 오브젝트</returns>
    public GameObject GetObject(PoolObjectType type, Vector3? position = null, Vector3? euler = null)
    {
        GameObject result = null;
        switch (type)
        {
            case PoolObjectType.Bigtrash:
                result = bigtrash.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.LightPlane:
                result = lightPlane.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.IceCream:
                result = iceCream.GetObject(position, euler).gameObject;
                break;
        }

        return result;
    }


    public BigTrash GetBigTrash()
    {
        return bigtrash.GetObject();
    }
    public BigTrash GetBigTrash(Vector3 position, float angle = 0.0f)
    {
        return bigtrash.GetObject(position, angle * Vector3.forward);
    }

    public LightPlane GetLightPlane()
    {
        return lightPlane.GetObject();
    }
    public LightPlane GetLightPlane(Vector3 position, float angle = 0.0f)
    {
        return lightPlane.GetObject(position, angle * Vector3.forward);
    }

    public IceCream GetIceCream()
    {
        return iceCream.GetObject();
    }
    public IceCream GetIceCream(Vector3 position, float angle = 0.0f)
    {
        return iceCream.GetObject(position, angle * Vector3.forward);
    }

}
