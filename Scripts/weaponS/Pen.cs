using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour
{
    public GameObject bulletSpawn, bullet, IventoryManager;
    public float speed, damage, drop, fireRate, cooldown;
    public float range;
    public bool canShoot;

    public int AmmoCount;

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private AudioSource penAudio;
    private float nextFire;

    private Animator animator;


    public bool bleed, ink, stun;

    void Start()
    {
        penAudio = GetComponent<AudioSource>();
        fpsCam = Camera.main;
        animator = GetComponentInChildren<Animator>();
        IventoryManager =  GameObject.FindGameObjectWithTag("inventory");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire && AmmoCount > 0)
        {
            nextFire = Time.time + fireRate;
            AmmoCount--;
            IventoryManager.GetComponent<InventoryManager>().ammoSet(IventoryManager.GetComponent<InventoryManager>().getActiveWeapon(), AmmoCount);
            /*StartCoroutine(shotEffect());
            Vector3 rayOrigin = (fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)));
            
            

            if((rayOrigin, fpsCam.transform.forward, out hit, range))
            {
                GameObject obj = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                obj.GetComponent<bullet>().rayOrigin = rayOrigin;
                obj.GetComponent<bullet>().GunPos = transform.position;
                //.GetComponent<Rigidbody>().velocity = (rayOrigin - transform.position) * speed;
                obj.GetComponent<bullet>().speed = speed;
                obj.GetComponent<bullet>().damage = damage;
                obj.GetComponent<bullet>().drop = drop;
                obj.GetComponent<bullet>().bleed = bleed;

            }
            else
            {
                GameObject obj = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                obj.GetComponent<bullet>().GetComponent<Rigidbody>().velocity = (rayOrigin - transform.position) * speed;
                //obj.GetComponent<bullet>().speed = speed;
                obj.GetComponent<bullet>().damage = damage;
                obj.GetComponent<bullet>().drop = drop;
                obj.GetComponent<bullet>().bleed = bleed;
            }
               */
            StartCoroutine(shotEffect());
            Shoot();   

        }

        IEnumerator shotEffect()
        {
            penAudio.Play();
            yield return shotDuration;
        }

        void Shoot()
        {
            animator.SetTrigger("Shoot");
            RaycastHit hit;
            if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Vector3 endPoint = hit.point - transform.position;
                GameObject obj = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);   
                obj.GetComponent<bullet>().rayOrigin = endPoint;
                obj.GetComponent<bullet>().GunPos = transform.position;
                obj.GetComponent<Rigidbody>().velocity = endPoint * speed;
                obj.GetComponent<bullet>().speed = speed;
                obj.GetComponent<bullet>().damage = damage;
                //obj.GetComponent<bullet>().drop = drop;
                obj.GetComponent<bullet>().bleed = bleed;
                obj.GetComponent<bullet>().ink = ink;
                obj.GetComponent<bullet>().stun = stun;
            }
            else
            {
                Vector3 endPoint = (fpsCam.transform.position + fpsCam.transform.forward * range) - transform.position;
                GameObject obj = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);   
                obj.GetComponent<bullet>().rayOrigin = endPoint;
                obj.GetComponent<bullet>().GunPos = transform.position;
                obj.GetComponent<Rigidbody>().velocity = endPoint * speed;
                obj.GetComponent<bullet>().speed = speed;
                obj.GetComponent<bullet>().damage = damage;
                //obj.GetComponent<bullet>().drop = drop;
                obj.GetComponent<bullet>().bleed = bleed;
                obj.GetComponent<bullet>().ink = ink;
                obj.GetComponent<bullet>().stun = stun;
            }
            
        }

        
    }

    public void setAmmo(int ammo)
    {
        AmmoCount = ammo;
    }

}