using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFootsteps : MonoBehaviour
{
    void OnCollisionExit(Collision coll)
    {
        GetComponent<AudioSource>().Play();
    }
}
