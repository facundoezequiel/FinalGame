﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBoss : MonoBehaviour {
    public Character character;
    public Animator anim;
    private Transform target;
    public GameObject FloatingTextPrefab;
    public float finalBossLive = 500;
    public float finalBossChestLive = 50;
    public float finalBossMoveSpeed = 7;
    public int finalBossMinForce = 20;
    public int finalBossMaxForce = 30;
    public int finalBossForce = 29;
    public int randomAttack;
    public bool floatingTextActive = false;
    public bool finalBossRecover = false;
    public bool finalBossAttackingZone = false;
    public bool characterAttackingZone = false;
    public bool characterEnterInRightZone = false;
    public bool characterEnterInLeftZone = false;
    public FinalBossFollowRightZone finalBossFollowRightZone;
    public FinalBossFollowLeftZone finalBossFollowLeftZone;

    void Start () {
        anim = GetComponent<Animator> ();
        target = GameObject.FindGameObjectWithTag ("Character").GetComponent<Transform> ();
        randomAttack = Random.Range (1, 4);
    }

    void Update () {
        if (finalBossLive > 0) {
            if (finalBossChestLive == 50) {
                print ("Boss final activo");
                if (characterEnterInLeftZone == true || characterEnterInRightZone == true) {
                    FinalBossRotation ();
                    if (finalBossAttackingZone == false) {
                        FinalBossWalk ();
                    } else {
                        if (Input.GetKeyDown (KeyCode.P) && character.input.pressP == true && characterAttackingZone == true) {
                            FinalBossHurt ();
                        } else if (finalBossRecover == false) {
                            FinalBossAttack ();
                        }
                    }
                } else {
                    FinalBossIdle ();
                }
            }
        } else {
            print ("Boss final muerto");
            anim.SetBool ("isWalking", false);
            anim.SetBool ("isHurt", false);
            anim.SetBool ("isAttacking1", false);
            anim.SetBool ("isAttacking2", false);
            anim.SetBool ("isAttacking3", false);
            anim.SetBool ("isBlock", false);
            anim.SetBool ("isDying", true);
            if (floatingTextActive == false) {
                ShowFloatingText ();
            }
            Invoke ("FinalBossBeforeDie", 1f);
        }
    }

    public void ShowFloatingText () {
        var FloatingText = Instantiate (FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        floatingTextActive = true;
        if (finalBossLive > 0) {
            if (character.stats.characterForce == character.stats.characterMaxForce - 1) {
                FloatingText.GetComponent<TextMesh> ().color = Color.red;
                FloatingText.GetComponent<TextMesh> ().text = "Stuned! " + character.stats.characterForce.ToString ();;
            } else {
                FloatingText.GetComponent<TextMesh> ().color = Color.blue;
                FloatingText.GetComponent<TextMesh> ().text = "-" + character.stats.characterForce.ToString ();
            }
        } else {
            FloatingText.GetComponent<TextMesh> ().color = Color.red;
            FloatingText.GetComponent<TextMesh> ().text = "Dead!";
        }
    }

    public void FinalBossIdle () {
        anim.SetBool ("isWalking", false);
        anim.SetBool ("isHurt", false);
        anim.SetBool ("isAttacking1", false);
        anim.SetBool ("isAttacking2", false);
        anim.SetBool ("isAttacking3", false);
        anim.SetBool ("isBlock", false);
        anim.SetBool ("isDying", false);
        floatingTextActive = false;
    }

    public void FinalBossWalk () {
        if (finalBossRecover == false) {
            anim.SetBool ("isWalking", true);
            anim.SetBool ("isHurt", false);
            anim.SetBool ("isAttacking1", false);
            anim.SetBool ("isAttacking2", false);
            anim.SetBool ("isAttacking3", false);
            anim.SetBool ("isBlock", false);
            anim.SetBool ("isDying", false);
            transform.position = Vector2.MoveTowards (transform.position, target.position, finalBossMoveSpeed * Time.deltaTime);
            floatingTextActive = false;
        }
    }

    public void FinalBossAttack () {
        if (character.stats.characterDie == true) {
            FinalBossIdle ();
        }
        var acurrenceAttack = Random.Range (0, 70);
        anim.SetBool ("isAttacking1", false);
        anim.SetBool ("isAttacking2", false);
        anim.SetBool ("isAttacking3", false);
        if (character.stats.characterDie == false && acurrenceAttack == 69) {
            finalBossForce = Random.Range (finalBossMinForce, finalBossMaxForce);
            character.stats.characterLive = character.stats.characterLive - finalBossForce;
            character.actions.showLiveInText = finalBossForce;
            character.actions.Hurt ();
            anim.SetBool ("isWalking", false);
            anim.SetBool ("isHurt", false);
            randomAttack = Random.Range (1, 4);
            if (randomAttack == 1) {
                anim.SetBool ("isAttacking1", true);
            } else if (randomAttack == 2) {
                anim.SetBool ("isAttacking2", true);
            } else {
                anim.SetBool ("isAttacking3", true);
            }
            anim.SetBool ("isBlock", false);
            anim.SetBool ("isDying", false);
        } else if (character.stats.characterDie == false && acurrenceAttack != 69) {
            anim.SetBool ("isWalking", false);
            anim.SetBool ("isHurt", false);
            randomAttack = Random.Range (1, 4);
            if (randomAttack == 1) {
                anim.SetBool ("isAttacking1", true);
            } else if (randomAttack == 2) {
                anim.SetBool ("isAttacking2", true);
            } else {
                anim.SetBool ("isAttacking3", true);
            }
            anim.SetBool ("isBlock", false);
            anim.SetBool ("isDying", false);
            character.actions.showLiveInText = 0;
        }
        floatingTextActive = false;
    }

    public void FinalBossHurt () {
        finalBossLive = finalBossLive - character.stats.characterForce;
        if (character.stats.characterForce < character.stats.characterMaxForce - 1) {
            anim.SetBool ("isWalking", false);
            anim.SetBool ("isHurt", true);
            anim.SetBool ("isAttacking1", false);
            anim.SetBool ("isAttacking2", false);
            anim.SetBool ("isAttacking3", false);
            anim.SetBool ("isBlock", false);
            anim.SetBool ("isDying", false);
            finalBossRecover = false;
        } else if (character.stats.characterForce == character.stats.characterMaxForce - 1) {
            anim.SetBool ("isWalking", false);
            anim.SetBool ("isHurt", true);
            anim.SetBool ("isAttacking1", false);
            anim.SetBool ("isAttacking2", false);
            anim.SetBool ("isAttacking3", false);
            anim.SetBool ("isBlock", false);
            anim.SetBool ("isDying", false);
            finalBossRecover = true;
            Invoke ("FinalBossGettingUp", 0.7f);
        }
        ShowFloatingText ();
    }

    public void FinalBossGettingUp () {
        finalBossRecover = false;
    }

    public void FinalBossRotation () {
        if (characterEnterInLeftZone == true) {
            transform.localRotation = Quaternion.Euler (0, 0, 0);
        } else if (characterEnterInRightZone == true) {
            transform.localRotation = Quaternion.Euler (0, 180, 0);
        }
    }

    public void FinalBossBeforeDie () {
        Destroy (GetComponent<BoxCollider2D> ());
        Invoke ("FinalBossDie", 1f);
    }

    public void FinalBossDie () {
        finalBossAttackingZone = false;
        characterAttackingZone = false;
        Destroy (this.gameObject);
    }
}