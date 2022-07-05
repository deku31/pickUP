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
    public float speed=10f;
    [SerializeField] private Collider objcol;

    bool pickupCondition=false;

    [SerializeField]private MeshFilter targetmesh;
    public MeshFilter _item;

    public bool dontdrop;

    void Update()
    {
        targetmesh.mesh = _item.mesh;//ini rencananya buat ngikutin mest object yang disellect tapi baru bisa untuk satu object
        int layerMask = 3;//
        float pickupRange=5f;
        float pickForce = 150f;

        
        layerMask = ~layerMask;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange, layerMask)  )
        {
            if (item == null)
            {
                if (Input.GetMouseButtonDown(0))
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
            else if (item != null)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if (dontdrop==false)
                    {
                        drop();
                    }
                    else
                    {
                        Debug.Log("tidak bisa di letakan disini");
                    }
                }
            }
            if (hit.collider.tag == "ground")
            {
                target.transform.position = Vector3.MoveTowards(target.transform.position, hit.point, Time.deltaTime * speed);
                if (pickupCondition)
                {
                    itemPick.transform.position = parent.transform.position;
                }
            }

        }
       
    }
    void pickUp(GameObject PickObj)//untuk pick object
    {
        if (PickObj.GetComponent<Rigidbody>())
        {
            pickupCondition = true;
            itemPick = PickObj.GetComponent<Rigidbody>();
            itemPick.useGravity = false;
            itemPick.drag = 10;
            itemPick.constraints = RigidbodyConstraints.FreezeRotation;
            objcol.isTrigger = true;
            item = PickObj;
            //target.active = true;
            //item.active = false;


        }
    }
    void drop()
    {
        //item.active = true;
        //target.active = false;
        pickupCondition = false;
        item.transform.rotation = target.transform.rotation;
        objcol.isTrigger = false;

        itemPick.useGravity = true;
        itemPick.drag = 1;
        itemPick.constraints = RigidbodyConstraints.None;
        itemPick.transform.parent = null;
        item = null;

    }
   
}

