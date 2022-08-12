using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExplosion : MonoBehaviour
{
    public GameObject explosion;
    // Start is called before the first frame update
   void OnCollisionEnter(Collision coll){
    if (coll.gameObject.CompareTag("Shootable")){
        GameObject blow = GameObject.Instantiate(explosion, transform.position, transform.rotation) as GameObject;
        GameObject.Destroy(blow, 1f);
        Destroy(gameObject);
    }
   }
}
