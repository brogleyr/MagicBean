using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float speed = 4.0f;

    Vector3 lastPos;
    [SerializeField]
    float travelDistance = 0.0f;
    float plantInterval = 10.0f;

    [SerializeField]
    GameObject plantPrefab;

    Animator animator;

    bool inAir = false;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 lastPos = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Add travel distance
        travelDistance += Vector3.Distance(lastPos, transform.position);
        lastPos = transform.position;

        if (!inAir && travelDistance > plantInterval) {
            Jump();
        }

        float xInput = 0, yInput = 0;
        if (Input.GetKey("w")) yInput += 1;
        if (Input.GetKey("a")) xInput -= 1;
        if (Input.GetKey("s")) yInput -= 1;
        if (Input.GetKey("d")) xInput += 1;
        Vector2 inputDir = new Vector2(xInput, yInput);
        if (inputDir.magnitude > 1) {
            inputDir.Normalize();
        } 
        GetComponent<Rigidbody2D>().velocity = inputDir * speed;

        if (inputDir.magnitude > 0) {
            animator.SetBool("UserInput", true);
        }
    }

    void Jump() {
        Debug.Log("Called Jump");
        travelDistance = 0.0f;
        inAir = true;
        animator.SetTrigger("Jump");
    }

    public void Land() {
        Debug.Log("Called Land");
        inAir = false;
    }

    public void PlantSprout() {
        Debug.Log("Called PlantSprout");
        Instantiate(plantPrefab, transform.position, Quaternion.identity);
    }
}
