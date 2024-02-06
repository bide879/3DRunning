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

    private void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(0, 0, Time.fixedDeltaTime * -scrollingSpeed);
    }
}
