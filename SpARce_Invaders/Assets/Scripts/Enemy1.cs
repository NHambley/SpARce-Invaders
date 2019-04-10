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
        if (leader)
        {
            transform.forward = (destination - position).normalized;
            formMinions();
        }
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
        }
        else
        {
            if (leader == true)
            {
                Vector2 randomXZ = Random.insideUnitCircle * 10;
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
        transform.position += direction/3 * Time.deltaTime;

        transform.forward = -direction;
        Vector3 right = transform.right;
        right.y = 0;
        right.z = 0;
        transform.position += right * Time.deltaTime;
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
