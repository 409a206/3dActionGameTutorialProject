using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogData
{
    public DialogType Type {get; set;}

    public DialogData(DialogType type) {
        this.Type = type;
    }
}
