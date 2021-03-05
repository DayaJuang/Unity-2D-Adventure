using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    Vector3 offset = new Vector3(0,0,-10);

    //Make Camera run smoother
    [Range(0, 10f)] public float smoothFactor = 5f;

    //Max and Min bounds of the camera
    //Manually assign by moving the camera and look for the min bounds of x,y and the max bounds of x,y
    public Vector3 minBound,maxBound;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        //Assign the transform position of the target(player) to the new variable(Temp. camera transform position)
        Vector3 targetPosition = target.position + offset;
        //Add bounds to it
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, minBound.x, maxBound.x), Mathf.Clamp(targetPosition.y, minBound.y, maxBound.y), targetPosition.z); 
        //Smoothen the camera movement using Lerp and add the smooth facto multiply by deltatime
        Vector3 smoothCamera = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        //Assign the smoothen camera movement to the camera transform position
        transform.position = smoothCamera;
    }
}
