using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FollowingCamera
//주인공 캐릭터를 카메라가 일정한 거리를 유지한 채로 따라다니게 합니다.
public class FollowingCamera : MonoBehaviour
{
    public float distanceAway = 7f;
    public float distanceUp = 4f;

    //따라다닐 객체를 지정합니다.
    public Transform follow;

    private void LateUpdate() {
        //카메라의 위치를 distanceUp만큼 위에, distanceAway만큼 앞에 위치시킵니다.
        transform.position = follow.position + Vector3.up * distanceUp - Vector3.forward * distanceAway;    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
