using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//예/아니오를 선택할 수 있는 분기형 팝업창을 관리하는 컨트롤러
public class DialogControllerConfirm : DialogController
{
    //제목(Title)을 변경할 수 있도록 연결된 Text 게임 오브젝트 변수
    public Text LabelTitle;
    //내용(Message)을 변경할 수 있도록 연결된 Text 게임 오브젝트 변수
    public Text LabelMessage;

    //제목과 내용, 그리고 콜백 함수를 가지고 있는 데이터
    DialogDataConfirm Data {
        get;
        set;
    }

    public override void Awake() {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        
        //DialogManager에 현재 이 다이얼로그 컨트롤러 클래스가 확인창을 다룬다는 사실을 등록
        DialogManager.Instance.Regist(DialogType.Confirm, this);
    }

    //확인 팝업창이 생성될 때 호출되는 함수
    public override void Build(DialogData data) {
        base.Build(data);
        //데이터가 없는데 Build를 하면 로그를 남기고 예외 처리
        if(!(data is DialogDataConfirm)) {
            Debug.LogError("Invalid dialog data!");
            return;
        }

        //DialogDataConfirm로 데이터를 받고 화면의 제목과 메시지의 내용을 입력
        Data = data as DialogDataConfirm;
        LabelTitle.text = Data.Title;
        LabelMessage.text = Data.Message;
    }

    public void OnClickOK() {
        //예(OK) 버튼을 누르면 콜백함수를 호출
        if(Data.Callback != null) {
            Data.Callback(true);

            Debug.Log("OnClickOK");

            //모든 과정이 끝났으므로, 현재 팝업을 DialogManager에서 제거
            DialogManager.Instance.Pop();
        }
    }

    public void OnClickCancel() {
        //아니오(Cancel) 버튼을 누르면, 콜백 함수를 호출
        if(Data.Callback != null) {
            Data.Callback(false);
        }

        //모든 과정이 끝났으므로, 현재 팝업을 DialogManager에서 제거
        DialogManager.Instance.Pop();
    }

}
