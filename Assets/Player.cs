using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float speed = 2.0f;

    Vector3 lastPos;
    float travelDistance = 0.0f;
    float plantInterval = 5.0f;

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
        // Update lastPos



        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        Vector2 inputDir = new Vector2(xInput, yInput);
        inputDir.Normalize();

        GetComponent<Rigidbody2D>().velocity = inputDir * speed;
    }
}
