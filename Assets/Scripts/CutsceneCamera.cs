using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneCamera : MonoBehaviour
{
    public bool isInCutScene = false;
    public bool simpleMovementDebug = false;
    public Vector3 cameraSpeeds;
    public GameObject cutSceneTarget;
    public float distanceToTravel;
    public float minSpeed;
    public float maxSpeed;

    private float distanceTraveled;
    void Start()
    {
        isInCutScene = true;
        distanceTraveled = 0;
        RandomizeCameraSpeeds();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInCutScene)
            CutSceneMovement();
    }

    private void CutSceneMovement()
    {
        Vector3 previousPosition = this.transform.position;
        this.transform.Translate(cameraSpeeds*Time.deltaTime, Space.World);
        this.transform.LookAt(cutSceneTarget.transform);
        distanceTraveled += Vector3.Distance(previousPosition, this.transform.position);
        if(Vector3.Distance(cutSceneTarget.transform.position, this.transform.position) <= 2f)
        {
            distanceTraveled = distanceToTravel * 2;
        }
        if (distanceTraveled >= distanceToTravel)
        {
            this.transform.position = new Vector3(cutSceneTarget.transform.position.x + Random.Range(-10, 10), cutSceneTarget.transform.position.y + Random.Range(0, 10), cutSceneTarget.transform.position.z + Random.Range(-10, 10));
            distanceTraveled = 0;
            RandomizeCameraSpeeds();
        }
    }

    private void RandomizeCameraSpeeds()
    {
        if (!simpleMovementDebug)
        {
            cameraSpeeds = new Vector3(Random.Range(minSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed));
            if (cameraSpeeds.y <= 0)
            {
                cameraSpeeds.y = 0;
            }
        }
    }
}
