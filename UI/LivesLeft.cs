using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesLeft : MonoBehaviour
{
    public static int lives = 3;
    Text livesLeft;

    // Start is called before the first frame update
    void Start()
    {
        livesLeft = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //the lives equals the users lives and is put into text for the UI
        livesLeft.text = ("Lives:" + lives);
    }
}
