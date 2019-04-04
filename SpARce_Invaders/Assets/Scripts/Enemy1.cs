using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public GameObject player;
    private Vector3 position;
    private Vector3 destination;
    public bool alive;

    private int a_nSubdivisions;
    private float a_fRadius;
    private Vector3 Center;
    private float degree;
    private Vector3 previousPoint;
    private Vector3 CalcPoint;
    private float radx;
    private float rady;
    private float angle;


    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        destination = player.transform.position;
        alive = true;

        // Circle
        a_nSubdivisions = 20;
        a_fRadius = (destination - position).normalized.magnitude;
        Center = new Vector3(0.0f, 0.0f, 0.0f);
        degree = (float)360.0 / (float)a_nSubdivisions;
        previousPoint = new Vector3(a_fRadius, 0.0f, 0.0f);
        CalcPoint = new Vector3(0.0f, 0.0f, 0.0f);
        radx = 0;
        rady = 0;
        angle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            position = transform.position;
            destination = player.transform.position;

            //Circle
            float a = angle;
            //Point on a circle
            radx = a_fRadius * Mathf.Cos((a + degree) * (float)(Mathf.PI / 180));
            rady = a_fRadius * Mathf.Sin((a + degree) * (float)(Mathf.PI / 180));
            CalcPoint.x = radx;
            CalcPoint.y = rady;

            //Next segment left side connects to previous segment's right side
            previousPoint = CalcPoint;
            angle += degree;
            angle = angle % 360.0f;

            
            
            //Move();
        }
        else
        {
            //Vector2 randomXZ = Random.insideUnitCircle * 10;
            //Vector3 newPosition = new Vector3(randomXZ.x, 5, randomXZ.y);
            //GameObject newEnemy = Instantiate(gameObject, newPosition, Quaternion.identity);
            //Destroy(gameObject);
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
        if (col.gameObject.tag == "Bullet" || col.gameObject.name == "Player")
        {
            Destroy(col.gameObject);
            alive = false;
        }
    }


}
