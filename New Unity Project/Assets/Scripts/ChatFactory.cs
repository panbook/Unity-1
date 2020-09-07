using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatFactory : MonoBehaviour
{
    public DialogData dialog;

    public GameObject prefab1;
    public GameObject prefab2;
    public Transform contentTransform;

    //flaga od migającego kursora
    bool afterWarmup = false;
    bool isBotIsWriting = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        for (int i = 0; i < dialog.dialogData.Length; i++)
        {
            yield return new WaitUntil(() => !isBotIsWriting);
            InstantiateChatItem(dialog.dialogData[i]);
        }
        //InvokeRepeating("InstantiateChatItem", 1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator InstantiateChatItemCoroutine(BotSentence botSentence)
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

    void InstantiateChatItem(BotSentence botSentence)
    {
        StartCoroutine(InstantiateChatItemCoroutine(botSentence));
    }
}
