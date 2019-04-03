using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Control : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check for a touch input
        if(Input.touchCount > 0)
        {
            RaycastHit hit;
            
            if (Physics.Raycast(Input.GetTouch(0).position, transform.forward, out hit))
            {
                if(hit.collider.gameObject.tag == "MenuSwitch")
                {
                    hit.collider.gameObject.GetComponent<LevelChange>().SceneChange();
                }
            }
        }
    }
}
