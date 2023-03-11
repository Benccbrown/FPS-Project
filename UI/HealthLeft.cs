using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthLeft : MonoBehaviour
{
    public static int health = 100;
    Text Health;

    // Start is called before the first frame update
    void Start()
    {
        Health = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //the health equals the users health and is put into text for the UI
        Health.text = ("Health:" + health);
    }
}
