using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = Vector3.zero;
        position.y = ((Mathf.PerlinNoise(Time.time*5,0f)*2f)-1f)*0.05f;
        gameObject.transform.localPosition = position;
    }
}
