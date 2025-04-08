using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;

class CutScene : MonoBehaviour
{
    public GlobalEvent activateEvent;
    public float deltaT = 0.1f;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    public int currDialogue = 0;
    private bool isDialogueEnd;
    void Start()
    {
        EventManager.Instance.AddListener(activateEvent, StartAnim);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        animator.enabled = false;
    }

    void StartAnim()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        animator.enabled = true;
    }
    private IEnumerator TypeSentence(string sentence)
    {     
        bool flag = false;
        for (int i = 0; i < sentence.Length; i++)
        {
            dialogueText.text = sentence.Substring(0, i + 1);
            for (float t = 0; t < deltaT; t += Time.deltaTime)
            {
                if (Input.anyKeyDown)
                {
                    dialogueText.text = sentence;
                    flag = true;
                    break;
                }
                yield return null;
            }
            if (flag) { break; }
        }
        yield return null;
        while (!Input.anyKeyDown) { yield return null; }
        yield return null;
        if (isDialogueEnd) { EventManager.Instance.Invoke(GlobalEvent.LevelEnded); }
        else { animator.SetTrigger("next"); }
        yield break;
    }

    public void EndDialogue()
    {
        isDialogueEnd = true;
    }
}