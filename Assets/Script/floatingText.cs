using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingText : MonoBehaviour
{
    public float destroyTime = 3f;
    public AudioSource audioSource;

  //  public Vector3 random = new Vector3(0.5f, 0, 0);
    void Start()
    {
        Destroy(gameObject, destroyTime);
        audioSource.enabled = true;
       // transform.localPosition += new Vector3(Random.Range(random.x, random.x), Random.Range(random.y, random.y), Random.Range(random.z, random.z));
    }

   
}
