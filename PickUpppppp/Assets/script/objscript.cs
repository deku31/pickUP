using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objscript : MonoBehaviour
{
    [SerializeField] private Material[] objcolorchange;
    [SerializeField] private MeshRenderer objmest;
    private PickUpScript pick;

    // Start is called before the first frame update
    void Start()
    {
        pick = GameObject.Find("object pick").GetComponent<PickUpScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag=="dontdropobject")
        {
            objmest.material = objcolorchange[1];
            print("dontdroped");
            pick.dontdrop = true;
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag== "dontdropobject")
        {
            objmest.material = objcolorchange[0];
            pick.dontdrop = false;
        }
    }
}
