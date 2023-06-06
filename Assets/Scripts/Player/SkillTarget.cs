using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SkillTarget은 일반 공격을 할 때 공격 반경에 있는 적들의 리스트를 관리하는 클래스이다.
public class SkillTarget : MonoBehaviour
{   
    //스킬 공격 대상에 있는 적들의 리스트
    public List<Collider> targetList;

    private void Awake() {
        targetList = new List<Collider>();
    }

    //적 개체가 스킬 공격 반경 안에 들어오면, targetList에 해당 개체를 추가
    private void OnTriggerEnter(Collider other) {
        targetList.Add(other);
    }

    private void OnTriggerExit(Collider other) {
        targetList.Remove(other);
    }
}
