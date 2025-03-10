# 🎭 AlterEgo - AI-Powered Virtual Assistant

AlterEgo is a **C# desktop application** inspired by the *Danganronpa* series, featuring an AI assistant with **speech recognition**, **text-to-speech synthesis**, and **dynamic character animations**. This interactive assistant communicates using OpenAI's GPT model, bringing your AI conversations to life!

![User Interface](https://github.com/Megamer-studios/AlterEgo/blob/master/image_2025-03-09_154149416.png "GUI")


---

## ✨ Features

✅ **Speech Recognition** - Converts spoken words into text.  
✅ **AI-Powered Responses** - Uses OpenAI's GPT model to generate realistic conversations.  
✅ **Text-to-Speech (TTS)** - Reads responses aloud for a more immersive experience.  
✅ **Dynamic Character Animations** - Changes character expressions based on interactions.  

---

## 📦 Installation

### 🔧 Prerequisites
Ensure you have the following installed:
- .NET Framework 4.7.2 or later
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) (for JSON handling)
- `System.Speech` (for speech recognition and synthesis)

### 🚀 Cloning the Repository
```sh
git clone https://github.com/Megamer-studios/AlterEgo/
cd AlterEgo
```

### 🔑 Setting Up API Key
1. Obtain an OpenAI API key from [OpenAI](https://openai.com/).
2. Replace `API KEY HERE` in `GetChatGPTResponse()` with your actual API key.

---

## 🎮 How to Use

1. **Run the application.**
2. **Click the speech recognition button** to speak to the AI.
3. **Wait for a response** - the AI will process your input and reply via text and voice.
4. **Enjoy dynamic visuals** - the character sprite changes based on interactions.

---

## 🛠 Code Overview

### 📌 `Form1_Load(object sender, EventArgs e)`
- Initializes the UI and loads the default character sprite.
- Configures the speech synthesizer to use a **female voice**.
- Sets console text color for debugging.

### 🤖 `GetChatGPTResponse(string prompt)`
- Sends user input to OpenAI's GPT model.
- Implements **exponential backoff retry logic** for handling API rate limits.
- Returns the AI-generated response or an error message if unsuccessful.

### 🎤 `RecogniseSpeech()`
- Uses `SpeechRecognitionEngine` to capture and transcribe speech.
- Displays the recognized text in `richTextBox1`.
- Passes the transcribed text to the AI via `oshit(result.Text)`.

### 💬 `oshit(string pr)`
- Sends the recognized speech or text input to OpenAI.
- **Updates** `richTextBox2` with the AI-generated response.
- Uses `synth.Speak(response)` to read the response aloud.
- Activates a **timer** to trigger character animations.

### 🎭 `timer1_Tick(object sender, EventArgs e)`
- **Randomly selects** one of five character sprites to display.
- Creates a more **dynamic and engaging** user interface.

### 🎛 `button1_Click(object sender, EventArgs e)`
- Triggers **speech recognition** when the user clicks Button 1.


---

## 🔗 Dependencies

📌 [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) - JSON serialization/deserialization.  
📌 `System.Speech` - Speech recognition and synthesis.  



---

## 💡 Contributing

🚀 Pull requests are welcome! For major changes, please open an **issue** first to discuss proposed modifications.

---

## 📬 Contact

For any issues, feel free to contact **[cozoral@outlook.com]**.

---

