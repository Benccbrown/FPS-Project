using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDifficulty : MonoBehaviour
{
    public static int diff = 1;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Save");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
