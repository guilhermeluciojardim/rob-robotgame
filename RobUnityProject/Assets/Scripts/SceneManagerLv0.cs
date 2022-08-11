using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerLv0 : MonoBehaviour
{
    //References
    public Animator animFade;
    public GameObject rocket;
    public GameObject Text;
    public GameObject rocketExplosion;
    public Transform ship;
    public float moveSpeed;
    private bool start = false;
    private bool end = false;


    public Transform cameraTransform;
    private Vector3 orignalCameraPos;

    // Shake Parameters
    public float shakeDuration = 6.5f;
    public float shakeAmount = 0.3f;

    private bool canShake = false;
    private float _shakeTimer;
   
    // Update is called once per frame
    void Start(){
        orignalCameraPos = cameraTransform.localPosition;
    }
    
    void Update()
    {
        if (start){
            Vector3 directionToMove = ship.position - rocket.transform.position;
            directionToMove = directionToMove.normalized * Time.deltaTime * moveSpeed;

            float maxDistance = Vector3.Distance(rocket.transform.position, ship.position);
            rocket.transform.position = rocket.transform.position + Vector3.ClampMagnitude(directionToMove, maxDistance);
            if (rocket.transform.position == ship.transform.position){
                Destroy(rocket);
                start=false;
                end=true;
                GameObject explode = GameObject.Instantiate(rocketExplosion, ship.position, ship.rotation) as GameObject;
		        GameObject.Destroy(explode, 2f);
                ShakeCamera();
                StartCoroutine(WaitforTime(5));
            }
        }
        if (end){
                StartCameraShakeEffect();
            }
    }

    public void ShakeCamera()
    {
        canShake = true;
        _shakeTimer = shakeDuration;
    }

    public void StartCameraShakeEffect()
    {
        if (_shakeTimer > 0)
        {
            cameraTransform.localPosition = orignalCameraPos + Random.insideUnitSphere * shakeAmount;
            _shakeTimer -= Time.deltaTime;
        }
        else
        {
            _shakeTimer = 0f;
            cameraTransform.position = orignalCameraPos;
            canShake = false;
        }
    }

    public void ActivateStartGame(){
        start=true;
        Text.SetActive(false);
    }

    IEnumerator  WaitforTime(float waitTime){
        yield return new WaitForSeconds(waitTime);
        animFade.SetTrigger("Fade_Out");
        StartCoroutine(WaitforSceneChange(2));
    }
     IEnumerator  WaitforSceneChange(float waitTime){
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Level - 1");
    }

}
