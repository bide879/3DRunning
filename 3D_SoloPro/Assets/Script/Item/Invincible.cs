using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Invincible : MonoBehaviour
{
    public float pushPower = 15.0f;

    bool isInvncivle = false;
    private void Start()
    {
        GameManager.Instance.onSpeedUp += () =>
        {
            isInvncivle = true;
        };
        GameManager.Instance.onSpeedUpEnd += () =>
        {
            isInvncivle = false;
        };

    }

    private void OnTriggerEnter(Collider other)
    {
        if(isInvncivle)
        OnActivate(other.gameObject);
    }


    protected void OnActivate(GameObject target)
    {

        Rigidbody rigid = target.GetComponent<Rigidbody>();
        if (rigid != null)
        {
            Vector3 dir = (transform.up + -transform.forward).normalized;
            rigid.AddForce(pushPower * dir, ForceMode.Impulse);
        }
    }

}
