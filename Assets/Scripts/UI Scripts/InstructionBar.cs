using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionBar : MonoBehaviour
{
    [SerializeField]
    private Text instructionText;

    public virtual void SetInstruction(string text)
    {
        this.instructionText.text = text;
        this.ToggleInstructionBar(true);
    }

    public virtual void ToggleInstructionBar(bool toggle)
    {
        this.gameObject.SetActive(toggle);
    }

}
