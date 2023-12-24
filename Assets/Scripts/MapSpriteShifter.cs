using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * DO NOT FORGET!
 * 
 * GeoCoords are LON=X, LAT=Y. In real world LON is Y, and LAT is X!
 */
public class MapSpriteShifter : MonoBehaviour
{
    public SpriteRenderer image;

    public Vector2 imageSize = new Vector2(30.72f, 30.72f); 
    public Vector2 ldCorner = new Vector2(55.73762f,48.72372f); // x = Lon, y = lat;
    public Vector2 urCorner = new Vector2(55.76218f,48.76745f); // x = Lon, y = lat;

    public Vector2 currentCoords; // debug

    private Vector2 imageSizeHalf;
    private Vector2 centerCoords; // x = Lon, y = lat;
    private Vector2 cornersDifference; // x = Lon, y = lat;
    private Vector2 translateCoeffs;

    private void Start()
    {
        imageSizeHalf = new Vector2(imageSize.x / 2, imageSize.y / 2);
        cornersDifference = new Vector2(urCorner.x - ldCorner.x, urCorner.y - ldCorner.y);
        centerCoords = new Vector2(ldCorner.x + (cornersDifference.x/2), ldCorner.y + (cornersDifference.y/2));
        translateCoeffs = new Vector2(imageSizeHalf.x / cornersDifference.y * 2, imageSizeHalf.y / cornersDifference.x * 2);

        //SetCoordinates(new Vector3(55.7461f, 48.72681f));
        SetCoordinates(centerCoords);
    }

    public void UpdateImage()
    {
        SetCoordinates(currentCoords);
    }

    public void SetCoordinates(Vector2 coords)
    {
        this.currentCoords = coords;
        float imageScale = image.transform.localScale.x;
        image.transform.position = new Vector3((centerCoords.y - coords.y) * translateCoeffs.x * imageScale, (centerCoords.x - coords.x) * translateCoeffs.y * imageScale, 0);
    }
}
