using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//스테이지를 관리하는 콘트롤러. 스테이지의 시작과 종료 지점에 스테이지의 시작과 마감을 처리
//스테이지에서 획득한 포인트도 여기에서 관리
public class StageController : MonoBehaviour
{
    //스테이지 콘트롤러의 인스턴스를 저장하는 static 변수
    public static StageController Instance;
    //StagePoint는 현재 스테이지에서 쌓은 포인트를 저장
    public int StagePoint = 0;
    //현재 포인트를 표시하는 Text 게임 오브젝트
    public Text PointText;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DialogDataAlert alert = new DialogDataAlert("START", "Game Start!", 
        delegate() {
            Debug.Log("OK Pressed");
        });
        

        DialogManager.Instance.Push(alert);

    }

    //StageController에서는 AddPoint()함수로 유저가 획득한 포인트를 저장
    public void AddPoint(int point) {
        StagePoint += point;
        PointText.text = StagePoint.ToString();
    }

    public void FinishGame() {
        //DialogDataConfirm 클래스의 인스턴스를 생성
        //이때 제목(Title), 내용(Message), 콜백 함수(delegate(bool yn))를 매개변수로 전달합니다.
        DialogDataConfirm confirm = new DialogDataConfirm("Restart?", "Please press OK if you want to restart the game.",
        delegate(bool yn) {
            if(yn) {
                Debug.Log("OK Pressed");
                Application.LoadLevel(Application.loadedLevel);
            } else {
                Debug.Log("Cancel Pressed");
                Application.Quit();
            }
        });
        
        //생성한 다이얼로그 데이터를 다이얼로그 메니저에게 전달
        DialogManager.Instance.Push(confirm);
    }

   
}
