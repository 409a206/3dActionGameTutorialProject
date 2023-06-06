using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //플레이어가 몬스터에게 주는 데미지 수치입니다.
    //레벨/경험치 파츠 업그레이드 챕터에서 캐릭터 성장 시스템을 도입하면 변경될 예정입니다.
    public int NormalDamage = 10;
    public int SkillDamage = 30;
    public int DashDamage = 30;

    //캐릭터의 공격 반경
    //타겟의 Trigger로 어떤 몬스터가 공격 반경 안에 들어왔는지 판정합니다.
    public NormalTarget normalTarget;
    public SkillTarget skillTarget;

    public void NormalAttack() {
        //normalTarget에 붙어있는 Trigger Collider에 들어있는 몬스터의 리스트를 조회
        List<Collider> targetList
            = new List<Collider>(normalTarget.targetList);

        //타겟 리스트 안에 있는 몬스터들을 foreach문으로 하나하나 다 조회합니다.
        foreach(Collider one in targetList) {
            //타겟의 게임 오브젝트에 EnemyHealth라는 스크립트를 가져옵니다.
            EnemyHealth enemy = one.GetComponent<EnemyHealth>();
            //만약 EnemyHealth스크립트가 있다면 모스터이므로, 몬스터에게 데미지를 줍니다.
            if(enemy != null) {
                //몬스터에게 데미지를 얼마 줄지, 얼마나 뒤로 밀려나게 할지(pushBack),
                StartCoroutine(enemy.StartDamage(NormalDamage, transform.position, 0.5f, 0.5f));
            }
        }

        
    }
    public void DashAttack() {
        List<Collider> targetList
            = new List<Collider>(skillTarget.targetList);

            foreach(Collider one in targetList) {
                EnemyHealth enemy = one.GetComponent<EnemyHealth>();

                if(enemy != null) {
                    StartCoroutine(enemy.StartDamage(DashDamage, transform.position, 1f, 2f));
                }
            }
    }

    public void SkillAttack() {
        List<Collider> targetList
            = new List<Collider>(skillTarget.targetList);

            foreach (Collider one in targetList)
            {
                EnemyHealth enemy = one.GetComponent<EnemyHealth>();

                if(enemy != null) {
                    StartCoroutine(enemy.StartDamage(SkillDamage, transform.position, 1f, 2f));
                }
            }
    }
}
