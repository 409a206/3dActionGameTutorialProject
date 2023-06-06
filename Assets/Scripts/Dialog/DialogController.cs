using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//일반 팝업창과 확인 팝업창을 관리하는 DialogController***(DialogControllerAlert, DialogControllerConfirm)의 부모 클래스
public class DialogController : MonoBehaviour
{

    public Transform window;

    //팝업창이 보이는지 조회하거나, 보이지 않게 설정하는 변수
    public bool Visible {
        get {
            return window.gameObject.activeSelf;
        }
        private set {
            window.gameObject.SetActive(value);
        }
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    public virtual void Awake() {
        
    }

    //팝업이 화면에 나타날 때 OnEnter() 열거형(IEnumerator) 함수로 애니메이션을 구현할 수 있습니다.
    IEnumerator OnEnter(Action callback) {
        Visible = true;

        if(callback != null) {
            callback();
        }
        yield break;
    }

    //팝업이 화면에 사라질 때 OnEnter() 열거형(IEnumerator) 함수로 애니메이션을 구현할 수 있습니다.
    IEnumerator OnExit(Action callback) {
        Visible = false;

        if(callback != null) {
            callback();
        }
        yield break;
    }

    public virtual void Build(DialogData data) {

    }

    //팝업이 화면에 나타날 때 OnEnter() 열거형(IEnumerator) 함수로 애니메이션을 구현할 수 있습니다.
    public void Show(Action callback) {
        StartCoroutine(OnEnter(callback));
    }

    //팝업이 화면에 사라질 때 OnEnter() 열거형(IEnumerator) 함수로 애니메이션을 구현할 수 있습니다.
    public void Close(Action callback) {
        StartCoroutine(OnExit(callback));
    }
}
