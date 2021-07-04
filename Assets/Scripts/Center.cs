using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    [SerializeField] float controlSpeed;
    float mouseInputX;

    private void OnEnable()
    {
        StartCoroutine(nameof(CenterRotateCoroutine));
    }
    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetMouseButton(0))
        {
            mouseInputX = Input.GetAxis("Mouse X");
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseInputX = 0f;
        }
    }
    IEnumerator CenterRotateCoroutine()
    {
        while (true)
        {
            transform.Rotate(Vector3.up, -mouseInputX * controlSpeed);
            yield return null;
        }
    }
}
