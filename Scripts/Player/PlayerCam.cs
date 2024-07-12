using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX, sensY;
    public Transform player, leftArm;

    float xRot, yRot, mouseX, mouseY;

    float timer = 0;
    private void Start()
    { 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > .5)
        {
            mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * sensX;
            mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * sensY;

            xRot -= mouseY;
            xRot = Mathf.Clamp(xRot, -90f, 90f);

            yRot += mouseX;

            transform.localRotation = Quaternion.Euler(xRot, 0, 0);
            player.rotation = Quaternion.Euler(0, yRot, 0);
            //leftArm.rotation = Quaternion.Euler(xRot, yRot, 0);

            transform.position = new Vector3(player.position.x, player.position.y + .5f, player.position.z);
        }
    }
}
