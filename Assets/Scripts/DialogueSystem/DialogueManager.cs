using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviourDDOL<DialogueManager>
{
    [SerializeField] private List<DialogueBlock> _dialogues;

    private void Start()
    {
        base.SingletonCheck(this);
    }

    public DialogueBlock GetDialogue(string tag)
    {
        foreach (DialogueBlock dB in _dialogues)
        {
            if (dB.DialogueTag == tag) return dB;
        }

        throw new Exception("Dialogue Tag Not Found!");
    }
}
