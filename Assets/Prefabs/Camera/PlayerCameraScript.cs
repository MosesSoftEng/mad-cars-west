using UnityEngine;

/**
 * Class to control player camera behaviour
 */
public class PlayerCameraScript : MonoBehaviour
{
    [Header("Object to follow")] 
    public Transform targetTransform;

    [Header("Camera")]
    public Vector3 cameraOffset = new Vector3(0f, 7.5f, 0f);

    /**
     * Start is called before the first frame update
     */
    private void Start()
    {
        if (targetTransform == null)
            Debug.LogError("PlayerCameraScript: Player camera target object not set");
    }

    /**
     * Called several times per frame
     */
    private void FixedUpdate()
    {
        FollowTarget();
    }

    /**
     * Make camera follow object
     */
    private void FollowTarget()
    {
        var position = targetTransform.position;
        var transform1 = transform;

        var targetPos = new Vector3(
            position.x + cameraOffset.x,
            cameraOffset.y,
            position.z + cameraOffset.z
        );
        
        transform1.position = targetPos;
    }
}