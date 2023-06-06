using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationCenter : MonoBehaviour
{
   //singleton patter
   private static readonly NotificationCenter instance  =new NotificationCenter();
   public static NotificationCenter Instance {
       get {
           return instance;
       }
   }
    //

    public delegate void UpdateDelegator();
    public enum Subject
    {
        PlayerData,
    }

    Dictionary<Subject, UpdateDelegator> _delegateMap;

    private NotificationCenter() {
        _delegateMap = new Dictionary<Subject, UpdateDelegator>();
    }

}
