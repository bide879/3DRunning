using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpImg : MonoBehaviour
{
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        GameManager.Instance.onSpeedUp += () =>
        {
            canvasGroup.alpha = 0.5f;
            canvasGroup.blocksRaycasts = true;
            Debug.Log("이미지 onSpeedUp");
        };


        GameManager.Instance.onSpeedUpEnd += () =>
        {
            canvasGroup.alpha = 0.0f;
            canvasGroup.blocksRaycasts = false;
            Debug.Log("이미지 onSpeedUpEnd");
        };

    }

}
