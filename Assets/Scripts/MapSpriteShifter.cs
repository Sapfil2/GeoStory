using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

/**
 * DO NOT FORGET!
 * 
 * GeoCoords are LON=X, LAT=Y. In real world LON is Y, and LAT is X!
 */
public class MapSpriteShifter : MonoBehaviour
{
    public GameObject playField;

    public Vector2 imageSize = new Vector2(30.72f, 30.72f); 
    public Vector2 ldCorner = new Vector2(55.73762f,48.72372f); // x = Lon, y = lat;
    public Vector2 urCorner = new Vector2(55.76218f,48.76745f); // x = Lon, y = lat;
    public float movementSmoothFactor = 1.03f;
    public Vector3 correction;

    public Vector3 acceptedCoords; // debug

    private Vector2 imageSizeHalf;
    private Vector2 centerCoords; // x = Lon, y = lat;
    private Vector2 cornersDifference; // x = Lon, y = lat;
    private Vector2 translateCoeffs;

    private Vector3 currentPosition;
    private Vector2 lastGpsCoords;

    public bool instantMode;
    public TMP_Text modeText;

    private void Start()
    {
        imageSizeHalf = new Vector2(imageSize.x / 2, imageSize.y / 2);
        cornersDifference = new Vector2(urCorner.x - ldCorner.x, urCorner.y - ldCorner.y);
        centerCoords = new Vector2(ldCorner.x + (cornersDifference.x/2), ldCorner.y + (cornersDifference.y/2));
        translateCoeffs = new Vector2(imageSizeHalf.x / cornersDifference.y * 2, imageSizeHalf.y / cornersDifference.x * 2);

        SetCoordinates(centerCoords);
        currentPosition = playField.transform.localPosition;
    }

    public void switchMode() 
    {
        instantMode = !instantMode;
        if (instantMode) { modeText.text = "MODE: INSTANT"; }
        else { modeText.text = "MODE: INTERPOLATE"; }
    }

    private void Update()
    {
        if (instantMode)
        {
            playField.transform.localPosition = acceptedCoords + correction;
        }
        else
        {
            Vector3 newCoords = ((currentPosition - acceptedCoords) / movementSmoothFactor) + acceptedCoords;
            currentPosition = newCoords;
            playField.transform.localPosition = currentPosition + correction;
        }
    }

    public void UpdateImageScale(float scale)
    {
        playField.transform.localScale = new Vector3(scale, scale, 1);
        currentPosition = transformGpsDataToLocalDecartCoordinates(lastGpsCoords);
        acceptedCoords = currentPosition;
    }

    public void SetCoordinates(Vector2 gpsCoords)
    {
        acceptedCoords = transformGpsDataToLocalDecartCoordinates(gpsCoords);
    }

    private Vector3 transformGpsDataToLocalDecartCoordinates(Vector2 gpsCoords)
    {
        lastGpsCoords = gpsCoords;
        float imageScale = playField.transform.localScale.x;
        return new Vector3(
            (centerCoords.y - gpsCoords.y) * translateCoeffs.x * imageScale,
            (centerCoords.x - gpsCoords.x) * translateCoeffs.y * imageScale, 0);
    }
}
