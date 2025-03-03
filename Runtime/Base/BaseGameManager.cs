namespace ZuyZuy.Workspace
{
    public abstract class BaseGameManager<T> : BaseSingleton<T> where T : BaseGameManager<T>
    {
        protected override void Awake()
        {
            base.Awake();
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
