using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FavoriteListManager : MonoBehaviour
{
    private static FavoriteListManager instance = null;

    // public Transform panel;
    // public CanvasGroup background;

    public Transform content;

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

    public static FavoriteListManager Instance
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

    public Dictionary<int, FurnitureInfo> favoriteDict = new Dictionary<int, FurnitureInfo>();


    // onClick
    public void MakeFavList()
    {
        List<FurnitureInfo> infos = new List<FurnitureInfo>(favoriteDict.Values);

        foreach (var info in infos)
        {
            if (info.isFavorite)
            {
                Instantiate(info.furnitureModelImagePrefab, Vector2.zero, Quaternion.identity, content);
            }
        }
    }

    // clear content
    public void ClearFavList()
    {
        StartCoroutine(ClearCoroutine());
    }
    public IEnumerator ClearCoroutine()
    {
        yield return new WaitForSeconds(1);

        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
    }

    // public void ShowPanel()
    // {
    //     // Update Fav info
    //     FurniturePanel.Instance.furnitureInfo = new List<FurnitureInfo>(favoriteDict.Values);
    //     FurniturePanel.Instance.CreateFurnitures();

    //     background.blocksRaycasts = true;
    //     background.alpha = 0;
    //     background.LeanAlpha(1, 1f);

    //     panel.localPosition = new Vector2(0, -1165);
    //     panel.LeanMoveLocalY(0, 1f).setEaseOutExpo().delay = 0.1f;
    // }

    // public void HidePanel()
    // {
    //     background.LeanAlpha(0, 1f);
    //     panel.LeanMoveLocalY(-1165, 1f).setEaseOutExpo();
    //     background.blocksRaycasts = false;
    // }
}
