using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BigTrash : RecycleObject
{
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float scrollingSpeed = 8.0f;

    /// <summary>
    /// 수명
    /// </summary>
    public float lifeTime = 10.0f;


    private void Start()
    {
        GameManager.Instance.onSpeedUp += () =>
        {
            scrollingSpeed = 16.0f;
            lifeTime = 3.0f;
        };

        GameManager.Instance.onSpeedUpEnd += () =>
        {
            scrollingSpeed = 8.0f;
            lifeTime = 10.0f;
        };
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(LifeOver(lifeTime));
    }

    void Move()
    {
        transform.Translate(0, 0, Time.fixedDeltaTime * -scrollingSpeed);
    }

}
