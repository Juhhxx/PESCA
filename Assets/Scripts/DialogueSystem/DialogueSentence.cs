using System;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

[Serializable]
public class DialogueSentence
{
    [field: TextArea]
    [field: SerializeField] public string Dialogue { get; private set; }

    [field: AllowNesting]
    [field: TextArea]
    [field: ShowIf("DifferentDialog")]
    [field: SerializeField] public string Dialogue2 { get; private set; }

    [field: SerializeField] public AudioClip Audio { get; private set; }

    [field: SerializeField] public DialogueSide Side { get; private set; }
    public enum DialogueSide { Left, Right, Both, Center}

    [field: AllowNesting]
    [field: ShowIf("Side", DialogueSide.Both)]
    [field: SerializeField] public bool DifferentDialog { get; private set; }

    [field: SerializeField] public float TimeToNext { get; private set; }

    public UnityEvent OnDialoguePlayed;
}