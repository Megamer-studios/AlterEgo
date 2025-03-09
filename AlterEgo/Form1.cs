using System;
using CharacterAiNetApiWrapper;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;
using System.IO;

namespace AlterEgo
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer synth = new SpeechSynthesizer();

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(@"Sprites/Ch1.png");
            synth.SetOutputToDefaultAudioDevice();
            synth.SelectVoiceByHints(VoiceGender.Female);
            Console.ForegroundColor = ConsoleColor.Green;
        }

        private async Task<string> GetChatGPTResponse(string prompt)
        {
            
               
            string apiKey = "API KEY HERE"; // Replace with your actual API key
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "gpt-3.5-turbo", // Updated model
                    messages = new[]
                    {
                        new { role = "user", content = prompt }
                    },
                    max_tokens = 150
                };

                var jsonRequestBody = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                int maxRetries = 5;
                int delay = 1000; // Initial delay in milliseconds

                for (int i = 0; i < maxRetries; i++)
                {
                    var response = await client.PostAsync("https://api.openai.com/v1/completions", content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    // Log the response string for debugging
                    Console.WriteLine($"Response: {responseString}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseObject = JsonConvert.DeserializeObject<dynamic>(responseString);
                        if (responseObject == null || responseObject.choices == null || responseObject.choices.Count == 0)
                        {
                            return "Error: Unable to get a response from ChatGPT.";
                        }

                        return responseObject.choices[0].text ?? "Error: Unable to get a response from ChatGPT.";
                    }
                    else if (response.StatusCode == (System.Net.HttpStatusCode)429) // Too Many Requests
                    {
                        Console.WriteLine("Error: Too many requests. Retrying...");
                        await Task.Delay(delay);
                        delay *= 2; // Exponential backoff
                    }
                    else
                    {
                        return $"Error: {response.ReasonPhrase}";
                    }
                }

                return "Error: Too many requests. Please try again later.";
            }
        }

        public void RecogniseSpeech()
        {
            SpeechRecognitionEngine sr = new SpeechRecognitionEngine();
            sr.SetInputToDefaultAudioDevice();
            Grammar word = new DictationGrammar();
            sr.LoadGrammar(word);   

           RecognitionResult result = sr.Recognize();
            richTextBox1.Text = result.Text;
            oshit(result.Text);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            RecogniseSpeech();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Button2 clicked");
          
        }

        public async void oshit(string pr)
        {
            timer1.Enabled = true;
            Console.WriteLine("oshit method called");
            string prompt = pr;
            Console.WriteLine($"Prompt: {prompt}");
            string response = await GetChatGPTResponse(prompt);
            Console.WriteLine($"Response: {response}");
            richTextBox2.Text = response;
            synth.Speak(response);
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            int num = rand.Next(0, 5);
            if (num == 0)
            {
                pictureBox1.Image = Image.FromFile(@"Sprites/Ch1.png");
            }
            else if (num == 1)
            {
                pictureBox1.Image = Image.FromFile(@"Sprites/Ch2.png");
            }
            else if (num == 2)
            {
                pictureBox1.Image = Image.FromFile(@"Sprites/Ch3.png");
            }
            else if (num == 3)
            {
                pictureBox1.Image = Image.FromFile(@"Sprites/Ch4.png");
            }
            else if (num == 4)
            {
                pictureBox1.Image = Image.FromFile(@"Sprites/Ch5.png");
            }
        }
    }
}
