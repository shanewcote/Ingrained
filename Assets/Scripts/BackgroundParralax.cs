using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParralax : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = player.GetCameraVelocity() * 0.1f;
        //Vector3 playerVelocity = new Vector3(player.GetComponent<Rigidbody2D>().velocity.x, player.GetComponent<Rigidbody2D>().velocity.y, 0) * 0.0002f;

        gameObject.transform.position -= velocity;
    }
}
