using UnityEngine;

namespace Zuy.Workspace
{
    public abstract class BaseUIView<T> : MonoBehaviour where T : BaseGameManager<T>
    {
        protected T m_GameManager;

        protected virtual void Awake()
        {
            m_GameManager = BaseGameManager<T>.Instance;
        }

        protected virtual void Start() { }

        protected virtual void Update() { }

        protected virtual void FixedUpdate() { }

        protected virtual void LateUpdate() { }

        protected virtual void OnEnable() { }

        protected virtual void OnDisable() { }

        protected virtual void OnDestroy() { }

        protected virtual void OnApplicationQuit() { }
    }
}
