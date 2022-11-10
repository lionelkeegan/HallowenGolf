using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GPS : MonoBehaviour
{
    [SerializeField] string latitude;
    [SerializeField] string longitude;
    [SerializeField] string altitude;
    [SerializeField] string horizontalAccuracy;
    [SerializeField] string timestamp;
    Coroutine ActivatedGPSCouroutine;

    void Update()
    {
        if (Input.location.status != LocationServiceStatus.Running)
            return;

        Debug.Log("Location is running");
        latitude = "xxx." + Input.location.lastData.latitude.ToString("F2").Split('.')[1];
        longitude = "xxx." + Input.location.lastData.longitude.ToString("F2").Split('.')[1];
        altitude = "xxx." + Input.location.lastData.altitude.ToString("F2").Split('.')[1];
        horizontalAccuracy = Input.location.lastData.horizontalAccuracy.ToString();
        timestamp = Input.location.lastData.timestamp.ToString();

        this.transform.rotation = Quaternion.Euler(0, -Input.compass.trueHeading, 0);

    }

    private void OnEnable()
    {
        if (ActivatedGPSCouroutine == null)
        {
            ActivatedGPSCouroutine = StartCoroutine(ActivateGPS());
        }
    }

    private void OnDisable()
    {
        StopCoroutine(ActivatedGPSCouroutine);
        if (Input.location.status == LocationServiceStatus.Running)
        {
            Input.location.Stop();
        }
    }

    IEnumerator ActivateGPS()
    {
#if UNITY_EDITOR
        Debug.Log("Unity Remote Connecting....");
        while (UnityEditor.EditorApplication.isRemoteConnected == false)
        {
            yield return new WaitForSecondsRealtime(1);
        }
#endif
        Debug.Log("Unity Remote Connected");

        if (Input.location.isEnabledByUser == false)
        {
            Debug.Log("Location Services Is Not Enabled By Users");
            yield break;
        }

        Debug.Log("Start Location Services");
        Input.location.Start();

        int maxWait = 15;
        while (Input.location.status == LocationServiceStatus.Stopped || Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            Debug.Log("Location Services Status Check : " + Input.location.status);
            yield return new WaitForSecondsRealtime(1);
            maxWait -= 1;
        }

        if (maxWait <= 0)
        {
            Debug.Log("Location Services Time Out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Location Services Failed");
            yield break;
        }

        Input.compass.enabled = true;

    }
}