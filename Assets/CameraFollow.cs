using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    float xMargin = 5, yMargin = 3;

    [SerializeField]
    Transform player;

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = player.position - transform.position;
        if (difference.x > xMargin) {
            transform.position = new Vector3(player.position.x - xMargin, transform.position.y, transform.position.z);
        }
        else if (difference.x < -xMargin) {
            transform.position = new Vector3(player.position.x + xMargin, transform.position.y, transform.position.z);
        }

        if (difference.y > yMargin) {
            transform.position = new Vector3(transform.position.x, player.position.y - yMargin, transform.position.z);
        }
        else if (difference.y < -yMargin) {
            transform.position = new Vector3(transform.position.x, player.position.y + yMargin, transform.position.z);
        }
    }
}
