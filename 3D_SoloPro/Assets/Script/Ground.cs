using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour
{
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float scrollingSpeed = 2.5f;

    Vector3 startGround = new Vector3(0, 17.8f, 96);

    private void Update()
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
        if (transform.position.z < -95.999)
        {
            transform.position = startGround;
        }
    }
}
