using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.IO;

public class ChatGptApi : MonoBehaviour
{
    private const string ApiEndpoint = "https://api.openai.com/v1/chat/completions";
    private string ApiKey;
    private const string chatModel = "gpt-3.5-turbo";

    //Reading the text file containing chatgpt api key
    public TextAsset textAsset = Resources.Load<TextAsset>("apiKey");

    public async Task<string> GetChatResponse(string prompt)
    {

        using (HttpClient client = new HttpClient())
        {
            
            
            if (textAsset != null)
            {
                //setting the api key to the string variable
                ApiKey = textAsset.text;
                Debug.Log("API Key: " + ApiKey);
            }
            else
            {
                Debug.LogWarning("API Key file not found.");
            }
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");

            // var jsonContent = new StringContent($"{{\"model\": \"{chatModel}\", \"messages\": [{{\"role\": \"user\", \"content\": \"Make up a story about aligators\"}}]}}");
            JObject jsonContent = new JObject(
                new JProperty("model", chatModel),
                new JProperty("messages",
                    new JArray(
                        new JObject(
                            new JProperty("role", "user"),
                            new JProperty("content", prompt)
                        )
                    )
                )
            );
            var stringContent = new StringContent(jsonContent.ToString(), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ApiEndpoint, stringContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }
    }
}
