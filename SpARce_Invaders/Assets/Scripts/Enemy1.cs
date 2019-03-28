using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public GameObject player;
    private Vector3 position;
    private Vector3 destination;
    public bool alive;
    
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        alive = true;
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
            Vector2 randomXZ = Random.insideUnitCircle * 10;
            Vector3 newPosition = new Vector3(randomXZ.x, 5, randomXZ.y);
            GameObject newEnemy = Instantiate(gameObject, newPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Move()
    {
        Vector3 direction = (destination - position).normalized;
        
        Vector3 scale = new Vector3(0.02f, 0.02f, 0.02f);
        direction.Scale(scale);
        position += direction;
        transform.position = position;

        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Bullet" || col.gameObject.name == "Player")
        {
            Destroy(col.gameObject);
            alive = false;
        }
    }


}
