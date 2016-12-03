using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;

public class NLPTest : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    public string commandLog;
    public int units;
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
    void Start()
    {
        speechText.text = "";

        //---COMMANDS---\\

        keywords.Add("Left", () =>
        {
            Debug.Log("L");
            commandLog += "Left ";

        });

        keywords.Add("Right", () =>
        {
            Debug.Log("R");
            commandLog += "Right ";
        });

        keywords.Add("Up", () =>
        {
            Debug.Log("U");
            commandLog += "Up ";
        });

        keywords.Add("Down", () =>
        {
            Debug.Log("D");
            commandLog += "Down ";
        });

        keywords.Add("Stop", () =>
        {
            Debug.Log("Stop");
            moveVector = new Vector3(0, 0, 0);
        });

        keywords.Add("Slowly", () =>
        {
            commandLog += "1";
            units = 1;
        
        });

        keywords.Add("Medium", () =>
        {
            commandLog += "2";
            units = 2;

        });

        keywords.Add("Fast", () =>
        {
            commandLog += "3";
            units = 3;

        });




        //Create the keyword recognizer and tell it what to recognize:
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        //Register for the OnPhraseRecognized event
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;

        //Start recognize
        keywordRecognizer.Start();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveVector * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            commandLog += "Up ";
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            commandLog += "Down ";
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            commandLog += "Left ";
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            commandLog += "Right ";
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            commandLog += "Stop";
        }

        //------------------------------------

        if (Input.GetKeyDown(KeyCode.A))
        {
            commandLog += "1";
            units = 1;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            commandLog += "2";
            units = 2;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            commandLog += "3";
            units = 3;
        }

        //------------------------------------

        if (commandLog == "Left " + units)
        {
            Debug.Log("left " + units);
            moveVector = new Vector3(-units, 0, 0);
            EraseLog();
        }

        if (commandLog == "Right " + units)
        {
            Debug.Log("right " + units);
            moveVector = new Vector3(units, 0, 0);
            EraseLog();
        }

        if (commandLog == "Up " + units)
        {
            Debug.Log("up " + units);
            moveVector = new Vector3(0, units, 0);
            EraseLog();
        }

        if (commandLog == "Down " + units)
        {
            Debug.Log("down " + units);
            moveVector = new Vector3(0, -units, 0);
            EraseLog();
        }

        if (commandLog == "Stop")
        {
            Debug.Log("stop " + units);
            moveVector = new Vector3(0, 0, 0);
            EraseLog();
        }
    }

    void EraseLog()
    {
        commandLog = "";
    }
}

