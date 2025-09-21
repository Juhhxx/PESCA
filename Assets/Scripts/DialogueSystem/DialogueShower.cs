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
    [SerializeField] private GameObject _bothDialoguePrefab;
    [SerializeField] private GameObject _centerDialoguePrefab;

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

        GameObject prefab;

        switch (sentence.Side)
        {
            case DialogueSentence.DialogueSide.Left:
                prefab = _leftDialoguePrefab;
                break;

            case DialogueSentence.DialogueSide.Right:
                prefab = _rightDialoguePrefab;
                break;

            case DialogueSentence.DialogueSide.Both:
                prefab = _bothDialoguePrefab;
                break;

            case DialogueSentence.DialogueSide.Center:
                prefab = _centerDialoguePrefab;
                break;

            default:
                prefab = null;
                break;
        }

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

        TextMeshProUGUI[] tmps = textBubble.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (TextMeshProUGUI tmp in tmps) tmp.text = sentence.Dialogue;

        if (sentence.Dialogue == "")
        {
            Transform child = textBubble.transform.GetChild(0);

            child.gameObject.SetActive(false);
        }

        _dialoguesList.Add(textBubble);
    }

    public void ClearDialogues()
    {
        foreach (GameObject d in _dialoguesList) Destroy(d);

        _dialoguesList.Clear();
    }
}
