using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoLeft : MonoBehaviour
{
    public static int ammo = 12;
    Text Ammo;

    // Start is called before the first frame update
    void Start()
    {
        Ammo = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //the ammo equals the users ammo and is put into text for the UI
        Ammo.text = ("Ammo:" + ammo);
    }
}
