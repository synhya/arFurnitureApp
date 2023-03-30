using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureInfo : MonoBehaviour
{
    public GameObject furnitureModelImagePrefab;
    public int furnitureId = 1;

    public bool isFavorite = false; // 메모리 낭비

    public Vector3 furnitureRotation; // save rotation and position
    public Vector3 furnitureScale = Vector3.one; // save rotation and position
    void Start()
    {
        isFavorite = FavoriteListManager.Instance.favoriteDict[furnitureId].isFavorite;
        furnitureRotation = FavoriteListManager.Instance.favoriteDict[furnitureId].furnitureRotation;
        furnitureScale = FavoriteListManager.Instance.favoriteDict[furnitureId].furnitureScale;
    }
}
