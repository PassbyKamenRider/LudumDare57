using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor.Animations;

class CutScene : MonoBehaviour
{
    public float deltaT = 0.1f;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    public int currDialogue = 0;
    void OnEnable()
    {
        
    }

    // public void PlayNextDialogue()
    // {
    //     StartCoroutine(TypeSentence(sentences[currDialogue]));
    //     currDialogue += 1;
    // }

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
        animator.SetTrigger("next");
        yield break;
    }

    public void EndDialogue()
    {
        Debug.Log("Dialogue ends");
    }
}