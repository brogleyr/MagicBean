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
    Animator spriteAnimator;

    bool inAir = false;
    public int jumpCount = 0;

    [SerializeField]
    float density = 1f;

    [SerializeField]
    TextMeshPro comboText;

    void Start()
    {
        Vector3 lastPos = transform.position;
        animator = GetComponent<Animator>();
        spriteAnimator = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update() {
        int xInput = 0, yInput = 0;
        if (Input.GetKey("w")) yInput += 1;
        if (Input.GetKey("a")) xInput -= 1;
        if (Input.GetKey("s")) yInput -= 1;
        if (Input.GetKey("d")) xInput += 1;

        spriteAnimator.SetInteger("InputX", xInput);
        spriteAnimator.SetInteger("InputY", yInput);

        inputDir = new Vector2((float) xInput, (float) yInput);
        if (inputDir.magnitude > 1) {
            inputDir.Normalize();
        }

        if (inputDir.magnitude > 0) {
            animator.SetBool("UserInput", true);
        }
        else {
            animator.SetBool("UserInput", false);
        }

        if (!inAir && Input.GetKey("space")) {
            animator.SetBool("JumpInput", true);
        }
        else {
            animator.SetBool("JumpInput", false);
        }

        UpdateCombo();
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = inputDir * speed;
    }

    void Jump() {
        inAir = true;
        jumpCount += 1;
        SetPlantCollisions(false);
        animator.SetBool("OnPlant", false);
    }

    public void Land() {
        inAir = false;
        SetPlantCollisions(true);
        if (IsTouchingPlant()) {
            animator.SetBool("OnPlant", true);
        }
        else {
            PlantSprouts(jumpCount * Mathf.Max(1, Bank.Instance.MagicBeans));
            animator.SetBool("OnPlant", false);
            jumpCount = 0;
        }
    }

    void PlantSprouts(int plantCount) {
        float radius = Mathf.Sqrt(plantCount / density);
        for (int i = 0; i < plantCount; i++) {
            if (i == 0) {
                Instantiate(plantPrefab, transform.position, Quaternion.identity);
                continue;
            }
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

    void UpdateCombo() {
        if (jumpCount < 3) {
            comboText.text = "";
        }
        else if (Bank.Instance.MagicBeans < 2) {
            comboText.text = jumpCount.ToString();
        }
        else {
            comboText.text = jumpCount.ToString() + "x" + Bank.Instance.MagicBeans.ToString();
        }
    }
}
