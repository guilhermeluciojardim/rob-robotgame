using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyShip : MonoBehaviour
{
    private float FlyYMin;
    private float FlyYMax;

    private bool swapDirection = false;
    
       // Start is called before the first frame update
    void Start()
    {
        FlyYMin = transform.position.y;
        FlyYMax = transform.position.y + 1f;
    }
    // Update is called once per frame
    void Update()
    {
        // Makes the ship float
        if (!swapDirection){
            transform.Translate(Vector3.up * Time.deltaTime * 0.2f);
            if (transform.position.y >= FlyYMax) swapDirection = true;   
        }
        else{
            transform.Translate(Vector3.down * Time.deltaTime * 0.2f);
            if (transform.position.y <= FlyYMin) swapDirection = false;
        }
       
        
    }
}
