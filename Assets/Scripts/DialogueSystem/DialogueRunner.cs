using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class DialogueRunner : MonoBehaviour
{
    [SerializeField] private string _dialogueTag;

    DialogueBlock _currentDialogue;

    [Button(enabledMode: EButtonEnableMode.Always)]
    public void PlayDialogue()
    {
        _currentDialogue = DialogueManager.Instance.GetDialogue(_dialogueTag);

        StopAllCoroutines();
        StartCoroutine(PlayDialogueCR());
    }

    private IEnumerator PlayDialogueCR()
    {
        Queue<DialogueSentence> dialogueQueue =
        new Queue<DialogueSentence>(_currentDialogue.Sentences);

        while (dialogueQueue.Count > 0)
        {
            DialogueSentence d = dialogueQueue.Dequeue();

            DialogueShower.Instance.PlayDialogue(d);

            float audioTime = d.Audio != null ? d.Audio.length : 0;

            float waitTime = audioTime + d.TimeToNext;

            yield return new WaitForSeconds(waitTime);
        }
    }
}
 