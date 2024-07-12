using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mechaNest : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public float speed;
    float time, fullTime;
    int randomNumber;
    public int maxPoints = 100;
    public int totalpoints;
    public int spawnAmount;
    public GameObject jackets;
    public GameObject player;
    public float radius;

    private void Start()
    {
        time = speed;
        fullTime = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        time -= Time.deltaTime;
        fullTime += Time.deltaTime;
        randomNumber = Random.Range(0, enemies.Count);
        if (time <= 0 && totalpoints < maxPoints)
        {    
            GameObject newEnemy =  Instantiate(jackets, transform.position, transform.rotation);
            newEnemy.GetComponent<BasicEnemy>().setSpawn(gameObject);
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist < radius)
            {
                GameObject newEnemy2 = Instantiate(jackets, transform.position, transform.rotation);
                newEnemy2.GetComponent<BasicEnemy>().setSpawn(gameObject);
                totalpoints += newEnemy.GetComponent<BasicEnemy>().getPointValue();
                speed = speed * .5f;
            }
            else
            {
                speed = speed * 2;
            }
            time = speed - (.001f * fullTime);
            totalpoints += newEnemy.GetComponent<BasicEnemy>().getPointValue();
        }
    }

    public void RemovePoints(int points)
    {
        totalpoints -= points;
    }
    
}
