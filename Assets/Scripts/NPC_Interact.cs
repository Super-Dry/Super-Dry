using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class NPC_Interact : MonoBehaviour
{
    private Canvas BubbleCanvas;
    public TextMeshProUGUI textMeshPro;
    private string chat_text;
    private int parser;
    private bool ChatUpdate;
    private string objectName;
    void Start()
    {
        BubbleCanvas = GetComponent<Canvas>();
        BubbleCanvas.enabled = false;
        chat_text = "";
        parser = 0;
        ChatUpdate = false;
    }

    async void Update()
    {
        if(ChatUpdate)
        {
            ChatUpdate = false;
            await ChatQuery(objectName);
            
        }

        if(chat_text != null && parser < chat_text.Length)
        {
            textMeshPro.text += chat_text[parser];
            parser++;
        }
    }

    public void Interact(string newObjectName)
    {
        BubbleCanvas.enabled = true;
        objectName = newObjectName;
        ChatUpdate = true;
    }

    public void Neglect()
    {
        chat_text = "";
        textMeshPro.text = "";
        parser = 0;
        BubbleCanvas.enabled = false;
    }

    private async Task ChatQuery(string objectName)
    {
        string prompt;
        var chatGptApi = new ChatGptApi();
        if(objectName == "Big Vegas@Bellydancing")
        {
            prompt = "Pretend that you are elvis presley and brag about yourself in 25 words max";
        }
        else if (objectName == "kaya@Nervously Look Around")
        {
            prompt = "warn me about a danger of one Government agent in a scary tone in 25 words max";
        }
        else
        {
            prompt = "Unknown";
        }

        chat_text = await chatGptApi.GetChatResponse(prompt);
        JObject responseObj = JObject.Parse(chat_text);
        chat_text = responseObj["choices"][0]["message"]["content"].ToString();
    }
}