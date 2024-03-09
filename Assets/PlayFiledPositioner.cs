using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFiledPositioner : MonoBehaviour
{
    public Scaler scaler;
    public MapSpriteShifter shifter;
    public GPSLocationGetter locationGetter;

    // Update is called once per frame
    void Update()
    {
        float lat = locationGetter.getLatitude();
        float lon = locationGetter.getLongitude();
        shifter.SetCoordinates(new Vector2(lat, lon));
    }
}
