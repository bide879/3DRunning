using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCream : RecycleObject
{
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float scrollingSpeed = 20.0f;

    /// <summary>
    /// 수명
    /// </summary>
    public float lifeTime = 8.0f;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }


}
