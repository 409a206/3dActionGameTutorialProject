using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == player) {
            playerInRange = true;
            //Debug.Log("PlayerInRange");
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject == player) {
            playerInRange = false;
        }
    }

    private void Update() {
        timer += Time.deltaTime;
        //Debug.Log("update");
        if(timer >= timeBetweenAttacks  && playerInRange && enemyHealth.currentHealth > 0) {
            Attack();
            // Debug.Log("AttackTriggered");
        }
    }

    private void Attack()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0) {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
