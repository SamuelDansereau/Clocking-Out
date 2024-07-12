using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public hotbar[] HotBarItems;

    public Camera myCamera;
    public Transform ItemContent, ItemSpawn;
    public GameObject penHand, staplerHand, stapler, pen, player;
    public GameObject macheteHand, machete, paperShred, paperShredHand;
    public GameObject RBB, RBBHand, Reports, ReportsHand;

    public GameObject emptyHanded;
    public Sprite emptyHanded_sprite;

    public int selected, mouse;
    public bool added = false;
    bool justStarted = true;

    public TextMeshProUGUI weaponName, ammoText;

    int ammo1, ammo2, ammo3, ammo4, ammo5, ammo6;

    AudioSource InventorySounds;

    private void Start()
    {
        InventorySounds = GetComponent<AudioSource>();
        Instance = this;
        HotBarItems = FindObjectsOfType<hotbar>().OrderBy( go => go.name ).ToArray();

        List<hotbar> temp = new List<hotbar>();
        for(int i = 0; i < HotBarItems.Length; i++)
        {
            temp.Add(HotBarItems[i]);
        }

        for (int i = 0; i < HotBarItems.Length; i++)
        {
            if (i != selected)
            {
                HotBarItems[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
                HotBarItems[i].GetComponent<Image>().sprite = emptyHanded_sprite;

            }
        }

        GameObject held;
        held = Instantiate(emptyHanded, ItemSpawn.position, ItemSpawn.rotation);
        weaponName.text = " ";

    }

    private void Update()
    {
        ammoText.text = getAmmo().ToString();
        var v = Input.GetAxis("Mouse ScrollWheel");
        if (v < 0)
        {
            mouse++;
            if (mouse < 0)
                mouse = 5;
        }
        else if (v > 0)
        {
            mouse--;
            if (mouse > 5)
                mouse = 0;
        }
        
        if (Input.GetMouseButtonDown(1))
            ItemThrow(selected);

        if (justStarted || Input.GetKeyDown(KeyCode.Alpha1) || (Input.GetAxis("Mouse ScrollWheel") != 0 && mouse == 0))
        {
            justStarted = false;
            selected = 0;
            Destroy(GameObject.FindGameObjectWithTag("ItemInUse"));
            var itemName = HotBarItems[selected].transform.Find("Text").GetComponent<TextMeshProUGUI>();
            HotBarItems[selected].GetComponent<Image>().color = new Color(255, 255, 255, 150);
            weaponName.text = "";
            for (int i = 0; i < HotBarItems.Length; i++)
            {
                if (i != selected)
                    HotBarItems[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
            }

            if (itemName.text == "Pen")
            {
                GameObject held;
                held = Instantiate(penHand, ItemSpawn.position, ItemSpawn.rotation);
                held.GetComponent<Pen>().setAmmo(ammo1);
                weaponName.text = "Pen";
                
            }
            else if (itemName.text == "Stapler")
            {
                GameObject held;
                held = Instantiate(staplerHand, ItemSpawn.position, ItemSpawn.rotation);
                held.GetComponent<Stapler>().setAmmo(ammo1);
                weaponName.text = "Stapler";

            }

            else if (itemName.text == "Paper Cutter")
            {
                GameObject held;
                held = Instantiate(macheteHand, ItemSpawn.position, macheteHand.transform.rotation);
                weaponName.text = "Paper Cutter";
            }
            else if (itemName.text == "Paper Shredder")
            {
                GameObject held;
                held = Instantiate(paperShredHand, ItemSpawn.position, ItemSpawn.rotation);
                held.GetComponent<PaperShredder>().setAmmo(ammo1);
                weaponName.text = "Paper Shredder";

            }
            else if (itemName.text == "RBB")
            {
                Instantiate(RBBHand, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = "Rubber Band Ball";
            }
            else if (itemName.text == "Reports")
            {
                GameObject held;
                held = Instantiate(ReportsHand, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = "Reports";
            }
            else{
                GameObject held;
                held = Instantiate(emptyHanded, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = " ";
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || (Input.GetAxis("Mouse ScrollWheel") != 0 && mouse == 1))
        {
            selected = 1;
            Destroy(GameObject.FindGameObjectWithTag("ItemInUse"));
            var itemName = HotBarItems[selected].transform.Find("Text").GetComponent<TextMeshProUGUI>();
            HotBarItems[selected].GetComponent<Image>().color = new Color(255, 255, 255, 150);
            weaponName.text = "";
            for (int i = 0; i < HotBarItems.Length; i++)
            {
                if (i != selected)
                    HotBarItems[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
            }

          if (itemName.text == "Pen")
            {
                GameObject held;
                held = Instantiate(penHand, ItemSpawn.position, ItemSpawn.rotation);
                held.GetComponent<Pen>().setAmmo(ammo2);
                weaponName.text = "Pen";
                
            }
            else if (itemName.text == "Stapler")
            {
                GameObject held;
                held = Instantiate(staplerHand, ItemSpawn.position, ItemSpawn.rotation);
                held.GetComponent<Stapler>().setAmmo(ammo2);
                weaponName.text = "Stapler";

            }

            else if (itemName.text == "Paper Cutter")
            {
                GameObject held;
                held = Instantiate(macheteHand, ItemSpawn.position, macheteHand.transform.rotation);
                weaponName.text = "Paper Cutter";
            }
            else if (itemName.text == "Paper Shredder")
            {
                GameObject held;
                held = Instantiate(paperShredHand, ItemSpawn.position, ItemSpawn.rotation);
                held.GetComponent<PaperShredder>().setAmmo(ammo2);
                weaponName.text = "Paper Shredder";

            }
            else if (itemName.text == "RBB")
            {
                GameObject held;
                held = Instantiate(RBBHand, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = "Rubber Band Ball";
            }
            else if (itemName.text == "Reports")
            {
                GameObject held;
                held = Instantiate(ReportsHand, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = "Reports";
            }
            else{
                GameObject held;
                held = Instantiate(emptyHanded, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = " ";
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || (Input.GetAxis("Mouse ScrollWheel") != 0 && mouse == 2))
        {
            selected = 2;
            Destroy(GameObject.FindGameObjectWithTag("ItemInUse"));
            var itemName = HotBarItems[selected].transform.Find("Text").GetComponent<TextMeshProUGUI>();
            HotBarItems[selected].GetComponent<Image>().color = new Color(255, 255, 255, 150);
            weaponName.text = "";
            for (int i = 0; i < HotBarItems.Length; i++)
            {
                if (i != selected)
                    HotBarItems[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
            }

          if (itemName.text == "Pen")
            {
                GameObject held;
                held = Instantiate(penHand, ItemSpawn.position, ItemSpawn.rotation);
                held.GetComponent<Pen>().setAmmo(ammo3);
                weaponName.text = "Pen";
                
            }
            else if (itemName.text == "Stapler")
            {
                GameObject held;
                held = Instantiate(staplerHand, ItemSpawn.position, ItemSpawn.rotation);
                held.GetComponent<Stapler>().setAmmo(ammo3);
                weaponName.text = "Stapler";

            }

            else if (itemName.text == "Paper Cutter")
            {
                GameObject held;
                held = Instantiate(macheteHand, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = "Paper Cutter";
            }
            else if (itemName.text == "Paper Shredder")
            {
                GameObject held;
                held = Instantiate(paperShredHand, ItemSpawn.position, ItemSpawn.rotation);
                held.GetComponent<PaperShredder>().setAmmo(ammo3);
                weaponName.text = "Paper Shredder";

            }
            else if (itemName.text == "RBB")
            {
                GameObject held;
                held = Instantiate(RBBHand, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = "Rubber Band Ball";
            }
            else if (itemName.text == "Reports")
            {
                GameObject held;
                held = Instantiate(ReportsHand, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = "Reports";
            }
            else{
                GameObject held;
                held = Instantiate(emptyHanded, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = " ";
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) || (Input.GetAxis("Mouse ScrollWheel") != 0 && mouse == 3))
        {
            selected = 3;
            Destroy(GameObject.FindGameObjectWithTag("ItemInUse"));
            var itemName = HotBarItems[selected].transform.Find("Text").GetComponent<TextMeshProUGUI>();
            HotBarItems[selected].GetComponent<Image>().color = new Color(255, 255, 255, 150);
            weaponName.text = "";
            for (int i = 0; i < HotBarItems.Length; i++)
            {
                if (i != selected)
                    HotBarItems[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
            }

           if (itemName.text == "Pen")
            {
                GameObject held;
                held = Instantiate(penHand, ItemSpawn.position, ItemSpawn.rotation);
                held.GetComponent<Pen>().setAmmo(ammo4);
                weaponName.text = "Pen";
                
            }
            else if (itemName.text == "Stapler")
            {
                GameObject held;
                held = Instantiate(staplerHand, ItemSpawn.position, ItemSpawn.rotation);
                held.GetComponent<Stapler>().setAmmo(ammo4);
                weaponName.text = "Stapler";

            }

            else if (itemName.text == "Paper Cutter")
            {
                GameObject held;
                held = Instantiate(macheteHand, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = "Paper Cutter";
            }
            else if (itemName.text == "Paper Shredder")
            {
                GameObject held;
                held = Instantiate(paperShredHand, ItemSpawn.position, ItemSpawn.rotation);
                held.GetComponent<PaperShredder>().setAmmo(ammo4);
                weaponName.text = "Paper Shredder";

            }
            else if (itemName.text == "RBB")
            {
                GameObject held;
                held = Instantiate(RBBHand, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = "Rubber Band Ball";
            }
            else if (itemName.text == "Reports")
            {
                GameObject held;
                held = Instantiate(ReportsHand, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = "Reports";
            }
            else{
                GameObject held;
                held = Instantiate(emptyHanded, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = " ";
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) || (Input.GetAxis("Mouse ScrollWheel") != 0 && mouse == 4))
        {
            selected = 4;
            Destroy(GameObject.FindGameObjectWithTag("ItemInUse"));
            var itemName = HotBarItems[selected].transform.Find("Text").GetComponent<TextMeshProUGUI>();
            HotBarItems[selected].GetComponent<Image>().color = new Color(255, 255, 255, 150);
            weaponName.text = "";
            for (int i = 0; i < HotBarItems.Length; i++)
            {
                if (i != selected)
                    HotBarItems[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
            }

          if (itemName.text == "Pen")
            {
                GameObject held;
                held = Instantiate(penHand, ItemSpawn.position, ItemSpawn.rotation);
                held.GetComponent<Pen>().setAmmo(ammo5);
                weaponName.text = "Pen";
                
            }
            else if (itemName.text == "Stapler")
            {
                GameObject held;
                held = Instantiate(staplerHand, ItemSpawn.position, ItemSpawn.rotation);
                held.GetComponent<Stapler>().setAmmo(ammo5);
                weaponName.text = "Stapler";

            }

            else if (itemName.text == "Paper Cutter")
            {
                GameObject held;
                held = Instantiate(macheteHand, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = "Paper Cutter";
            }
            else if (itemName.text == "Paper Shredder")
            {
                GameObject held;
                held = Instantiate(paperShredHand, ItemSpawn.position, ItemSpawn.rotation);
                held.GetComponent<PaperShredder>().setAmmo(ammo5);
                weaponName.text = "Paper Shredder";

            }
            else if (itemName.text == "RBB")
            {
                GameObject held;
                held = Instantiate(RBBHand, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = "Rubber Band Ball";
            }
            else if (itemName.text == "Reports")
            {
                GameObject held;
                held = Instantiate(ReportsHand, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = "Reports";
            }
            
            else{
                GameObject held;
                held = Instantiate(emptyHanded, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = " ";
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) || (Input.GetAxis("Mouse ScrollWheel") != 0 && mouse == 5))
        {
            selected = 5;
            Destroy(GameObject.FindGameObjectWithTag("ItemInUse"));
            var itemName = HotBarItems[selected].transform.Find("Text").GetComponent<TextMeshProUGUI>();
            HotBarItems[selected].GetComponent<Image>().color = new Color(255, 255, 255, 150);
            weaponName.text = "";
            for (int i = 0; i < HotBarItems.Length; i++)
            {
                if (i != selected)
                    HotBarItems[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
            }

          if (itemName.text == "Pen")
            {
                GameObject held;
                held = Instantiate(penHand, ItemSpawn.position, ItemSpawn.rotation);
                held.GetComponent<Pen>().setAmmo(ammo6);
                weaponName.text = "Pen";
                
            }
            else if (itemName.text == "Stapler")
            {
                GameObject held;
                held = Instantiate(staplerHand, ItemSpawn.position, ItemSpawn.rotation);
                held.GetComponent<Stapler>().setAmmo(ammo6);
                weaponName.text = "Stapler";

            }

            else if (itemName.text == "Paper Cutter")
            {
                GameObject held;
                held = Instantiate(macheteHand, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = "Paper Cutter";
            }
            else if (itemName.text == "Paper Shredder")
            {
                GameObject held;
                held = Instantiate(paperShredHand, ItemSpawn.position, ItemSpawn.rotation);
                held.GetComponent<PaperShredder>().setAmmo(ammo6);
                weaponName.text = "Paper Shredder";

            }
            else if (itemName.text == "RBB")
            {
                GameObject held;
                held = Instantiate(RBBHand, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = "Rubber Band Ball";
            }
            else if (itemName.text == "Reports")
            {
                GameObject held;
                held = Instantiate(ReportsHand, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = "Reports";
            }
            
            else{
                GameObject held;
                held = Instantiate(emptyHanded, ItemSpawn.position, ItemSpawn.rotation);
                weaponName.text = " ";
            }
        }
    }

    public void Add(Item item, GameObject obj, int ammo, AudioClip sound)
    {
        InventorySounds.PlayOneShot(sound, 1);
        int i = 0;
        while (i < HotBarItems.Length && !added)
        {
            var itemName = HotBarItems[i].transform.Find("Text").GetComponent<TextMeshProUGUI>();
            var itemIcon = HotBarItems[i].transform.Find("Image").GetComponent<Image>();
            if (itemName.text == "" && !added)
            {
                itemName.text = item.itemName;
                itemIcon.sprite = item.icon;
                added = true;
                ammoSet(i, ammo);
                Destroy(obj);
            }
            else
                i++;
        } 
    }

    public void ThrowCurrent()
    {
        ItemThrow(selected);
    }

    public void ItemThrow(int selected)
    {
        weaponName.text = "";
        if (HotBarItems[selected].transform.Find("Text").GetComponent<TextMeshProUGUI>().text == "Pen")
        {
            GameObject item = Instantiate(pen, myCamera.transform.position + myCamera.transform.forward, myCamera.transform.rotation);
            item.GetComponent<Rigidbody>().AddForce(myCamera.transform.forward * 10 + transform.up * 4, ForceMode.Impulse);
            item.gameObject.tag = "throwable";
            ammoSet(selected, 0);
        }
        else if (HotBarItems[selected].transform.Find("Text").GetComponent<TextMeshProUGUI>().text == "Stapler")
        {
            GameObject item = Instantiate(stapler, myCamera.transform.position + myCamera.transform.forward, myCamera.transform.rotation);
            item.GetComponent<Rigidbody>().AddForce(myCamera.transform.forward * 10 + transform.up * 4, ForceMode.Impulse);
            item.gameObject.tag = "throwable";
            ammoSet(selected, 0);
        }
        else if (HotBarItems[selected].transform.Find("Text").GetComponent<TextMeshProUGUI>().text == "Paper Cutter")
        {
            GameObject item = Instantiate(machete, myCamera.transform.position + myCamera.transform.forward, myCamera.transform.rotation);
            item.GetComponent<Rigidbody>().AddForce(myCamera.transform.forward * 10 + transform.up * 4, ForceMode.Impulse);
            item.gameObject.tag = "throwable";
            ammoSet(selected, 0);
        }
        else if (HotBarItems[selected].transform.Find("Text").GetComponent<TextMeshProUGUI>().text == "Paper Shredder")
        {
            GameObject item = Instantiate(paperShred, myCamera.transform.position + myCamera.transform.forward, myCamera.transform.rotation);
            item.GetComponent<Rigidbody>().AddForce(myCamera.transform.forward * 10 + transform.up * 4, ForceMode.Impulse);
            item.gameObject.tag = "throwable";
            ammoSet(selected, 0);
        }
        /*else if (HotBarItems[selected].transform.Find("Text").GetComponent<Text>().text == "RBB")
        {
            GameObject item = Instantiate(RBB, myCamera.transform.position + myCamera.transform.forward, myCamera.transform.rotation);
            item.GetComponent<Rigidbody>().AddForce(myCamera.transform.forward * 10 + transform.up * 4, ForceMode.Impulse);
            //item.gameObject.tag = "throwable";
        }*/
        DeleteItem();

        GameObject held;
        held = Instantiate(emptyHanded, ItemSpawn.position, ItemSpawn.rotation);
        weaponName.text = " ";
        held.GetComponentInChildren<Animator>().SetTrigger("Throw");
    }

    public void DeleteItem()
    {
        Destroy(GameObject.FindGameObjectWithTag("ItemInUse"));
        HotBarItems[selected].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "";
        HotBarItems[selected].transform.Find("Image").GetComponent<Image>().sprite = emptyHanded_sprite;

    }
    public void DeleteItem(int selected)
    {
        HotBarItems[selected].transform.Find("Text").GetComponent<Text>().text = "";
        HotBarItems[selected].transform.Find("Image").GetComponent<Image>().sprite = emptyHanded_sprite;

    }

    public void ammoSet(int i, int ammoSet)
    {
        if(i == 0)
        {
            ammo1 = ammoSet;
        }
        if(i == 1)
        {
            ammo2 = ammoSet;
        }
        if(i == 2)
        {
            ammo3 = ammoSet;
        }
        if(i == 3)
        {
            ammo4 = ammoSet;
        }
        if(i == 4)
        {
            ammo5 = ammoSet;
        }
        if(i == 5)
        {
            ammo6 = ammoSet;
        }
    }

    public int getActiveWeapon()
    {
        return selected;
    }

    public string getWeaponName()
    {
        return HotBarItems[selected].transform.Find("Text").GetComponent<TextMeshProUGUI>().text ;
    }

    public int getAmmo()
    {
        int i = selected;
        if(i == 0)
        {
            return ammo1;
        }
        if(i == 1)
        {
            return ammo2;
        }
        if(i == 2)
        {
            return ammo3;
        }
        if(i == 3)
        {
            return ammo4;
        }
        if(i == 4)
        {
            return ammo5;
        }
        if(i == 5)
        {
            return ammo6;
        }
        else
        {
            return 0;
        }
    }
}
