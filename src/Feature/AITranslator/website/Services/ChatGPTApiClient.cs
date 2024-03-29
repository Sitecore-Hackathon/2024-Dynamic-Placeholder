﻿using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Sitecore.Configuration;
using Newtonsoft.Json;
using System.Threading;
using Sitecore.Diagnostics;
using Sitecore.Data.Items;

public class ChatGPTApiClient
{
    private readonly HttpClient _httpClient;
    private const string ApiUrl = "https://api.openai.com/v1/chat/completions";

    public ChatGPTApiClient()
    {
        Sitecore.Data.Database master = Sitecore.Configuration.Factory.GetDatabase("master");
        Item apiKeyItem = master.GetItem("{7827E662-E253-4E06-B827-73D36DD4CA30}");
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKeyItem["API KEY"]);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
    }

    public string GenerateResponse(string prompt)
    {
        var requestData = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
                {
                    new { role = "user", content = prompt}
                },
            temperature = 0.7,
            max_tokens = 1000
        };

        var jsonContent = JsonConvert.SerializeObject(requestData);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = _httpClient.PostAsync(ApiUrl, content).Result;
        var responseString = response.Content.ReadAsStringAsync().Result;
        Thread.Sleep(3000);
        //Serialize string to json
        var serializedResponse = JsonConvert.DeserializeObject<dynamic>(responseString);

        var contentResponse = serializedResponse.choices[0].message.content.ToString();
        return(contentResponse);

    }
}