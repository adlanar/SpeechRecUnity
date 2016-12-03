using UnityEngine;
using System.Collections;

public class FoodSpawn : MonoBehaviour {

    public GameObject foodPrefab;

    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    void Spawn()
    {
        // x posiiton between L & R border
        int x = (int)Random.Range(borderLeft.position.x, borderRight.position.x);
        
       // y position between T & B border
        int y = (int)Random.Range(borderBottom.position.z, borderTop.position.z);

        // Instantiate food at (x, y)
        Instantiate(foodPrefab, new Vector3(x, 0.5f, y), Quaternion.identity);
        Debug.Log("Food Spawn: " + x + ", " + y);
        //Instantiate(foodPrefab, Vector3(Random.Range(, maxY), Random.Range(minZ, maxZ), Random.Range(minX, maxX)), Quaternion.identity)
    }

    void Start () {
        InvokeRepeating("Spawn", 3, 4);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
