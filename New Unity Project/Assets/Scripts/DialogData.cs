using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogData : ScriptableObject
{
    [TextArea(3, 6)]
    public string[] sentences;
}
