using UnityEngine;

// Attach this script to the Main Camera and assign the car as target.
public class ChaseCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 5f, -8f);
    public float followSmoothness = 6f;
    public float lookHeight = 1.2f;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(
            transform.position, desiredPosition,
            1f - Mathf.Exp(-followSmoothness * Time.deltaTime));

        Vector3 lookPoint = target.position + Vector3.up * lookHeight;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(lookPoint - transform.position),
            1f - Mathf.Exp(-followSmoothness * Time.deltaTime));
    }
}