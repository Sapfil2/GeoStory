using System;
using TMPro;
using UnityEngine;
using static GPSLocationGetter;

public class GpsTextShow : MonoBehaviour
{
    public TMP_Text GPSStatus;
    public TMP_Text latitude;
    public TMP_Text longitude;
    public TMP_Text altitude;
    public TMP_Text horizontalAccuracy;
    public TMP_Text timestampText;
    public String defaultFormatter = "N3";

    public GPSLocationGetter locationGetter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GPSStatus.text = locationGetter.GetGPSStatus().ToString();
        latitude.text = "LAT " + locationGetter.getLatitude().ToString(defaultFormatter);
        longitude.text = "LON " + locationGetter.getLongitude().ToString(defaultFormatter);
        altitude.text = "ALT " + locationGetter.getAltitude().ToString(defaultFormatter);
        horizontalAccuracy.text = "ACC " + locationGetter.getAccuracy().ToString(defaultFormatter);
        timestampText.text = "TIM " + getLocalDateLimeFromTimestamp(locationGetter.getTimestamp());
    }

    private string getLocalDateLimeFromTimestamp(double timestamp)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timestamp);
        return dateTime.ToString();
    }
}
