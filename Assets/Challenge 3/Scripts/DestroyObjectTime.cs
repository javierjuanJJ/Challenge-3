using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectTime : MonoBehaviour
{

    public float timeToDestroy = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
