using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    GameObject player;
    Player script;
    Vector3 movementVec;
    Vector3 home;
    Vector3 position;
    float speed;

    public bool hostile = false;
    public bool fired = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        script = player.GetComponent<Player>();
        home = transform.position;
        position = transform.position;
        movementVec = player.transform.forward;
        movementVec = transform.parent.forward;
        speed = 5f;

        Invoke("Remove", 3);// three seconds after firing, send this bullet back to the player and disable it
    }

    // Update is called once per frame
    void Update()
    {
        // if the bullt is active and fired move it, if not don't
        if(fired)
        {
            position += movementVec * speed * Time.deltaTime;
            transform.position = position;
        }
    }

    // move the bullet back to the start of it's path if it is fired again or is "removed" from the scene
    public void MoveHome(){transform.position = home;}

    public void Remove()
    {
        transform.gameObject.SetActive(false);
        MoveHome();
        fired = false;
    }
}
