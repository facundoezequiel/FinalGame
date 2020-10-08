﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public Character character;
    public CharacterActions actions;
    public GroundCheck groundcheck;
    public BlackEnemy blackEnemy;

    void Update () {
        if (character.stats.characterDie == false) {
            if (character.stats.superPowerActive == false) {
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
                // Character Attack
                if (Input.GetKeyDown (KeyCode.P)) {
                    actions.Attack ();
                }
                // Character Super Power
                if (Input.GetKeyDown (KeyCode.O)) {
                    actions.SuperPower ();
                }
                // Character Meditar
                if (Input.GetKeyDown (KeyCode.M)) {
                    if (character.stats.characterMeditar == false) {
                        actions.Meditar ();
                    }
                }
                // Character IDLE
                if (!Input.anyKey || Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.A)) {
                    actions.Idle ();
                }
            }
        } else {
            // Character Die
            actions.Die ();
        }
    }
}