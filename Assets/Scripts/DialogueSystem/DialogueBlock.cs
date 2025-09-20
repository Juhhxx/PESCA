using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DialogueBlock
{
    [field: SerializeField] public string DialogueTag { get; private set; }

    [field: SerializeField] public List<DialogueSentence> Sentences { get; private set; }
}