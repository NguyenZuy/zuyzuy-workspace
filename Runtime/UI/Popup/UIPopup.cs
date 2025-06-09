using UnityEngine;
using LitMotion;
using LitMotion.Extensions;

namespace ZuyZuy.Workspace
{
    public abstract class UIPopup : MonoBehaviour
    {
        protected string m_PopupName;
        protected object m_Data;

        protected GameObject m_Container;
        protected CanvasGroup m_CanvasGroup;
        protected MotionHandle m_FadeHandle;
        protected MotionHandle m_ScaleHandle;
        protected MotionHandle m_SlideHandle;

        [SerializeField] protected PopupAppearanceAnim m_AppearanceAnim = PopupAppearanceAnim.Fade;
        [SerializeField] protected float m_AnimationDuration = 0.3f;
        [SerializeField] protected Vector2 m_SlideOffset = new Vector2(0, 100f);
        [SerializeField] protected Vector3 m_ScaleStart = new Vector3(0.8f, 0.8f, 1f);

        public string PopupName => m_PopupName;
        public float AnimationDuration => m_AnimationDuration;

        protected virtual void Start()
        {
            m_Container = transform.GetChild(0).gameObject;
            m_CanvasGroup = m_Container.GetComponent<CanvasGroup>();
            if (m_CanvasGroup == null)
                m_CanvasGroup = m_Container.AddComponent<CanvasGroup>();

            Init();
        }

        protected abstract void Init();

        public virtual void Show(object data = null)
        {
            m_Data = data;
            m_Container.SetActive(true);
            CancelCurrentMotions();

            switch (m_AppearanceAnim)
            {
                case PopupAppearanceAnim.Fade:
                    PlayFadeAnimation(0f, 1f);
                    break;
                case PopupAppearanceAnim.Scale:
                    PlayScaleAnimation(m_ScaleStart, Vector3.one);
                    break;
                case PopupAppearanceAnim.SlideFromTop:
                    PlaySlideAnimation(new Vector2(0, m_SlideOffset.y), Vector2.zero);
                    break;
                case PopupAppearanceAnim.SlideFromBottom:
                    PlaySlideAnimation(new Vector2(0, -m_SlideOffset.y), Vector2.zero);
                    break;
                case PopupAppearanceAnim.SlideFromLeft:
                    PlaySlideAnimation(new Vector2(-m_SlideOffset.x, 0), Vector2.zero);
                    break;
                case PopupAppearanceAnim.SlideFromRight:
                    PlaySlideAnimation(new Vector2(m_SlideOffset.x, 0), Vector2.zero);
                    break;
                case PopupAppearanceAnim.Bounce:
                    PlayScaleAnimation(m_ScaleStart * 1.2f, Vector3.one);
                    PlayFadeAnimation(0f, 1f);
                    break;
            }

            OnShow();
        }

        public virtual void Hide()
        {
            CancelCurrentMotions();

            switch (m_AppearanceAnim)
            {
                case PopupAppearanceAnim.Fade:
                    PlayFadeAnimation(1f, 0f, () => m_Container.SetActive(false));
                    break;
                case PopupAppearanceAnim.Scale:
                    PlayScaleAnimation(Vector3.one, m_ScaleStart, () => m_Container.SetActive(false));
                    break;
                case PopupAppearanceAnim.SlideFromTop:
                    PlaySlideAnimation(Vector2.zero, new Vector2(0, m_SlideOffset.y), () => m_Container.SetActive(false));
                    break;
                case PopupAppearanceAnim.SlideFromBottom:
                    PlaySlideAnimation(Vector2.zero, new Vector2(0, -m_SlideOffset.y), () => m_Container.SetActive(false));
                    break;
                case PopupAppearanceAnim.SlideFromLeft:
                    PlaySlideAnimation(Vector2.zero, new Vector2(-m_SlideOffset.x, 0), () => m_Container.SetActive(false));
                    break;
                case PopupAppearanceAnim.SlideFromRight:
                    PlaySlideAnimation(Vector2.zero, new Vector2(m_SlideOffset.x, 0), () => m_Container.SetActive(false));
                    break;
                case PopupAppearanceAnim.Bounce:
                    PlayScaleAnimation(Vector3.one, m_ScaleStart * 1.2f);
                    PlayFadeAnimation(1f, 0f, () => m_Container.SetActive(false));
                    break;
            }

            OnHide();
        }

        protected virtual void PlayFadeAnimation(float from, float to, System.Action onComplete = null)
        {
            m_FadeHandle = LMotion.Create(from, to, m_AnimationDuration)
                .WithEase(Ease.OutQuad)
                .WithOnComplete(onComplete)
                .BindToAlpha(m_CanvasGroup)
                .AddTo(m_Container);
        }

        protected virtual void PlayScaleAnimation(Vector3 from, Vector3 to, System.Action onComplete = null)
        {
            m_ScaleHandle = LMotion.Create(from, to, m_AnimationDuration)
                .WithEase(Ease.OutBack)
                .WithOnComplete(onComplete)
                .BindToLocalScale(m_Container.transform)
                .AddTo(m_Container);
        }

        protected virtual void PlaySlideAnimation(Vector2 from, Vector2 to, System.Action onComplete = null)
        {
            m_SlideHandle = LMotion.Create(from, to, m_AnimationDuration)
                .WithEase(Ease.OutQuad)
                .WithOnComplete(onComplete)
                .BindToAnchoredPosition(m_Container.GetComponent<RectTransform>())
                .AddTo(m_Container);
        }

        protected virtual void CancelCurrentMotions()
        {
            m_FadeHandle.TryCancel();
            m_ScaleHandle.TryCancel();
            m_SlideHandle.TryCancel();
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