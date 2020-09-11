﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public Character character;
    public CharacterActions actions;
    public GroundCheck groundcheck;

    void Update () {
        // Character Walk to left
        if (Input.GetKey (KeyCode.A)) {
            actions.Walk (-1);
        }
        // Character Walk to right
        if (Input.GetKey (KeyCode.D)) {
            actions.Walk (1);
        }
        // Character Jump
        if (Input.GetKeyDown (KeyCode.W)) {
            if (groundcheck.onGround == true) {
                actions.Jump ();
            }
        }
        // Character IDLE
        if (!Input.anyKey || Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.A)) {
            actions.Idle ();
        }
    }
}