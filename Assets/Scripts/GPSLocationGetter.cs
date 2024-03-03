using System.Collections;
using UnityEngine;
using System.Linq;

/**
 * Предоставление данных GPS
 */
public class GPSLocationGetter : MonoBehaviour
{
    public bool FakeGpsDebugMode = false;
    private Vector2 fakeGpsCoords;
    private Vector2 fakeGpsPathCenter = new Vector3(55.752085f, 48.744618f);
    private float fakeGpsTimer = 0;
    public float fakeGpsPathSpeed = 0.1f;
    public float fakeGpsPathRadius = 0.005f;

    public MapSpriteShifter shifter;

    public readonly short cacheSize = 5;
    public float initLatitude = 55.752085f;
    public float initLongitude = 48.744618f;
    private float[] latitudeCache;
    private float[] longitudeCache;
    private float[] altitudeCache;
    private float[] accurcyCache;
    private short currentCacheIndex = 0;
    private double timestamp = 0;
    private GPSStatus gpsStatus;

    public 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GPSLocationEnabledCheck());
        InvokeRepeating("UpdateGPSData", 0.5f, 1f);
        latitudeCache = new float[cacheSize];
        longitudeCache = new float[cacheSize];
        altitudeCache = new float[cacheSize];
        accurcyCache = new float[cacheSize];

        for (int i = 0; i < cacheSize; i++)
        {
            latitudeCache[i] = initLatitude;
            longitudeCache[i] = initLongitude;
        }
    }

    IEnumerator GPSLocationEnabledCheck() 
    {
        gpsStatus = GPSStatus.INITIALIZING;
        yield return new WaitForSeconds(5);

        if (!Input.location.isEnabledByUser)
        {
            gpsStatus = GPSStatus.DISABLED;
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
            gpsStatus = GPSStatus.TIMEOUT; ;
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            gpsStatus = GPSStatus.FAILED; ;
            yield break;
        }
        else
        {
            gpsStatus = GPSStatus.RUNNING;
        }
        
    }

    private void UpdateGPSData()
    {
        currentCacheIndex++;
        if (currentCacheIndex >= cacheSize)
            currentCacheIndex = 0;

        if (!FakeGpsDebugMode)
        {
            if (Input.location.status == LocationServiceStatus.Running)
            {
                gpsStatus = GPSStatus.RUNNING;
                latitudeCache[currentCacheIndex] = Input.location.lastData.latitude;
                longitudeCache[currentCacheIndex] = Input.location.lastData.longitude;
                altitudeCache[currentCacheIndex] = Input.location.lastData.altitude;
                accurcyCache[currentCacheIndex] = Input.location.lastData.horizontalAccuracy;
                timestamp = Input.location.lastData.timestamp;             
            }
        }
        else
        {
            gpsStatus = GPSStatus.FAKE;
            latitudeCache[currentCacheIndex] = fakeGpsCoords.x;
            longitudeCache[currentCacheIndex] = fakeGpsCoords.y;
        }


        shifter.SetCoordinates(new Vector2(getLatitude(), getLongitude()));
    }

    private void Update()
    {
        fakeGpsTimer += Time.deltaTime;
        fakeGpsCoords = fakeGpsPathCenter + new Vector2(
            Mathf.Cos(fakeGpsTimer * fakeGpsPathSpeed) / 2 * fakeGpsPathRadius,
            Mathf.Sin(fakeGpsTimer * fakeGpsPathSpeed) * fakeGpsPathRadius);
    }

    public GPSStatus GetGPSStatus() 
    {
        return gpsStatus;
    }

    public float getLatitude() 
    {
        return latitudeCache.Sum() / cacheSize;
    }

    public float getLongitude() 
    {
        return longitudeCache.Sum() / cacheSize;
    }

    public float getAltitude()
    {
        return altitudeCache.Sum() / cacheSize;
    }

    public float getAccuracy()
    {
        return accurcyCache.Sum() / cacheSize;
    }

    public double getTimestamp()
    {
        return timestamp;
    }

    public enum GPSStatus
    {
        INITIALIZING,
        RUNNING,
        TIMEOUT,
        DISABLED,
        FAILED,
        FAKE
    }
}
