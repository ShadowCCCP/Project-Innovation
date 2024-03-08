using System.IO;
using System.Text.RegularExpressions;
using HuggingFace.API;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechRecognitionTest : MonoBehaviour
{
    //[SerializeField] private Button startButton;
    //[SerializeField] private Button stopButton;
    [SerializeField] Button button;
    HoldDownButton buttonScript;
    Image buttonImage;

    [SerializeField] TextMeshProUGUI text;

    private AudioClip clip;
    private byte[] bytes;
    private bool recording;

    Contact[] contacts;

    private void Start()
    {
        buttonScript = button.GetComponent<HoldDownButton>();
        contacts = GameManager.Instance.GetContacts();
    }

    private void Update()
    {
        if (recording && Microphone.GetPosition(null) >= clip.samples)
        {
            StopRecording();
        }

        CheckButtonPress();
    }

    private void CheckButtonPress()
    {
        if (buttonScript.IsPressed() && !recording)
        {
            StartRecording();
        }
        else if(!buttonScript.IsPressed() && recording)
        {
            StopRecording();
        }
    }

    private void StartRecording()
    {
        text.color = Color.black;
        //text.text = "Speak...";
        clip = Microphone.Start(null, false, 10, 44100);
        recording = true;
    }

    private void StopRecording()
    {
        var position = Microphone.GetPosition(null);
        Microphone.End(null);
        var samples = new float[position * clip.channels];
        clip.GetData(samples, 0);
        bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);
        recording = false;
        SendRecording();
    }

    private void SendRecording()
    {
        text.color = Color.black;
        //text.text = "Wait...";
        //stopButton.interactable = false;
        HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response => {
            CheckForWords(response);
        }, error => {
            CheckForWords(error);
        });
    }

    private byte[] EncodeAsWAV(float[] samples, int frequency, int channels)
    {
        using (var memoryStream = new MemoryStream(44 + samples.Length * 2))
        {
            using (var writer = new BinaryWriter(memoryStream))
            {
                writer.Write("RIFF".ToCharArray());
                writer.Write(36 + samples.Length * 2);
                writer.Write("WAVE".ToCharArray());
                writer.Write("fmt ".ToCharArray());
                writer.Write(16);
                writer.Write((ushort)1);
                writer.Write((ushort)channels);
                writer.Write(frequency);
                writer.Write(frequency * channels * 2);
                writer.Write((ushort)(channels * 2));
                writer.Write((ushort)16);
                writer.Write("data".ToCharArray());
                writer.Write(samples.Length * 2);

                foreach (var sample in samples)
                {
                    writer.Write((short)(sample * short.MaxValue));
                }
            }
            return memoryStream.ToArray();
        }
    }

    bool ContainsWord(string input, string word)
    {
        string pattern = "\\b" + Regex.Escape(word) + "\\b";
        Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
        return regex.IsMatch(input);
    }

    void CheckForWords(string text)
    {
        if (ContainsWord(text, "yes") || ContainsWord(text, "okay") || ContainsWord(text, "yeah"))
        {
            contacts[CallPickerOrdered.GetCurrentCaller()].AddAnswer(Contact.Answers.Yes);
            UDPSender.SendBroadcast("Answer: Yes");
            button.gameObject.SetActive(false);

        }
        else if (ContainsWord(text, "no"))
        {
            contacts[CallPickerOrdered.GetCurrentCaller()].AddAnswer(Contact.Answers.No);
            UDPSender.SendBroadcast("Answer: No");
            button.gameObject.SetActive(false);
        }
        else
        {
            UDPSender.SendBroadcast("Answer: Unclear");
        }

        //this.text.text = "Push to talk";
    }
}