using UnityEngine;

namespace ZuyZuy.Workspace
{
    public abstract class BaseUIView : MonoBehaviour
    {
        protected virtual void Awake() { }
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
