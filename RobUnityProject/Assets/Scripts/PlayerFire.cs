using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //Variables



    //References

    public GameObject shotPrefab;
    public Transform weaponPos;


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
		GameObject.Destroy(shot, 2f);
    }
    
}

