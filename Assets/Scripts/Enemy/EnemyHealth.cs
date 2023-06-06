using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
   public int startingHealth = 100;
   public int currentHealth;

   public float flashSpeed = 5f;
   public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

   public float sinkSpeed = 1f;

   bool isDead;
   bool isSinking;
   bool damaged;

   private void Start() {
       //몬스터가 죽고 나서 다시 쓰일 때를 위해서 초기화는 Init() 함수에서 합니다.
       Init();
   }

   //오브젝트 풀 활용 : 몬스터가 죽고 나서 다시 쓰일 때를 위해서 초기화는 Init() 함수에서 합니다.
   public void Init() {
       currentHealth = startingHealth;

       isDead = false;
       damaged = false;
       isSinking = false;

       BoxCollider collider = transform.GetComponentInChildren<BoxCollider>();
       collider.isTrigger = false;

       GetComponent<NavMeshAgent>().enabled = true;

   }

   private void Awake() {
       currentHealth = startingHealth;
   }

   public void TakeDamage(int amount) {
       damaged = true;
       currentHealth -= amount;

       if(currentHealth <= 0 && !isDead) {
           Death();
       }
   }

   public IEnumerator StartDamage(int damage, Vector3 playerPosition, float delay, float pushBack) {
       
       yield return new WaitForSeconds(delay);

       try {
           TakeDamage(damage);
           Vector3 diff = playerPosition - transform.position;
           diff = diff/diff.sqrMagnitude;
           GetComponent<Rigidbody>().AddForce((transform.position - new Vector3(diff.x, diff.y, 0f))*50f*pushBack);
       } catch(MissingReferenceException e) {
           Debug.Log(e.ToString());
       }
   }

    private void Update() {
        if(damaged) {
            transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_OutlineColor", flashColor);
        } else {
            transform.GetChild(0).GetComponent<Renderer>().material.SetColor(
                "_OutlineColor", Color.Lerp(transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_OutlineColor"), Color.black, flashSpeed * Time.deltaTime));
        }
        damaged = false;

        if(isSinking) {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }
    private void Death()
    {
        isDead = true;

        StageController.Instance.AddPoint(10);

        transform.GetChild(0).GetComponent<BoxCollider>().isTrigger = true;
        StartSinking();
    }

    public void StartSinking() {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }
}
