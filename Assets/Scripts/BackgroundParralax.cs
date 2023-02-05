using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParralax : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = Player.GetComponent<Rigidbody2D>().velocity;

        gameObject.transform.position -= velocity * 0.0003f;
    }
}
