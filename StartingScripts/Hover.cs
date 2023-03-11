using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //the item starts with the colour blue
        GetComponent<Renderer>().material.color = Color.blue;
    }

    private void OnMouseEnter()
    {
        //if a mouse hovers over it, it is red
        GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnMouseExit()
    {
        //when the mouse stops hovering over it, it is blue again
        GetComponent<Renderer>().material.color = Color.blue;
    }
}
