namespace Zuy.Workspace
{
    public abstract class BaseGameManager : BaseSingleton<BaseGameManager>
    {
        protected override void Awake() { }
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
