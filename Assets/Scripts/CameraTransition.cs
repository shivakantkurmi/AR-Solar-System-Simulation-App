using UnityEngine;
using System.Collections;

public class CameraTransition : MonoBehaviour
{
    public float transitionDuration = 3f;  // How long the transition takes
    private bool isTransitioning = false;

    // Call this method to begin transitioning toward a target planet
    public void TransitionToPlanet(Transform targetPlanet, Vector3 offset)
    {
        if (!isTransitioning)
            StartCoroutine(MoveCamera(targetPlanet, offset));
    }

    IEnumerator MoveCamera(Transform target, Vector3 offset)
    {
        isTransitioning = true;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        
        // Calculate the target position (e.g., a point offset from the planet's center)
        Vector3 targetPos = target.position + offset;
        // Set the target rotation to look at the planet
        Quaternion targetRot = Quaternion.LookRotation(target.position - targetPos);

        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
        transform.rotation = targetRot;
        isTransitioning = false;
    }
}
