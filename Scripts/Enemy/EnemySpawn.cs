using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public float speed;
    float time, fullTime;
    int randomNumber;
    public int maxPoints = 100;
    public int totalpoints;
    private float seconds = 5f;

    private void Start()
    {
        time = speed;
        fullTime = 0;
    }

    private void Update()
    {
        time -= Time.deltaTime;
        fullTime += Time.deltaTime;
        randomNumber = Random.Range(0, enemies.Count);
        if (time <= 0 && totalpoints < maxPoints)
        {    
            GameObject newEnemy = Instantiate(enemies[randomNumber], transform.position, transform.rotation);
            if(newEnemy.GetComponent<JumperEnemy>() != null)
            {
                StartCoroutine(spawnSecond());
            }
            newEnemy.GetComponent<BasicEnemy>().setSpawn(gameObject);
            time = speed - (.001f * fullTime);
            totalpoints += newEnemy.GetComponent<BasicEnemy>().getPointValue();
        }
    }

    public void RemovePoints(int points)
    {
        totalpoints -= points;
    }

    private IEnumerator spawnSecond()
    {
        GameObject newEnemy2 = Instantiate(enemies[randomNumber], transform.position, transform.rotation);
        newEnemy2.GetComponent<BasicEnemy>().setSpawn(gameObject);
        totalpoints += newEnemy2.GetComponent<BasicEnemy>().getPointValue();
        yield return new WaitForSeconds(seconds);
    }
}
