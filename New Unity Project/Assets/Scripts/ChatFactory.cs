using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatFactory : MonoBehaviour
{
    public GameObject prefab1;
    public GameObject prefab2;
    public Transform contentTransform;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InstantiateChatItem", 1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator InstantiateChatItemCoroutine(float writeSpeed = 0.2f)
    {
        GameObject chatItem = Instantiate(prefab1, contentTransform);
        Text textComponent = chatItem.GetComponentInChildren<Text>();
        yield return null;
        StartCoroutine(WaitForText(textComponent, 5));
        // do dokończenia poza lekcją - czekanie na zakończenie korutyny

        for (int i = 0; i < 25; i++)
        {
            yield return new WaitForSeconds(writeSpeed);
            chatItem.GetComponentInChildren<Text>().text += "ab";
        }
    }

    IEnumerator WaitForText(Text textComponent, int waitCount = 0)
    {
        for (int i = 0; i < waitCount; i++)
        {
            textComponent.text = " |";
            yield return new WaitForSeconds(0.2f);
            textComponent.text = "";
            yield return new WaitForSeconds(0.2f);
        }
    }

    void InstantiateChatItem()
    {
        StartCoroutine(InstantiateChatItemCoroutine());
    }
}
