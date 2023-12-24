using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net.NetworkInformation;

public class GPSLocation : MonoBehaviour
{
    public TMP_Text GPSStatus; 
    public TMP_Text latitude; 
    public TMP_Text longitude; 
    public TMP_Text altitude; 
    public TMP_Text horizontalAccuracy; 
    public TMP_Text timestamp;
    public MapSpriteShifter shifter;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GPSLocationEnabledCheck());
        InvokeRepeating("UpdateGPSData", 0.5f, 1f);
    }

    IEnumerator GPSLocationEnabledCheck() 
    {
        GPSStatus.text = "Initializing";
        yield return new WaitForSeconds(5);

        if (!Input.location.isEnabledByUser)
        {
            GPSStatus.text = "Disabled By User";
            yield break;
        }

        Input.location.Start();
        int maxWaitSeconds = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWaitSeconds > 0)
        {
            yield return new WaitForSeconds(1);
            maxWaitSeconds--;
        }

        if (maxWaitSeconds < 0)
        {
            GPSStatus.text = "Time out";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            GPSStatus.text = "Failed to find GPS";
            yield break;
        }
        else
        {
            GPSStatus.text = "GPS running";
        }
        
    }

    private void UpdateGPSData()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            shifter.SetCoordinates(new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude));
            GPSStatus.text = "GPS running";
            latitude.text = "LAT " + Input.location.lastData.latitude.ToString();
            longitude.text = "LON " + Input.location.lastData.longitude.ToString();
            altitude.text = "ALT " + Input.location.lastData.altitude.ToString();
            horizontalAccuracy.text = "ACC " + Input.location.lastData.horizontalAccuracy.ToString();
            timestamp.text = "TIM " + Input.location.lastData.timestamp.ToString();
        }
    }
}
