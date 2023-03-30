using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour
{
    private static FirebaseManager instance = null;
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

    public static FirebaseManager Instance
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

    DatabaseReference m_Reference;
    public List<FurnitureInfo> listInfo = new List<FurnitureInfo>();

    void Start()
    {
        m_Reference = FirebaseDatabase.DefaultInstance.RootReference;

        // build favoriteDict
        foreach (var info in listInfo)
        {
            FavoriteListManager.Instance.favoriteDict.Add(info.furnitureId, info);
        }
        //**************************************************
        ReadAllInfoData();
        //**************************************************
    }

    public void ReadAllInfoData()
    {
        FirebaseDatabase.DefaultInstance.GetReference("furnitures")
            .GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...

            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                // Do something with snapshot...
                for (int id = 1; id <= 13; id++)
                {
                    if (snapshot.HasChild(id.ToString()))
                    {
                        Vector3 rotation = new Vector3(
                            float.Parse(snapshot.Child(id.ToString()).Child("rotation").Child("x").Value.ToString()),
                            float.Parse(snapshot.Child(id.ToString()).Child("rotation").Child("y").Value.ToString()),
                            float.Parse(snapshot.Child(id.ToString()).Child("rotation").Child("z").Value.ToString())
                        );
                        Vector3 scale = new Vector3(
                            float.Parse(snapshot.Child(id.ToString()).Child("scale").Child("x").Value.ToString()),
                            float.Parse(snapshot.Child(id.ToString()).Child("scale").Child("y").Value.ToString()),
                            float.Parse(snapshot.Child(id.ToString()).Child("scale").Child("z").Value.ToString())
                        );
                        bool isFavorite = bool.Parse(snapshot.Child(id.ToString()).Child("favorite").Value.ToString());

                        // update parts
                        if (FavoriteListManager.Instance.favoriteDict.ContainsKey(id))
                        {
                            FavoriteListManager.Instance.favoriteDict[id].furnitureRotation = rotation;
                            FavoriteListManager.Instance.favoriteDict[id].furnitureScale = scale;
                            FavoriteListManager.Instance.favoriteDict[id].isFavorite = isFavorite;
                        }
                    }
                }
            }
        });
    }

    public void WriteInfoData(FurnitureInfo info)
    {
        Vector3 rotation = info.furnitureRotation;
        Vector3 scale = info.furnitureScale;
        bool isFavorite = info.isFavorite;
        m_Reference.Child("furnitures").Child(info.furnitureId.ToString()).Child("rotation").Child("x").SetValueAsync(rotation.x);
        m_Reference.Child("furnitures").Child(info.furnitureId.ToString()).Child("rotation").Child("y").SetValueAsync(rotation.y);
        m_Reference.Child("furnitures").Child(info.furnitureId.ToString()).Child("rotation").Child("z").SetValueAsync(rotation.z);

        m_Reference.Child("furnitures").Child(info.furnitureId.ToString()).Child("scale").Child("x").SetValueAsync(scale.x);
        m_Reference.Child("furnitures").Child(info.furnitureId.ToString()).Child("scale").Child("y").SetValueAsync(scale.y);
        m_Reference.Child("furnitures").Child(info.furnitureId.ToString()).Child("scale").Child("z").SetValueAsync(scale.z);

        m_Reference.Child("furnitures").Child(info.furnitureId.ToString()).Child("favorite").SetValueAsync(isFavorite);
    }

}
