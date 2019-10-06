using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------
// --------------> Ask Frey about this script <--------------
// ----------------------------------------------------------

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            // Hard coded for boss room dimentions.
            transform.localPosition = new Vector3(11 + x, 23 + y, -35f);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPos;
    }

}