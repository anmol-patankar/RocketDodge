using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;
    public Camera myCamera;
    float speed;
    [SerializeField] float LowFov;
    [SerializeField] float maxFov;
    [SerializeField] float topSpeed;
    float correctionFactor;
    [SerializeField] Vector3 vector = new Vector3(1f, 1f, 1f);
    // units/sec
    void Start()
    {
        myCamera.fieldOfView = LowFov;
    }


    void Update()
    {
        speed = GameObject.Find("PlayerRocket").GetComponent<Movement>().velocityMagnitude;
        transform.position = player.transform.position + vector;
        correctionFactor = speed + ((maxFov - LowFov) / topSpeed);

        // Then, to modify the camera's FOV, do this:
        if (myCamera.fieldOfView < maxFov)
        {
            myCamera.fieldOfView = LowFov + correctionFactor;
        }
        else
            myCamera.fieldOfView = maxFov;



    }
}