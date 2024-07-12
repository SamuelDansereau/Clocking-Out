using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Printer : MonoBehaviour
{
    public bool active = true;
    GameObject player;
    public GameObject slide;
    public float radius = 5;
    public GameObject enemy;
    //public float spawnRate;
    bool runCouroutine = true;

    public float progress = 0.0f;
    public float captureSpeed = 0.04f;
    Slider point;
    AudioSource printerSound;

    public float modAdd;
    public float scoreTimerAdd;
    private bool sendMod;

    private void Start()
    {
        printerSound = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (!active)
            active = true;
        slide.SetActive(false);
        point = slide.GetComponent<Slider>();
        //StartCoroutine(SpawnEnemies());

        modAdd = 0.5f;
        scoreTimerAdd = 10.0f;
        sendMod = false;
    }

    void Update()
    {
        if(active)
        {
            CheckControl();
        }
        point.value = progress;
    }

    void CheckControl()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if(dist < radius)
        {
            progress += (Time.deltaTime) * captureSpeed;
            slide.SetActive(true);
            if(!printerSound.isPlaying)
                printerSound.Play();

            //SpawnEnemies();
        }
        else
        {
            printerSound.Stop();
            slide.SetActive(false);
        }

        if(progress >= 1)
        {
            slide.SetActive(false);
            runCouroutine = false;
            GetComponent<Objective>().done = true;
            
            if (sendMod == false)
            {
                ScoreManager.instance.AddModifier(modAdd, scoreTimerAdd);
                sendMod = true;
            }
        }
    }
}
