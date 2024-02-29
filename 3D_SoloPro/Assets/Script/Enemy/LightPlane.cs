using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlane : RecycleObject
{
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float scrollingSpeed = 20.0f;

    /// <summary>
    /// 수명
    /// </summary>
    public float lifeTime = 6.0f;

    private void Start()
    {
        GameManager.Instance.onSpeedUp += () =>
        {
            scrollingSpeed = 40.0f;
        };

        GameManager.Instance.onSpeedUpEnd += () =>
        {
            scrollingSpeed = 20.0f;
        };
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        transform.Rotate(0, -90, 0);
        StartCoroutine(LifeOver(lifeTime));
    }

    void Move()
    {
        transform.Translate(Time.fixedDeltaTime * -scrollingSpeed, 0, 0);
    }
}
