using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null) Create();
            return _instance;
        }
    }

    protected static void Create()
    {
        if (_instance == null)
        {
            T[] objects = FindObjectsOfType<T>(true);
            if (objects.Length > 0)
            {
                _instance = objects[0];

                for (int i = 1; i < objects.Length; i++)
                {
                    if (Application.isPlaying)
                        Destroy(objects[i].gameObject);
                    else
                        DestroyImmediate(objects[i].gameObject);

                }
            }
            else
            {
                GameObject singletonObject = new GameObject(string.Format("{0}", typeof(T).Name));
                _instance = singletonObject.AddComponent<T>();
            }
        }
    }

    protected virtual void Awake()
    {
        Create();
        if (_instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    protected virtual void OnEnable() { }
    protected virtual void Start() { }
    protected virtual void Update() { }
    protected virtual void FixedUpdate() { }
    protected virtual void LateUpdate() { }
    protected virtual void OnDisable() { }
}

