using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public Transform parent;
    private GameObject item;
    private Rigidbody itemPick;
    public GameObject target;//target respawn object
    RaycastHit hit;

    [SerializeField]private MeshFilter targetmesh;
    public MeshFilter _item;
    void Update()
    {
        targetmesh.mesh = _item.mesh;//ini rencananya buat ngikutin mest object yang disellect tapi baru bisa untuk satu object
        int layerMask = 3;//
        float pickupRange=5f;
        float pickForce = 150f;

        layerMask = ~layerMask;
        if (Input.GetMouseButtonDown(0))
        {
            if (item == null)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange, layerMask))
                {
                    pickUp(hit.transform.gameObject);
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    Debug.Log("Did Hit");
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                    Debug.Log("Did not Hit");
                }
            }
        }
        else if (item != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                drop();
            }
        }
    }
    void pickUp(GameObject PickObj)//untuk pick object
    {
        if (PickObj.GetComponent<Rigidbody>())
        {
            
            itemPick = PickObj.GetComponent<Rigidbody>();
            itemPick.useGravity = false;
            itemPick.drag = 10;
            itemPick.constraints = RigidbodyConstraints.FreezeRotation;
            itemPick.transform.parent = parent;
            
            item = PickObj;
            target.active = true;
            item.active = false;


        }
    }
    void drop()
    {
        item.active = true;
        target.active = false;
        item.transform.position = new Vector3(target.transform.position.x,target.transform.position.y,target.transform.position.z);
        item.transform.rotation = target.transform.rotation;

        itemPick.useGravity = true;
        itemPick.drag = 1;
        itemPick.constraints = RigidbodyConstraints.None;
        itemPick.transform.parent = null;
        item = null;

    }
}

