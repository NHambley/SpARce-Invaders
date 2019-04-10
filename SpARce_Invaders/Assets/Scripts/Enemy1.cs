using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy1 : MonoBehaviour
{
    public GameObject player;
    private Vector3 position;
    private Vector3 destination;
    public bool alive;
    private GameObject sceneManager;
    public bool leader;


    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        destination = player.transform.position;
        alive = true;
        transform.forward = (destination - position).normalized;

        sceneManager = GameObject.FindGameObjectWithTag("SM");

        createMinions();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            position = transform.position;
            destination = player.transform.position;

            Move();
        }
        else
        {
            if (leader == true)
            {
                Vector2 randomXZ = Random.insideUnitCircle * 10;
                Vector3 newPosition = new Vector3(randomXZ.x, 5, randomXZ.y);
                GameObject newEnemy = Instantiate(gameObject, newPosition, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    void Move()
    {
        Vector3 direction = (destination - position).normalized;
        transform.position += direction/3 * Time.deltaTime;

        transform.forward = direction;
        Vector3 right = transform.right;
        right.y = 0;
        transform.position += right * Time.deltaTime;
    }

   void createMinions()
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                Vector3 newPosition = position;
                newPosition += (transform.up * (5.0f * j));
                newPosition += (transform.right * (5.0f * j));
                
                GameObject newEnemy = Instantiate(gameObject, newPosition, Quaternion.identity);
                //newEnemy.leader = false;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            col.gameObject.GetComponent<Bullet_Script>().Remove();
            alive = false;
            sceneManager.GetComponent<ScoreScript>().AddScore(1);
        }
        else if (col.gameObject.name == "Player")
        {
            sceneManager.GetComponent<ScoreScript>().SaveScore();
            alive = false;
            //code for moving to final page that displays high score and your score.
            SceneManager.LoadScene("GameOverScene");
        }
    }


}
