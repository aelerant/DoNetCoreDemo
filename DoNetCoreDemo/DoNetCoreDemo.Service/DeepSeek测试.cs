using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetCoreDemo.Service
{
    public class DeepSeekService
    {
        private static readonly HttpClient client = new HttpClient();
        private const string ApiKey = "sk-df17d6b782594442b86684d16936c103"; // 替换为你的 API Key
        private const string ApiUrl = "https://api.deepseek.com/v1/chat/completions"; // 确认接口地址

        public static async Task DeepSeekTestAsync()
        {
            var requestBody = new
            {
                model = "deepseek-chat", // 指定模型，如 deepseek-chat、deepseek-coder
                messages = new[]
                {
                new { role = "user", content = "你好，请介绍 .NET 中的 HttpClient 用法。" }
            }
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ApiKey);

            try
            {
                var response = await client.PostAsync(ApiUrl, content);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(responseBody);
                Console.WriteLine(result.choices[0].message.content);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"请求错误: {ex.Message}");
            }
        }
    }
}
