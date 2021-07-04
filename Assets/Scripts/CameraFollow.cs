using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 originPos;
    float cameraPosY;
    float cameraDistance;
    private void Awake()
    {
        originPos = transform.position;
    }
    private void OnEnable()
    {
        GameManager.instance.onGameOver += ResetTransform;
    }
    private void OnDisable()
    {
        GameManager.instance.onGameOver -= ResetTransform;
    }
    private void Start()
    {
        cameraDistance = transform.position.y - GameManager.instance.originPos.y;
        cameraPosY = GameManager.instance.originPos.y;
        StartCoroutine(nameof(CameraFollowCoroutine));
    }

    private void ResetTransform()
    {
        transform.position = originPos;
        Start();
    }

    IEnumerator CameraFollowCoroutine()
    {
        while (true)
        {
            yield return null;
            if (cameraPosY > target.position.y )
                cameraPosY = target.position.y ;
            transform.position = new Vector3(transform.position.x, cameraPosY+ cameraDistance, transform.position.z);
        }
    }

}
