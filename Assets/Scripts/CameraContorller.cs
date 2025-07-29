using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContorller : MonoBehaviour
{
    public float panSpeed = 30f;
    private float panBorderThickness = 5f;
    //private bool doMoveMent = true;
    private float scrollSpeed = 10f;
    private float minY = 70f;
    private float maxY = 200f;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(new Vector3(-1f, 0, 0) * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(new Vector3(1f, 0, 0) * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(new Vector3(0, 0, 1f) * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(new Vector3(0, 0, -1f) * panSpeed * Time.deltaTime, Space.World);
        }

        //if (Input.GetKey("w"))
        //{
        //    transform.Translate(new Vector3(-1f, 0, 0) * panSpeed * Time.deltaTime, Space.World);
        //}
        //if (Input.GetKey("s"))
        //{
        //    transform.Translate(new Vector3(1f, 0, 0) * panSpeed * Time.deltaTime, Space.World);
        //}
        //if (Input.GetKey("d"))
        //{
        //    transform.Translate(new Vector3(0, 0, 1f) * panSpeed * Time.deltaTime, Space.World);
        //}
        //if (Input.GetKey("a"))
        //{
        //    transform.Translate(new Vector3(0, 0, -1f) * panSpeed * Time.deltaTime, Space.World);
        //}

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}
