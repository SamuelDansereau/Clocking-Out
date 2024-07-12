using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoHolder : MonoBehaviour
{

    public GameObject weaponType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getAmmo()
    {
        if(weaponType.name == "Paper Shredder (Held)")
        {
            return weaponType.GetComponent<PaperShredder>().AmmoCount;
        }
        if(weaponType.name == "pen_hand")
        {
            return weaponType.GetComponent<Pen>().AmmoCount;
        }
        if(weaponType.name == "Stapler_Hand")
        {
            return weaponType.GetComponent<Stapler>().AmmoCount;
        }
        else
        {
            return 0;
        }
    }
}
