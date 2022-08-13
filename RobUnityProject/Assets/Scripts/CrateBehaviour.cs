using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBehaviour : MonoBehaviour
{
    public GameObject explosion;
    public GameObject healthPrefab;

    private void OnCollisionEnter(Collision coll){
        if ((coll.gameObject.name == "shot_prefab(Clone)") || (coll.gameObject.name == "shot_prefab_enemy(Clone)")){
            GameObject blow = GameObject.Instantiate(explosion, transform.position, transform.rotation) as GameObject;
            GameObject.Destroy(blow, 1f);
            if (gameObject.CompareTag("KeyCrate")){
                Destroy(gameObject);
                GameObject health = GameObject.Instantiate(healthPrefab, transform.position, transform.rotation) as GameObject;
                
            }
            else{
                int randomHealth = Random.Range(1,10);
                if (randomHealth >=5){
                    GameObject health = GameObject.Instantiate(healthPrefab, transform.position, transform.rotation) as GameObject;
                }
                Destroy(gameObject);
            }
            
    }
    }
}
