using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneCamera : MonoBehaviour
{
    public bool isInCutScene = false;
    public bool simpleMovementDebug = false;
    public bool usingDefinedPos = false;
    public Vector3 cameraSpeeds;
    public Vector3[] startPos;
    public Vector3[] speeds;
    public GameObject cutSceneTarget;
    public float distanceToTravel;
    public float minSpeed;
    public float maxSpeed;

    private float distanceTraveled;
    private int arrayIndex;
    void Start()
    {
        isInCutScene = true;
        distanceTraveled = 0;
        arrayIndex = -1;
        DefineCameraPosAndSpeeds();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInCutScene)
            CutSceneMovement();
    }

    public void DebugMove()
    {
        simpleMovementDebug = true;
        usingDefinedPos = false;
    }
    public void RandomMove()
    {
        simpleMovementDebug = false;
        usingDefinedPos = false;
    }
    public void DefinedMove()
    {
        simpleMovementDebug = false;
        usingDefinedPos = true;
    }

    private void CutSceneMovement()
    {
        Vector3 previousPosition = this.transform.position;
        this.transform.Translate(cameraSpeeds*Time.deltaTime, Space.World);
        this.transform.LookAt(cutSceneTarget.transform);
        distanceTraveled += Vector3.Distance(previousPosition, this.transform.position);
        if(Vector3.Distance(cutSceneTarget.transform.position, this.transform.position) <= 3f)
        {
            distanceTraveled = distanceToTravel * 2;
        }
        if (distanceTraveled >= distanceToTravel)
        {
            this.transform.position = new Vector3(cutSceneTarget.transform.position.x + Random.Range(-10, 10), cutSceneTarget.transform.position.y + Random.Range(0, 10), cutSceneTarget.transform.position.z + Random.Range(-10, 10));
            distanceTraveled = 0;
            DefineCameraPosAndSpeeds();
        }
    }

    private void DefineCameraPosAndSpeeds()
    {
        if (!simpleMovementDebug && !usingDefinedPos)
        {
            cameraSpeeds = new Vector3(Random.Range(minSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed));
            if (cameraSpeeds.y <= 0)
            {
                cameraSpeeds.y = 0;
            }
        }

        if (usingDefinedPos)
        {
            arrayIndex += 1;
            if ((arrayIndex >= startPos.Length) || (arrayIndex >= speeds.Length))
                arrayIndex = 0;

            this.transform.position = startPos[arrayIndex];
            cameraSpeeds = speeds[arrayIndex];
        }
    }
}
