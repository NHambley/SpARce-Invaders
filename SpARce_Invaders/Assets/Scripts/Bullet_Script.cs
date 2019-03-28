using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    GameObject player;
    Vector3 movementVec;
    Vector3 position;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        position = transform.position;
        movementVec = player.transform.forward;
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        position += movementVec * speed * Time.deltaTime;
    }
}
