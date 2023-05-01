using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TypeEffect : MonoBehaviour
{
    public GameObject DiaEndCursor;
    public AudioSource audioSource;
    public bool isAnimation;
    string targetDialogue;
    public int TypeSpeed;
    Text dialogueText;
    int index;
    float interval;

    private void Awake()
    {
        dialogueText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetDialogue(string Dia)
    {
        if (isAnimation)
        {
            dialogueText.text = targetDialogue;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            targetDialogue = Dia;
            EffectStart();
        }

    }

    void EffectStart()
    {
        dialogueText.text = "";
        index = 0;
        DiaEndCursor.SetActive(false);

        interval = 1.0f / TypeSpeed;
        isAnimation = true;

        Invoke("Effecting", interval);
    }
    void Effecting()
    {
        if (dialogueText.text == targetDialogue)
        {
            EffectEnd();
            return;
        }
        dialogueText.text += targetDialogue[index];

        if (targetDialogue[index] != ' ' || targetDialogue[index] != '.')
        {
            audioSource.Play();
        }


        index++;
        Invoke("Effecting", interval);
    }
    void EffectEnd()
    {
        isAnimation = false;
        DiaEndCursor.SetActive(true);
    }
}
