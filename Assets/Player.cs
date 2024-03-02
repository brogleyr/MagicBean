using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float speed = 4.0f;

    Vector3 lastPos;
    float travelDistance = 0.0f;
    float plantInterval = 10.0f;

    [SerializeField]
    GameObject plantPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 lastPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Add travel distance
        travelDistance += Vector3.Distance(lastPos, transform.position);
        lastPos = transform.position;

        if (travelDistance > plantInterval) {
            Instantiate(plantPrefab, transform.position, Quaternion.identity);
            travelDistance = 0.0f;
        }



        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dirToMouse = mouseWorldPos - transform.position;
        Vector2 inputDir = new Vector2(dirToMouse.x, dirToMouse.y);
        if (inputDir.magnitude > 1) {
            inputDir.Normalize();
        } 

        GetComponent<Rigidbody2D>().velocity = inputDir * speed;
    }
}
