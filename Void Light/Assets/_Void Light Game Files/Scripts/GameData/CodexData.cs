using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/CodexData")]
public class CodexData : ScriptableObject {

    public CodexType codexType;
    public string title;
    [TextArea]
    public string summary;
    public Sprite runeImage;
    public int fontSize;

}
