using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;

public class SnakeSpeech : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    public Vector3 moveVector = Vector3.zero;
    public float speed = 5f;

    public Text scoreText;
    public Text speechText;
    public Text loseText;

    public int scoreValue;

    public GameObject tailPrefab;

    bool ate = false;


    List<Transform> tail = new List<Transform>();

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

    void Start()
    {
        //Default direction
        moveVector = new Vector3(0, 0, speed);

        //Set text to null
        speechText.text = "";

        //Move snake every 300ms
        InvokeRepeating("Move", 0.3f, 0.3f);

        //---COMMANDS---\\

        keywords.Add("Up", () =>
        {
            Debug.Log("U");
            speechText.text = "Command recognized: Up";
            //rb.AddForce(Vector3.forward * speed * Time.deltaTime);
            moveVector = new Vector3(0, 0, speed);
        });

        keywords.Add("Down", () =>
        {
            Debug.Log("D");
            speechText.text = "Command recognized: Down";
            moveVector = new Vector3(0, 0, -speed);
        });

        keywords.Add("Left", () =>
        {
            Debug.Log("L");
            speechText.text = "Command recognized: Left";
            moveVector = new Vector3(-speed, 0, 0);
        });

        keywords.Add("Right", () =>
        {
            Debug.Log("R");
            speechText.text = "Command recognized: Right";
            moveVector = new Vector3(speed, 0, 0);
        });

        keywords.Add("Stop", () =>
        {
            Debug.Log("S");
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

    void OnTriggerEnter(Collider coll)
    {
        if (coll.name.StartsWith("Food"))
        {
            // Get longer in next Move call
            ate = true;

            // Remove the Food
            Destroy(coll.gameObject);
        }
        // Collided with Tail or Border
        else
        {
            Destroy(this.gameObject);
            loseText.text = "oh. ok, you lose.";
        }
    }

    void Move()
    {
        //Save current position
        Vector3 headPos = transform.position;

        //Move into new direction
        transform.Translate(moveVector * Time.deltaTime);

        if (ate)
        {
            //Add score
            scoreValue += 1;
            scoreText.text = scoreValue.ToString();

            //Load tail behind head
            GameObject g = (GameObject)Instantiate(tailPrefab, headPos, Quaternion.identity);

            //Keep track in tail list
            tail.Insert(0, g.transform);

            //Reset state
            ate = false;
        }

        // If has a tail
        else if (tail.Count > 0)
        {
            // Move last Tail Element to where the Head was
            tail.Last().position = headPos;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }


    void Update()
    {


    }
}
