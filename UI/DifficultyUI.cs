using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyUI : MonoBehaviour
{
    public static int difficulty = 1;
    Text Difficulty;

    // Start is called before the first frame update
    void Start()
    {
        Difficulty = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //the difficulty equals the users difficulty and is put into text for the UI
        Difficulty.text = ("Difficulty:" + difficulty);
    }
}
