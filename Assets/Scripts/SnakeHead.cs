using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SnakeHead : MonoBehaviour
{

    public float speed = 5f;

    public Text scoreText;
    public Text speechText;
    public Text loseText;

    public int scoreValue;

    public GameObject tailPrefab;

    bool ate = false;
    
    //default direction
    Vector3 dir = Vector3.forward;

    List<Transform> tail = new List<Transform>();

    void Start()
    {
        //Set score to 0

        //Set text to null
        speechText.text = "";

        //Move snake every 300ms
        InvokeRepeating("Move", 0.3f, 0.3f);
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
               // ToDo 'You lose' screen
        }
    }

    void Move()
    {
        //Save current position
        Vector3 headPos = transform.position;

        //Move into new direction
        transform.Translate(dir);

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
        // Move in a new Direction?
        if (Input.GetKey(KeyCode.RightArrow))
            dir = Vector3.right;
        else if (Input.GetKey(KeyCode.DownArrow))
            dir = Vector3.back;
        else if (Input.GetKey(KeyCode.LeftArrow))
            dir = Vector3.left;
        else if (Input.GetKey(KeyCode.UpArrow))
            dir = Vector3.forward;


    }
}
