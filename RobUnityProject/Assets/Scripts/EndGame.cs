using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private  GameObject allEnemies;
    [SerializeField] private GameObject BigExplosion;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera endGameCamera;

    [SerializeField] private TextMeshProUGUI winnerText;

    private bool isShipReadyToFall = false;
    private bool isShipHitTheGround = false;

    private int count;
    void OnCollisionEnter(Collision coll){
        if (coll.gameObject.name == "powershot_prefab(Clone)"){
            EndGameAnimation();
        }
    }
    
    public void EndGameAnimation(){
        //Deactivate Enemies
        allEnemies.SetActive(false);
        mainCamera.gameObject.SetActive(false);
        endGameCamera.gameObject.SetActive(true);
        winnerText.gameObject.SetActive(true);
        GetComponent<FlyShip>().enabled = !GetComponent<FlyShip>().enabled;
        isShipReadyToFall = true;
        InvokeRepeating("GenerateExplosions",2f,5);
        GetComponent<AudioSource>().Play();
    }

    void Update(){
        if ((isShipReadyToFall) && (!isShipHitTheGround)){
            transform.Rotate(Vector3.right * Time.deltaTime);
            transform.Translate(Vector3.down * Time.deltaTime);
            count+=1;
            if (count == 1000){
                isShipHitTheGround = true;
                count=0;
            }
        }
        
    }


    private void GenerateExplosions(){
        GameObject BigExp = GameObject.Instantiate(BigExplosion, transform.position, transform.rotation) as GameObject;
        GameObject.Destroy(BigExp, 1f);
    }

    }
