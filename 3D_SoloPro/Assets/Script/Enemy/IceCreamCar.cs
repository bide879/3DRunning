using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamCar : MonoBehaviour
{
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float speed = 2.2f;
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float speedRL = 2f;

    public float back = 0.0f;

    private void Start()
    {
        GameManager.Instance.onSpeedUp += () =>
        {
            //if (back < 40)
            back += 4.0f;
        };
    }

        private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (transform.position.z < 7.5f - back)
        {
            speed = -speed;
        }
        else if (transform.position.z > 10.0f)
        {
            speed = -speed;
        }

        if (transform.position.x < -8.0f)
        {
            speedRL = -speedRL;
        }
        else if (transform.position.x > -0.5f)
        {
            speedRL = -speedRL;
        }
        transform.Translate(Time.fixedDeltaTime * speedRL, 0, Time.fixedDeltaTime * speed);

    }
}
