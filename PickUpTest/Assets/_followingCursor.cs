using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _followingCursor : MonoBehaviour
{
    public Camera cam;
    public GameObject obj;
    RaycastHit hit;
    Ray ray;
    public float speed=10f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);//menginpust arah mouse
        if (Physics.Raycast(ray,out hit))
        {
            if (hit.collider.tag== "ground")//ketika cursor menyentuh tag tanah akan mengubah posisi dari object
            {
                transform.position = Vector3.MoveTowards(transform.position, hit.point, Time.deltaTime* speed);
            }
        }
    }
}
