using System.Collections;
using UnityEngine;

public static class CameraShakerExtension
{
    public static void Shake(this Camera camera, float shakePower)
    {
        camera.GetComponent<MonoBehaviour>().StartCoroutine(ShakeRoutine(camera, shakePower));
    }
    public static IEnumerator ShakeRoutine(Camera camera, float power)
    {
        Vector3 startPosition = camera.transform.position;
        Vector3 shakePower = Random.insideUnitSphere * power;

        var inTime = .05f;
        var outTime = .1f;

        var timer = 0f;
        while (timer < inTime)
        {
            timer += Time.deltaTime;
            camera.transform.position = Vector3.Lerp(startPosition, startPosition + shakePower, timer / inTime);
            yield return null;

        }

        timer = 0;
        while (timer < outTime)
        {
            timer += Time.deltaTime;
            camera.transform.position = Vector3.Lerp(startPosition + shakePower, startPosition , timer / outTime);
            yield return null;

        }

    }
}
