using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DialogueSentence
{
    [field: TextArea]
    [field: SerializeField] public string Dialogue { get; private set; }
    [field: SerializeField] public AudioClip Audio { get; private set; }
    [field: SerializeField] public DialogueSide Side { get; private set; }
    public enum DialogueSide { Left, Right, Both, Center}
    [field: SerializeField] public float TimeToNext { get; private set; }
    public UnityEvent OnDialoguePlayed;
}