using UnityEngine;
using LitMotion;
using LitMotion.Extensions;

namespace ZuyZuy.Workspace
{
    public abstract class UIPopup : MonoBehaviour
    {
        protected string m_popupName;
        protected object m_data;

        private GameObject _container;
        private CanvasGroup _canvasGroup;
        private MotionHandle _fadeHandle;
        private MotionHandle _scaleHandle;
        private MotionHandle _slideHandle;

        [SerializeField] protected PopupAppearanceAnim _appearanceAnim = PopupAppearanceAnim.Fade;
        [SerializeField] public float _animationDuration = 0.3f;
        [SerializeField] protected Vector2 _slideOffset = new Vector2(0, 100f);
        [SerializeField] protected Vector3 _scaleStart = new Vector3(0.8f, 0.8f, 1f);

        public string PopupName => m_popupName;

        protected virtual void Start()
        {
            _container = transform.GetChild(0).gameObject;
            _canvasGroup = _container.GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
                _canvasGroup = _container.AddComponent<CanvasGroup>();

            Init();
        }

        protected abstract void Init();

        public virtual void Show(object data = null)
        {
            m_data = data;
            _container.SetActive(true);
            CancelCurrentMotions();

            switch (_appearanceAnim)
            {
                case PopupAppearanceAnim.Fade:
                    PlayFadeAnimation(0f, 1f);
                    break;
                case PopupAppearanceAnim.Scale:
                    PlayScaleAnimation(_scaleStart, Vector3.one);
                    break;
                case PopupAppearanceAnim.SlideFromTop:
                    PlaySlideAnimation(new Vector2(0, _slideOffset.y), Vector2.zero);
                    break;
                case PopupAppearanceAnim.SlideFromBottom:
                    PlaySlideAnimation(new Vector2(0, -_slideOffset.y), Vector2.zero);
                    break;
                case PopupAppearanceAnim.SlideFromLeft:
                    PlaySlideAnimation(new Vector2(-_slideOffset.x, 0), Vector2.zero);
                    break;
                case PopupAppearanceAnim.SlideFromRight:
                    PlaySlideAnimation(new Vector2(_slideOffset.x, 0), Vector2.zero);
                    break;
                case PopupAppearanceAnim.Bounce:
                    PlayScaleAnimation(_scaleStart * 1.2f, Vector3.one);
                    PlayFadeAnimation(0f, 1f);
                    break;
            }

            OnShow();
        }

        public virtual void Hide()
        {
            CancelCurrentMotions();

            switch (_appearanceAnim)
            {
                case PopupAppearanceAnim.Fade:
                    PlayFadeAnimation(1f, 0f, () => _container.SetActive(false));
                    break;
                case PopupAppearanceAnim.Scale:
                    PlayScaleAnimation(Vector3.one, _scaleStart, () => _container.SetActive(false));
                    break;
                case PopupAppearanceAnim.SlideFromTop:
                    PlaySlideAnimation(Vector2.zero, new Vector2(0, _slideOffset.y), () => _container.SetActive(false));
                    break;
                case PopupAppearanceAnim.SlideFromBottom:
                    PlaySlideAnimation(Vector2.zero, new Vector2(0, -_slideOffset.y), () => _container.SetActive(false));
                    break;
                case PopupAppearanceAnim.SlideFromLeft:
                    PlaySlideAnimation(Vector2.zero, new Vector2(-_slideOffset.x, 0), () => _container.SetActive(false));
                    break;
                case PopupAppearanceAnim.SlideFromRight:
                    PlaySlideAnimation(Vector2.zero, new Vector2(_slideOffset.x, 0), () => _container.SetActive(false));
                    break;
                case PopupAppearanceAnim.Bounce:
                    PlayScaleAnimation(Vector3.one, _scaleStart * 1.2f);
                    PlayFadeAnimation(1f, 0f, () => _container.SetActive(false));
                    break;
            }

            OnHide();
        }

        protected virtual void PlayFadeAnimation(float from, float to, System.Action onComplete = null)
        {
            _fadeHandle = LMotion.Create(from, to, _animationDuration)
                .WithEase(Ease.OutQuad)
                .WithOnComplete(onComplete)
                .BindToAlpha(_canvasGroup)
                .AddTo(_container);
        }

        protected virtual void PlayScaleAnimation(Vector3 from, Vector3 to, System.Action onComplete = null)
        {
            _scaleHandle = LMotion.Create(from, to, _animationDuration)
                .WithEase(Ease.OutBack)
                .WithOnComplete(onComplete)
                .BindToLocalScale(_container.transform)
                .AddTo(_container);
        }

        protected virtual void PlaySlideAnimation(Vector2 from, Vector2 to, System.Action onComplete = null)
        {
            _slideHandle = LMotion.Create(from, to, _animationDuration)
                .WithEase(Ease.OutQuad)
                .WithOnComplete(onComplete)
                .BindToAnchoredPosition(_container.GetComponent<RectTransform>())
                .AddTo(_container);
        }

        protected virtual void CancelCurrentMotions()
        {
            _fadeHandle.TryCancel();
            _scaleHandle.TryCancel();
            _slideHandle.TryCancel();
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }

        protected virtual void OnHideClick()
        {
            Hide();
        }
    }
}