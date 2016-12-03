using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;


public class SpawnerDemo : MonoBehaviour {

    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    public Text speechText;
    public float speed = 1.0f;
    public float spawnTime = 3f;
    public float spawnDelay = 2f;

    //Shapes
    public Rigidbody sphereObj;
    public Rigidbody cubeObj;
    public Rigidbody capsuleObj;
    public Transform spawnPos;

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
    
     //---SPAWNERS---\\
        keywords.Add("Spawn Cube", () =>
        {
            InvokeRepeating("spawnCube", spawnDelay, spawnTime);
        });

        keywords.Add("Spawn Sphere", () =>
        {
            InvokeRepeating("spawnSphere", spawnDelay, spawnTime);
        });

        keywords.Add("Spawn Capsule", () =>
        {
            InvokeRepeating("spawnCapsule", spawnDelay, spawnTime);
        });

        //Create the keyword recognizer and tell it what to recognize:
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        //Register for the OnPhraseRecognized event
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;

        //Start recognize
        keywordRecognizer.Start();

    }

    void spawnCube()
    {
        CancelInvoke("spawnSphere");
        CancelInvoke("spawnCapsule");
        Debug.Log("Cube");
        Instantiate(cubeObj, spawnPos.position, spawnPos.rotation);
        speechText.text = "Cube Spawned";
    }

    void spawnSphere()
    {
        CancelInvoke("spawnCube");
        CancelInvoke("spawnCapsule");
        Debug.Log("Sphere");
        Instantiate(sphereObj, spawnPos.position, spawnPos.rotation);
        speechText.text = "Sphere Spawned";
    }

    void spawnCapsule()
    {
        CancelInvoke("spawnCube");
        CancelInvoke("spawnSphere");
        Debug.Log("Capsule");
        Instantiate(capsuleObj, spawnPos.position, spawnPos.rotation);
        speechText.text = "Capsule Spawned";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
