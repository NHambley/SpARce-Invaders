using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy1 : MonoBehaviour
{
    private GameObject player;
    private GameObject bullet;
    public bool alive;
    public bool leader;
    private Vector3 position;
    private Vector3 destination;
    private float shootTimer;
    private float shootCD;

    private GameObject sceneManager;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bullet = transform.Find("Bullet").gameObject;
        bullet.GetComponent<Bullet_Script>().hostile = true;
        position = transform.position;
        destination = player.transform.position;
        alive = true;
        transform.forward = (destination - position).normalized;

        sceneManager = GameObject.FindGameObjectWithTag("SM");
        if (leader)
        {
            formMinions();
        }

        shootTimer = 5 + (Random.value * 15);
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            position = transform.position;
            destination = player.transform.position;
            transform.forward = (destination - position).normalized;
            Move();

            if (shootTimer <= 0)
            {
                Shoot();
            }

            shootTimer -= Time.deltaTime;
        }
        else
        {
            if (leader == true)
            {
                Vector2 randomXZ = Random.insideUnitCircle * 15;
                Vector3 newPosition = new Vector3(randomXZ.x, 5, randomXZ.y);
                GameObject newEnemy = Instantiate(gameObject, newPosition, Quaternion.identity);
                newEnemy.GetComponent<Enemy1>().leader = true;
            }
            Destroy(gameObject);
        }
    }

    void Move()
    {
        Vector3 direction = (destination - position).normalized;
        transform.forward = direction;
        transform.position += (direction / 3) * Time.deltaTime;

        Vector3 right = transform.right;
        right.y = 0;
        transform.position += right * Time.deltaTime;
    }

    void Shoot()
    {
        shootTimer = shootCD; ;

        // check if the bullet is already active, if it is move it back to home position
        if (bullet.activeInHierarchy == true)
        {
            bullet.GetComponent<Bullet_Script>().MoveHome();
        }
        else
        {
            bullet.SetActive(true);
            bullet.GetComponent<Bullet_Script>().fired = true;
        }
    }

   void formMinions()
    {
        createMinion(new Vector3( 1, -1, 0));
        createMinion(new Vector3( 1,  0, 0));
        createMinion(new Vector3( 1,  1, 0));
        createMinion(new Vector3(-1, -1, 0));
        createMinion(new Vector3(-1,  0, 0));
        createMinion(new Vector3(-1,  1, 0));
        createMinion(new Vector3( 0, -1, 0));
        createMinion(new Vector3( 0,  1, 0));
    }

    void createMinion(Vector3 minionPosition)
    {
        Vector3 newPosition = position;
        newPosition += (transform.right * minionPosition.x) + (transform.up * minionPosition.y);
        GameObject newEnemy = Instantiate(gameObject, newPosition, Quaternion.identity);
        newEnemy.GetComponent<Enemy1>().leader = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet" && col.gameObject.GetComponent<Bullet_Script>().hostile == false)
        {
            col.gameObject.GetComponent<Bullet_Script>().Remove();
            alive = false;
            sceneManager.GetComponent<ScoreTimerScript>().AddScore(1);
        }
        else if (col.gameObject.name == "Player")
        {
            sceneManager.GetComponent<ScoreTimerScript>().SaveScore();
            alive = false;
            //code for moving to final page that displays high score and your score.
            SceneManager.LoadScene("GameOverScene");
        }
    }


}
