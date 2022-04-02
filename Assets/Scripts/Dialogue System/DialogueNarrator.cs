using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterIdentity
{
    None,
    Ashura,
    Sensei,
    Shujin
}

[CreateAssetMenu(fileName = "Character Narrator", menuName = "Dialogue System/Create Character Narrator", order = 1)]
public class DialogueNarrator : ScriptableObject
{
    public CharacterIdentity characterIdentity;
    public Sprite characterAvatar;
}
