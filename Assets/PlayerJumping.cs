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

    private void OnBeforeMove()
    {
        throw new NotImplementedException();
    }
}
