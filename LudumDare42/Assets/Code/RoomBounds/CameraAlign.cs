using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAlign : MonoBehaviour {

    private Bounds currentBounds;

    private float alignDuration = .5f;

    IEnumerator AlignToNewBounds()
    {

        Vector3 startVect = transform.position;
        Vector3 trackingVect = transform.position;

        float targX = currentBounds.center.x;
        float targY = currentBounds.center.y;
        Vector3 targetPosition = new Vector3(targX, targY, transform.position.z);

        float lerpTime = 0;
        while (lerpTime < alignDuration)
        {

            lerpTime += Time.deltaTime;
            trackingVect = Vec3Lerp(lerpTime, alignDuration, startVect, targetPosition);
            transform.position = trackingVect;
            yield return 0;
        }

        transform.position = targetPosition;
    }

    public void SetNewBounds(Bounds newBounds)
    {

        currentBounds = newBounds;
        StartCoroutine(AlignToNewBounds());
    }

    public static Vector3 Vec3Lerp(float currentTime, float duration, Vector3 v3_start, Vector3 v3_target)
    {
        float step = (currentTime / duration);
        Vector3 v3_ret = Vector3.Lerp(v3_start, v3_target, step);
        return v3_ret;
    }
}
