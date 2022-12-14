using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCellBehaviour : MonoBehaviour
{
    private float FlyYMin;
    private float FlyYMax;

    private bool swapDirection = false;
    
       // Start is called before the first frame update
    void Start()
    {
        FlyYMin = transform.position.y;
        FlyYMax = transform.position.y + 0.2f;
    }
    // Update is called once per frame
    void Update()
    {
        // Makes the ship float
        if (!swapDirection){
            transform.Translate(Vector3.up * Time.deltaTime * 0.4f);
            if (transform.position.y >= FlyYMax) swapDirection = true;   
        }
        else{
            transform.Translate(Vector3.down * Time.deltaTime * 0.4f);
            if (transform.position.y <= FlyYMin) swapDirection = false;
        }
        if (gameObject.CompareTag("HealthShell")){
            GameObject.Destroy(gameObject, 20f);
        }
    }

    private void OnCollisionEnter(Collision coll){
        if ((gameObject.CompareTag("PowerShell")) && (coll.gameObject.CompareTag("Player"))){
            coll.gameObject.GetComponent<PlayerController>().FindKey();
            Destroy(gameObject);
        }
        else{
            if ((gameObject.CompareTag("HealthShell")) && (coll.gameObject.CompareTag("Player"))){
            coll.gameObject.GetComponent<PlayerController>().RecoverHealth();
            Destroy(gameObject);
    }
        }
        
    }
}
