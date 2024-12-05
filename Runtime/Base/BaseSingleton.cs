using UnityEngine;

namespace Zuy.Workspace
{
    public class BaseSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        private static bool _isInitialized;

        protected virtual bool PersistAcrossScenes => true;

        public static T Instance
        {
            get
            {
                if (_isInitialized)
                {
                    return _instance;
                }

                _instance = Object.FindFirstObjectByType<T>();
                if ((Object)_instance != (Object)null && _instance.gameObject != null)
                {
                    Object.DontDestroyOnLoad(_instance.gameObject);
                }

                _isInitialized = true;
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if ((Object)_instance == (Object)null)
            {
                _instance = this as T;
                if (PersistAcrossScenes)
                {
                    Object.DontDestroyOnLoad(base.gameObject);
                }
            }
            else if (_instance != this)
            {
                Object.Destroy(base.gameObject);
            }
        }
    }
}