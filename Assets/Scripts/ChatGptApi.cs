using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

public class ChatGptApi : MonoBehaviour
{
    private const string ApiEndpoint = "https://api.openai.com/v1/chat/completions";
    private const string ApiKey = "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";
    private const string chatModel = "gpt-3.5-turbo";

    public async Task<string> GetChatResponse(string prompt)
    {
        using (HttpClient client = new HttpClient())
        {
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
