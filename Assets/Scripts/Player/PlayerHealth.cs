using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //주인공의 시작 체력. 기본 100으로 설정
    public int startingHealth = 100;
    //주인공의 현재 체력
    public int currentHealth;
    //체력 게이지 UI와 연결된 변수
    public Slider healthSlider;
    //주인공이 데미지를 입을 때 화면을 빨갛게 만들기 위한 투명한 이미지
    public Image damageImage;
    //주인공이 데미지를 입을 때 재생할 오디오
    public AudioClip deathClip;
    //화면이 빨갛게 변하고 나서 다시 투명한 상태로 돌아가는 속도
    public float flashSpeed = 5f;
    //주인공이 데미지를 입엇을 때 화면이 변하게 되는 색상
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    //에니메이터 컨트롤러에 매개변수를 전달하기 위해 연결한 Animator 컴포넌트
    Animator anim;
    //플레이어 게임 오브젝트에 붙어있는 오디오 소스(Audio Source) 컴포넌트
    //효과음을 재생할 때 필요
    AudioSource playerAudio;
    //플레이어의 움직임을 관리하는 PlayerMovement 스크립트 컴포넌트
    PlayerMovement playerMovement;
    //플레이어가 죽었는지 저장하는 플래그
    bool isDead;
    bool damaged;

    //오브젝트가 시작되면 호출되는 함수
    private void Awake() {
        //Player 게임 오브젝트에 붙어있는 Animator 컴포넌트를 찾아서 변수에 넣는다
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = startingHealth;
    }

    private void Update() {
        if(damaged) {
            damageImage.color = flashColor;
        } else {
            //공격 받고 난 후에는 서서히 투명한 색(Color.clear)로 변경
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        //damaged 플래그로 damaged가 true일 때 화면을 빨갛게 만드는 명령을 딱 한번만 수행
        damaged = false;
    }

    //플레이어가 공격받았을 때 호출되는 함수
    public void TakeDamage(int amount) {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        if(currentHealth <= 0 && !isDead) {
            Death();
        } else {
            anim.SetTrigger("Damage");
        }
    }

    private void Death()
    {
        StageController.Instance.FinishGame();
       isDead = true;

       anim.SetTrigger("Die");
       playerMovement.enabled = false;
    }
}
