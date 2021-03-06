﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour {
    public Character character;
    public Animator HeartAnim;
    public Text HeartText;
    // Move
    public float moveSpeed = 10;
    public float jumpForce = 10;
    // Live
    public float characterLive = 200;
    public float characterMaxLive = 200;
    public float liveRegenerateVelocity = 1f;
    public float liveRegeneratePoints = 1;
    public bool characterDie = false;
    // Mana
    public float characterMana = 100;
    public float characterMaxMana = 100;
    public bool characterMeditar = false;
    public int characterManaReload = 1;
    // Attack
    public bool characterAttack = false;
    public int characterMinForce = 2;
    public int characterMaxForce = 8;
    public int characterForce = 7;
    public float characterAttackSpeed = 0.7f;
    public bool superPowerActive = false;
    public int superPowerManaCost = 80;
    // Defense
    public float characterResistence = 5;
    // Coins
    public float coins = 0;
    public bool takeCoin = false;
    // Fire
    public bool characterOnFire = false;

    void Start () {
        // Character live function
        liveDieRegeneration ();
        // Frist random character force on start
        characterForce = Random.Range (characterMinForce, characterMaxForce);
    }

    void Update () {
        if (characterLive <= 0) {
            characterDie = true;
        }
    }

    public void liveDieRegeneration () {
        if (characterLive < characterMaxLive && characterDie == false) {
            characterLive = characterLive + liveRegeneratePoints;
            HeartAnim.Play ("Base Layer.HeartImage", 0, 0.25f);
            HeartText.text = character.stats.characterLive.ToString ();
        }
        Invoke ("liveDieRegeneration", liveRegenerateVelocity);
    }
}