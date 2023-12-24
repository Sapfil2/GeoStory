using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YandexMaps;

public class TestMap : MonoBehaviour
{

    public RawImage image;
    public Map.TypeMap typeMap;
    public Map.TypeMapLayer typeMapLayer;
    private int size = 5;
    private float latitude;
    private float longitude;
    public TMP_Text sizeText;

    public void setCoordinates(float latitude, float longitude)
    {
        this.latitude = latitude;
        this.longitude = longitude;
    }

    public void IncreaseSize()
    {
        this.size++;
        sizeText.text = "Size " + this.size.ToString();
    }

    public void DecreaseSize()
    {
        this.size--;
        sizeText.text = "Size " + this.size.ToString();
    }

    public void LoadMap()
    {
        sizeText.text = "Size " + this.size.ToString();
        Map.EnabledLayer = true;
        Map.Size = this.size;
        Map.Latitude = latitude;
        Map.Longitude = longitude;
        Map.SetTypeMap = typeMap;
        Map.SetTypeMapLayer = typeMapLayer;
        Map.LoadMap();
        StartCoroutine(LoadTexture());  
    }

    IEnumerator LoadTexture() 
    {
        yield return new WaitForSeconds(3);
        image.texture = Map.GetTexture;
    }
}
