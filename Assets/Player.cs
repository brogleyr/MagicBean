using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    float speed = 4.0f;

    Vector2 inputDir;

    [SerializeField]
    GameObject plantPrefab;

    Animator animator;

    bool inAir = false;
    bool rejump = false;
    public int jumpCount = 0;

    [SerializeField]
    float density = 1f;

    void Start()
    {
        Vector3 lastPos = transform.position;
        animator = GetComponent<Animator>();
    }

    void Update() {
        float xInput = 0, yInput = 0;
        if (Input.GetKey("w")) yInput += 1;
        if (Input.GetKey("a")) xInput -= 1;
        if (Input.GetKey("s")) yInput -= 1;
        if (Input.GetKey("d")) xInput += 1;
        inputDir = new Vector2(xInput, yInput);
        if (inputDir.magnitude > 1) {
            inputDir.Normalize();
        }

        if (inputDir.magnitude > 0) {
            animator.SetBool("UserInput", true);
        }

        if (!inAir && Input.GetKey("space")) {
            Jump();
        }
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = inputDir * speed;
    }

    void Jump() {
        inAir = true;
        jumpCount += 1;
        SetPlantCollisions(false);
        animator.SetTrigger("Jump");
    }

    public void Land() {
        inAir = false;
        SetPlantCollisions(true);
        if (IsTouchingPlant()) {
            rejump = true;
        }
        else {
            PlantSprouts(jumpCount * Mathf.Max(1, Bank.Instance.MagicBeans));
            rejump = false;
            jumpCount = 0;
        }
    }

    public void Rejump() {
        if (rejump) {
            Jump();
        }
    }

    void PlantSprouts(int plantCount) {
        float radius = Mathf.Sqrt(plantCount / density);
        for (int i = 0; i < plantCount; i++) {
            float dist = Mathf.Sqrt(Random.Range(0f, radius * radius));
            float angle = Random.Range(0f, 360f);
            Vector3 displacement = Quaternion.AngleAxis(angle, Vector3.forward) * new Vector3(dist, 0f, 0f);
            Instantiate(plantPrefab, transform.position + displacement, Quaternion.identity);
        }
    }

    void SetPlantCollisions(bool plantCollisions) {
        if (!plantCollisions) {
            GetComponent<Collider2D>().excludeLayers = LayerMask.GetMask("Plant");
        }
        else {
            GetComponent<Collider2D>().excludeLayers = LayerMask.GetMask();
        }
    }

    bool IsTouchingPlant() {
        List<Collider2D> contacts = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = LayerMask.GetMask("Plant");
        GetComponent<Collider2D>().OverlapCollider(filter, contacts);
        return contacts.Count > 0;
    }
}
