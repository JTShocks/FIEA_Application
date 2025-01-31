using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float jumpPressBufferTime = .1f;
    PlayerMovement player;
    bool isTryingToJump;
    float lastJumpPressTime;

    void Awake()
    {
        player = GetComponent<PlayerMovement>();
    }
    void OnEnable(){ player.OnBeforeMove += OnBeforeMove;}

    void OnDisable(){player.OnBeforeMove -= OnBeforeMove;}

        void OnJump()
    {
        isTryingToJump = true;
        lastJumpPressTime = Time.time;
    }

    private void OnBeforeMove()
    {
        bool wasTryingToJump = Time.time - lastJumpPressTime < jumpPressBufferTime;

        bool isOrWasTryingToJump = isTryingToJump || wasTryingToJump;
        if(isOrWasTryingToJump && player.isGrounded)
        {
            player.velocity.y += jumpSpeed;
        }
        isTryingToJump = false;
    }
}
