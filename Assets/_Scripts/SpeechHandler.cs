using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

/**
 * 
 * Author: Ethan Horrigan
 * 
 * Adapted from: https://docs.unity3d.com/ScriptReference/Windows.Speech.KeywordRecognizer.html
 * 
 */
public class SpeechHandler : MonoBehaviour
{
    protected PhraseRecognizer recognizer;
    public string []keywords = new string[] { "pause", "play", "continue", "quit" };
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    protected string word;

    
    // Start is called before the first frame update
    void Start()
    {
        //if(keywords != null)
        //{
        //    recognizer = new KeywordRecognizer(keywords, confidence);
        //    recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
        //    recognizer.Start();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        //word = args.text;
        //Debug.Log(word);
    }
    private void OnApplicationQuit()
    {
        //if(recognizer != null && recognizer.IsRunning)
        //{
        //    recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
        //    recognizer.Stop();
        //}
    }
}
