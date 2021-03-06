﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class ChatFactory : MonoBehaviour
{
    public DialogData dialog;

    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject playerAnswer;
    public Transform contentTransform;

    //flaga od migającego kursora
    bool afterWarmup = false;
    bool isBotIsWriting = false;

    // Start is called before the first frame update
    void Start()
    {

        //yield return new WaitUntil(() => !isBotIsWriting);
        InstantiateChatItem(dialog.dialogData[0]);

        //InvokeRepeating("InstantiateChatItem", 1, 3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator InstantiateChatItemCoroutine(Sentence botSentence)
    {
        //flaga od migającego kursora
        afterWarmup = false;

        isBotIsWriting = true;


        GameObject chatItem = Instantiate(prefab1, contentTransform);
        Text textComponent = chatItem.GetComponentInChildren<Text>();
        yield return null;
        StartCoroutine(WaitForText(textComponent, botSentence.waitTime));
        yield return new WaitUntil(() => afterWarmup);

        for (int i = 0; i < botSentence.sentence.Length; i++)
        {
            yield return new WaitForSeconds(botSentence.writingSpeed);
            chatItem.GetComponentInChildren<Text>().text += botSentence.sentence[i];
        }

        isBotIsWriting = false;
        if (botSentence.idAnswers.Length == 1)
        {
            //Sentence result = (Sentence)dialog.dialogData.Where<Sentence>(item => item.id == botSentence.idAnswers[0]);


            InstantiateChatItem(GetSentenceById(botSentence.idAnswers[0]));
        }

        if (botSentence.idAnswers.Length > 1)
        {
            GeneratePlayerAnswers(botSentence);
        }
    }

    private void GeneratePlayerAnswers(Sentence botSentence)
    {
        List<GameObject> answersButtons = new List<GameObject>();

        for (int i = 0; i < botSentence.idAnswers.Length; i++)
        {
            GameObject playerAnswerButton = Instantiate(playerAnswer, contentTransform);
            answersButtons.Add(playerAnswerButton);
            playerAnswerButton.GetComponentInChildren<Text>().text = GetSentenceById(botSentence.idAnswers[i]).sentence;
            playerAnswerButton.GetComponentInChildren<Button>().onClick.AddListener(() => playerChoice(answersButtons, i, botSentence));
        }
    }

    private void playerChoice(List<GameObject> playerAnswers, int id, Sentence botSentence)
    {
        // JAK TO ZROBIĆ?!
        for (int i = 0; i < playerAnswers.Count; i++)
        {
            if (i != id)
            {
                Destroy(playerAnswers[i]);
            }
        }
    }

    Sentence GetSentenceById(int id)
    {
        for (int i = 0; i < dialog.dialogData.Length; i++)
        {
            if (dialog.dialogData[i].id == id)
            {
                return dialog.dialogData[i];
            }
        }

        return null;
    }

    IEnumerator WaitForText(Text textComponent, float waitCount = 0)
    {
        for (int i = 0; i < waitCount; i++)
        {
            textComponent.text = "|";
            yield return new WaitForSeconds(0.2f);
            textComponent.text = "";
            yield return new WaitForSeconds(0.2f);
        }

        afterWarmup = true;
    }

    void InstantiateChatItem(Sentence botSentence)
    {
        StartCoroutine(InstantiateChatItemCoroutine(botSentence));
    }
}
