using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bullet; 


    // Update is called once per frame
    void Update()
    {
        // check for touch input
        if(Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }

    private void FireBullet()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
