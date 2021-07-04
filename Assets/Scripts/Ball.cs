using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    new Rigidbody rigidbody;
    [SerializeField] float jumpForce;
    bool isJump = false;
    WaitForSeconds waitForJumpInterval;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        waitForJumpInterval = new WaitForSeconds(0.2f);
    }
    private void Start()
    {
        rigidbody.constraints = ~RigidbodyConstraints.FreezePositionY;
    }
    private void OnEnable()
    {
        GameManager.instance.onGameOver += Die;
    }
    private void OnDisable()
    {
        GameManager.instance.onGameOver -= Die;
    }
    IEnumerator Jump()
    {
        isJump = true;
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Force);
        yield return waitForJumpInterval;
        isJump = false;
    }
    private void  OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (!isJump)
            {
                StartCoroutine(nameof(Jump));
            }
        }
        else if (other.gameObject.CompareTag("DeathHelix"))
        {
            if (!isJump)
                GameManager.instance.OnGameOver();
        }
        else if (other.gameObject.CompareTag("WinHelix"))
        {
            GameManager.instance.OnWin();
        }
    }
    private void Die()
    {
        Debug.Log("YOU DIE!!!");
    }
}
