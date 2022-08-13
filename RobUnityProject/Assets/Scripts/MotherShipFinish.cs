using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipFinish : MonoBehaviour
{
    public GameObject BigExplosion;
    private void OnCollisionEnter(Collision coll){
        if (coll.gameObject.CompareTag("MotherShip")){
            GameObject blow = GameObject.Instantiate(BigExplosion, coll.transform.position, coll.transform.rotation) as GameObject;
            GameObject.Destroy(blow, 2f);
            Destroy(gameObject);

        

        }
        else if (coll.gameObject.CompareTag("Shootable")){
        GameObject blow = GameObject.Instantiate(BigExplosion, transform.position, transform.rotation) as GameObject;
        GameObject.Destroy(blow, 1f);
        Destroy(gameObject);
    }
    }
}
