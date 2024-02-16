using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamCar : MonoBehaviour
{
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float speed = -0.5f;

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (transform.position.z < 7.5)
        {
            speed = -speed;
        }
        else if (transform.position.z > 10.0f)
        {
            speed = -speed;
        }
        transform.Translate(0, 0, Time.fixedDeltaTime * speed);
    }
}
