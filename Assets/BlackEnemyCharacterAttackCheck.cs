﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackEnemyCharacterAttackCheck : MonoBehaviour {
    public BlackEnemy blackEnemy;

    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "Character" || collider.tag == "SuperPower") {
            blackEnemy.characterAttackingZone = true;
            if (collider.tag == "SuperPower") {
                blackEnemy.blackEnemyLive = 0;
            }
        }
    }

    private void OnTriggerExit2D (Collider2D collider) {
        if (collider.tag == "Character" || collider.tag == "SuperPower") {
            blackEnemy.characterAttackingZone = false;
        }
    }
}