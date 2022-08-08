using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //Variables



    //References

    public GameObject shotPrefab;
    public Transform weaponPos;
    public GameObject explosion;


    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            Fire();
        }
    }

    private void Fire(){
        GameObject shot = GameObject.Instantiate(shotPrefab, weaponPos.position,weaponPos.rotation) as GameObject;
		GameObject.Destroy(shot, 3f);
    }
    void OnCollisionEnter(Collision collider){
        if (collider.gameObject.CompareTag("Terrain")){
            GameObject blow = GameObject.Instantiate(explosion, weaponPos.position,weaponPos.rotation) as GameObject;
		    GameObject.Destroy(blow, 3f);
        }
   }
}

