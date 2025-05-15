using UnityEngine;
using LitMotion;
using LitMotion.Extensions;

namespace ZuyZuy.Workspace
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIPopup : MonoBehaviour
    {
        [SerializeField] protected UIPopupName _popupName;

        private CanvasGroup _canvasGroup;
        private MotionHandle _fadeHandle;

        public UIPopupName PopupName => _popupName;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _fadeHandle.Cancel();
            _fadeHandle = LMotion.Create(0f, 1f, 0.3f)
                .WithEase(Ease.OutQuad)
                .BindToAlpha(_canvasGroup)
                .AddTo(gameObject);

            OnShow();
        }

        public void Hide()
        {
            _fadeHandle.Cancel();
            _fadeHandle = LMotion.Create(1f, 0f, 0.3f)
                .WithEase(Ease.InQuad)
                .WithOnComplete(() => gameObject.SetActive(false))
                .BindToAlpha(_canvasGroup)
                .AddTo(gameObject);

            OnHide();
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }
    }
}