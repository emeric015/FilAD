using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    BoxCollider2D box;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        
        Debug.Log(box);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject);

        CurrentArea currentObject = other.gameObject.GetComponent<CurrentArea>();

        //Debug.Log(currentObject);

        if(currentObject != null) {
            Debug.Log("Entered on " + box.name);

            currentObject.setArea(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
