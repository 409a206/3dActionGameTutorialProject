using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//다이얼로그의 종류를 구분하는 enum변수
//DialogType.Alert , DialogType.Confirm 이런식으로 다이얼로그의 유형을 지어할 수 있습니다.
public enum DialogType {
    Alert,
    Confirm,
    Ranking
}

//DialogManager는 다이얼로그들을 관리하는 관리 클래스입니다.
public sealed class DialogManager : MonoBehaviour
{
    //유저에게 보여줄 팝업창들을 저장해 놓는 리스트. 리스트에 들어온 순서대로 꺼내서 하나씩 유저에게 보여줌
    List<DialogData> _dialogQueue;
    //다이얼로그 타입에 따른 컨트롤러를 매핑한 Dictionary변수
    //DialogType.Alert유형은 DialogControllerAlert
    Dictionary<DialogType, DialogController> _dialogMap;
    //현재 화면에 떠있는 다이얼로그를 지정
    DialogController _currentDialog;
    //싱글톤 패턴으로 하나의 인스턴스를 전역적으로 공유하기 위해 instance를 여기에 생성
    private static DialogManager instance = new DialogManager();

    public static DialogManager Instance {
        get {
            return instance;
        }
    }

    //생성자입니다. 클래스의 인스턴스가 생성될 때 인스턴스 변수들을 초기화 해줍니다.
    private DialogManager() {
        _dialogQueue = new List<DialogData>();
        _dialogMap = new Dictionary<DialogType, DialogController> ();
    }

    //Regist 함수로 특정 DialogType에 매칭되는 DialogController를 지정합니다.
    public void Regist (DialogType type, DialogController controller) {
        _dialogMap [type] = controller;
    }

    //Push함수로 DialogData를 추가
    public void Push(DialogData data) {
        //다이얼로그 리스트를 저장하는 변수에 새로운 다이얼로그 데이터를 추가
        _dialogQueue.Add(data);

        if(_currentDialog == null) {
            //다음으로 보여줄
            ShowNext();
        }
    }

    //Pop함수로 리스트에서 마지막으로 열린 다이얼로그를 닫습니다.
    public void Pop() {
        if(_currentDialog != null) {
            _currentDialog.Close(
                delegate {
                    _currentDialog = null;

                    if(_dialogQueue.Count > 0) {
                        ShowNext();
                    }
                }
            );
        }
    }

    private void ShowNext() {
        //다이얼로그 리스트에서 첫 번째 멤버를 가져옵니다.
        DialogData next = _dialogQueue[0];
        //가져온 멤버의 다이얼로그의 유형을 확인
        //그래서 그 다이얼로그 유형에 맞는 다이얼로그 컨트롤러(DialogController)를 조회
        DialogController controller = _dialogMap[next.Type].GetComponent<DialogController>();
        //조회한 다이얼로그 컨트롤러를 현재 열린 팝업의 다이얼로그 컨트롤러로 지정
        _currentDialog = controller;
        //현재 열린 다이얼로그 데이터를 화면에 표시
        _currentDialog.Build(next);
        //다이얼로그를 화면에 보여주는 애니메이션을 시작
        _currentDialog.Show(delegate{});
        //다이얼로그 리스트에서 꺼내온 데이터를 제거
        _dialogQueue.RemoveAt(0);
    }

    //현재 팝업 윈도우가 표시되어 있는지 확인하는 함수
    //_currentDialog가 비어있으면, 현재 화면에 팝업이 떠있지 않다고 판단
    public bool IsShowing() {
        return _currentDialog != null;
    }
}
