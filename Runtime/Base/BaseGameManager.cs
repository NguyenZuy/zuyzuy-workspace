namespace com.zuyzuy.workspace
{
    public abstract class BaseGameManager : BaseSingleton<BaseGameManager>
    {
        protected override void Awake() { }
        protected abstract void Start();
        protected abstract void Update();
        protected abstract void FixedUpdate();
        protected abstract void LateUpdate();
        protected abstract void OnEnable();
        protected abstract void OnDisable();
        protected abstract void OnDestroy();
        protected abstract void OnApplicationQuit();
    }
}
