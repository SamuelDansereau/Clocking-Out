using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSpawn : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    int randomNumber;

    private void Start()
    {
        randomNumber = Random.Range(0, weapons.Count);
        Instantiate(weapons[randomNumber], transform.position, transform.rotation);
    }
}
