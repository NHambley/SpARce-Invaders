using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameObject[] ammo;

    float shootCD = 0.5f;
    float shootTimer;
    bool canShoot = true;
    int tracker = 0;
    private void Start()
    {
        ammo = new GameObject[25];

        for (int i = 0; i < transform.childCount; i++)
        {
            ammo[i] = transform.GetChild(i).gameObject;
        }
        shootTimer = shootCD;
    }
    // Update is called once per frame
    void Update()
    {
        // check for touch input
        if(Input.touchCount > 0 && canShoot)
        {
            FireBullet();
        }
        else if(!canShoot)
        {
            shootTimer -= Time.deltaTime;
            if(shootTimer <= 0)
            {
                canShoot = true;
            }
        }
    }

    private void FireBullet()
    {
        canShoot = false;

        // check if the bullet is already active, if it is move it back to home position
        if (ammo[tracker].activeInHierarchy == true)
        {
            ammo[tracker].GetComponent<Bullet_Script>().MoveHome();
            shootTimer = shootCD;
        }
        else
        {
            ammo[tracker].SetActive(true);
            ammo[tracker].GetComponent<Bullet_Script>().fired = true;
            shootTimer = shootCD;
        }

        // avoid index out of bounds exception
        tracker++;
        if (tracker > ammo.Length - 1)
        {
            tracker = 0;
        }
    }
}
