using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BigTrash : RecycleObject
{
    /// <summary>
    /// �̵� �ӵ�
    /// </summary>
    public float scrollingSpeed = 2.5f;

    /// <summary>
    /// ����
    /// </summary>
    public float lifeTime = 10.0f;


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
