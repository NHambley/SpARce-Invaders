using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    int hits = 2;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "EBullet")
        {
            hits -= 1;
            if(hits == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
