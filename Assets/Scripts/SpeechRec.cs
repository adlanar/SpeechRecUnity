using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;

public class SpeechRec : MonoBehaviour {
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    public Text speechText;
    public float speed = 1.0f;
    public Vector3 moveVector = Vector3.zero;
    
    //Handler
    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        // if the keyword recognized is in our dictionary, call that Action.
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    // Use this for initialization
    void Start () {
        speechText.text = "";

    //---TEXT TESTS---\\

        keywords.Add("hello world", () =>
        {
            Debug.Log("hello world");
            speechText.text = "Word recognized: hello world";
        });

        keywords.Add("hi", () =>
        {
            Debug.Log("hi");
            speechText.text = "Word recognized: hi";
        });

    //---COMMANDS---\\

        keywords.Add("Move to the North", () =>
        {
            Debug.Log("N");
            speechText.text = "Command recognized: North";
            //rb.AddForce(Vector3.forward * speed * Time.deltaTime);
            moveVector = new Vector3(0, 0, speed);
        });

        keywords.Add("Move to the South", () =>
        {
            Debug.Log("S");
            speechText.text = "Command recognized: South";
            moveVector = new Vector3(0, 0, -speed);
        });

        keywords.Add("Move to the West", () =>
        {
            Debug.Log("W");
            speechText.text = "Command recognized: West";
            moveVector = new Vector3(-speed, 0, 0);
        });

        keywords.Add("Move to the East", () =>
        {
            Debug.Log("E");
            speechText.text = "Command recognized: East";
            moveVector = new Vector3(speed, 0, 0);
        });

        keywords.Add("Stop", () =>
        {
            Debug.Log("Stop");
            speechText.text = "Command recognized: Stop";
            moveVector = new Vector3(0, 0, 0);
        });


        //Create the keyword recognizer and tell it what to recognize:
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        //Register for the OnPhraseRecognized event
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        
        //Start recognize
        keywordRecognizer.Start();

    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(moveVector * Time.deltaTime);
	}
}
