using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    GameObject[] ammo;

    float shootCD = 0.5f;
    float shootTimer;
    bool canShoot = true;
    int tracker = 0;
    private GameObject sceneManager;
    private void Start()
    {
        ammo = new GameObject[25];

        for (int i = 0; i < transform.childCount; i++)
        {
            ammo[i] = transform.GetChild(i).gameObject;
        }
        shootTimer = shootCD;

        sceneManager = GameObject.FindGameObjectWithTag("SM");
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

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet" && col.gameObject.GetComponent<Bullet_Script>().hostile == true)
        {
            col.gameObject.GetComponent<Bullet_Script>().Remove();
            sceneManager.GetComponent<ScoreScript>().SaveScore();
            //code for moving to final page that displays high score and your score.
            SceneManager.LoadScene("GameOverScene");
        }
        
    }
}
