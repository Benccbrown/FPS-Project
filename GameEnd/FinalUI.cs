using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalUI : MonoBehaviour
{

    static Text maxDifficulty;
    // Start is called before the first frame update
    void Awake()
    {
        maxDifficulty = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        maxDifficulty.text = ("Maximum Difficulty Acheived: " + FinalDifficulty.diff);
    }
}
