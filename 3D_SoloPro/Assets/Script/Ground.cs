using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour
{
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float scrollingSpeed = 10.0f;

    Vector3 startGround = new Vector3(0, 17.8f, 192.0f);

    private void Start()
    {
        GameManager.Instance.onSpeedUp += () =>
        {
            scrollingSpeed = 20.0f;
        };

        GameManager.Instance.onSpeedUpEnd += () =>
        {
            scrollingSpeed = 10.0f;
        };
    }

    private void FixedUpdate()
    {
        Move();
        positionReset();
 
    }

    void Move()
    {
        transform.Translate(0, 0, Time.fixedDeltaTime * -scrollingSpeed);
    }

    void positionReset()
    {
        if (transform.position.z < -95.999f)
        {
            transform.position = startGround;
        }
        if (transform.position.z > 192.0f)
        {
            transform.position = startGround;
            scrollingSpeed = 10.0f;
        }
    }
}
