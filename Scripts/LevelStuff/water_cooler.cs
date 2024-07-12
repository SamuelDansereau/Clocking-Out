using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_cooler : MonoBehaviour
{
    GameObject player;
    public float radius, healthSpeed;
    AudioClip start, waterPour;
    AudioSource waterPours;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        waterPours = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist < radius)
        {
            if(!waterPours.isPlaying)
            {
                waterPours.Play();
            }
            player.GetComponent<PlayerMovement>().health += (Time.deltaTime) * healthSpeed;
        }
        else
        {
            waterPours.Stop();
        }
    }
}
