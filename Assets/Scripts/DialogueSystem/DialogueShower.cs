using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueShower : MonoBehaviourDDOL<DialogueShower>
{
    [SerializeField] private GameObject _dialogueStack;
    [SerializeField] private float _lerpSpeed;

    [SerializeField] private GameObject _rightDialoguePrefab;
    [SerializeField] private GameObject _leftDialoguePrefab;

    [SerializeField][ReadOnly] private List<GameObject> _dialoguesList;

    private VerticalLayoutGroup _dialogueLayoutGroup;
    private RectOffset tempPadding;

    private void Start()
    {
        base.SingletonCheck(this);

        _dialoguesList = new List<GameObject>();
        _dialogueLayoutGroup = _dialogueStack.GetComponent<VerticalLayoutGroup>();
        
        tempPadding = new RectOffset(
            _dialogueLayoutGroup.padding.left,
            _dialogueLayoutGroup.padding.right,
            _dialogueLayoutGroup.padding.top,
            _dialogueLayoutGroup.padding.bottom);
    }

    public void PlayDialogue(DialogueSentence sentence)
    {
        Debug.Log(sentence.Dialogue);

        GameObject prefab = sentence.FromRight ? _rightDialoguePrefab : _leftDialoguePrefab;

        StartCoroutine(AddBubble(prefab, sentence));
    }

    private IEnumerator AddBubble(GameObject bubble, DialogueSentence sentence)
    {
        float bottom = 0;

        if (_dialoguesList.Count > 0)
        {
            while (_dialogueLayoutGroup.padding.bottom < 220)
            {
                bottom += _lerpSpeed * Time.deltaTime;

                tempPadding.bottom = (int)bottom;
                _dialogueLayoutGroup.padding = tempPadding;
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_dialogueStack.transform);

                Debug.Log($"BOTTPM PADDING : {_dialogueLayoutGroup.padding.bottom}");
                yield return null;
            }
        }

        tempPadding.bottom = 0;
        _dialogueLayoutGroup.padding = tempPadding;
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_dialogueStack.transform);

        GameObject textBubble = Instantiate(bubble, _dialogueStack.transform);

        textBubble.GetComponentInChildren<TextMeshProUGUI>().text = sentence.Dialogue;

        _dialoguesList.Add(textBubble);
    }
}
