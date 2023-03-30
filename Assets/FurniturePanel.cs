using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurniturePanel : MonoBehaviour
{
    private static FurniturePanel instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static FurniturePanel Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public List<FurnitureInfo> furnitureInfo;
    Vector3 displayPos = new Vector3(0f, 0f, -1000f);

    List<GameObject> models;
    public void CreateFurnitures()
    {
        models = new List<GameObject>();
        foreach (var info in furnitureInfo)
        {
            var model = Instantiate(info.furnitureModelImagePrefab, displayPos, Quaternion.identity, transform);
            models.Add(model);
            model.SetActive(false);
        }
    }
}
