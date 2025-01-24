using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum eAssetType
{
    JsonData,
    Prefab,
    SO,
    Sprites,
    UI,
    Sound,
}

public enum eCategoryType
{
    None,
    Item,
    Npc,
    Stage,
    Character,
    Map,
    Model,
    Monster,
    Sound
}

public class ResourceManager : Singleton<ResourceManager>
{
    private Dictionary<string, object> assetPool = new Dictionary<string, object>();

    public T LoadAsset<T>(string key, eAssetType assetType, eCategoryType categoryType = eCategoryType.None)
    {
        T handle = default;

        var path = $"{assetType}{(categoryType == eCategoryType.None ? "" : $"/{categoryType}")}";

        if(!assetPool.ContainsKey(key + "_" + path))
        {
            var obj = Resources.Load($"{path}/{key}", typeof(T));

            if (obj == null) return default;

            assetPool.Add(key + "_" + path, obj);
        }

        handle = (T)assetPool[key + "_" + path];

        return handle;
    }

    public async Task<T> LoadAssetAsync<T>(string key, eAssetType assetType, eCategoryType categoryType = eCategoryType.None)
    {
        T handle = default;

        var path = $"{assetType}{(categoryType == eCategoryType.None ? "" : $"/{categoryType}")}";

        if (!assetPool.ContainsKey(key + "_" + path))
        {
            var operation = Resources.LoadAsync($"{path}/{key}", typeof(T));

            while (!operation.isDone)
            {
                Debug.Log($"{key} + 로딩중 ");
                await Task.Yield();
            }

            var obj = operation.asset;

            if (obj == null) return default;

            assetPool.Add(key + "_" + path, obj);
        }

        handle = (T)assetPool[key + "_" + path];

        return handle;
    }

}
