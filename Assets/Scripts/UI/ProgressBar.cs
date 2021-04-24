using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChatFight
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class ProgressBar : MonoBehaviour
    {
        public enum ProgressStyle
        {
            Rect,
            FillImage,
        }

        public enum FillStyle
        {
            Horizontal,
            Vertical
        }

        public TMP_Text Text { get { return text; } }

        [SerializeField] private ProgressStyle progressStyle = ProgressStyle.Rect;
        [SerializeField] private FillStyle fillStyle = FillStyle.Horizontal;
        [SerializeField] private RectTransform fillRect = null;
        [SerializeField] private Image fillImage = null;
        [SerializeField] private TMP_Text text = null;
        [SerializeField] [Range(0.0f, 1.0f)] private float progress = 0.0f;
        [SerializeField] private bool update = false;

        private Tweener tween = null;

#if UNITY_EDITOR
        private float lastProgress = 0.0f;
#endif

        private void Awake()
        {
            SetProgress(progress);
        }

        private void OnDestroy()
        {
            StopTween();
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (update == true)
            {
                update = false;
                SetProgress(progress);
            }
        }

        private void OnGUI()
        {
            if (lastProgress != progress && Application.isPlaying == false)
            {
                lastProgress = progress;
                update = true;
            }
        }
#endif

        public void SetFillColor(Color color)
        {
            if (progressStyle == ProgressStyle.FillImage)
            {
                fillImage.color = color;
            }
            else if (progressStyle == ProgressStyle.Rect)
            {
                var image = fillRect.GetComponent<Image>();
                if (image != null)
                {
                    image.color = color;
                }
            }
        }

        public void StopTween()
        {
            tween.Stop();
        }

        public void TweenToProgress(float endProgress, float duration, float delay = 0.0f, TweenCallback callback = null)
        {
            TweenToProgress(progress, endProgress, duration, delay, callback);
        }

        public void TweenToProgress(float startProgress, float endProgress, float duration, float delay = 0.0f, TweenCallback callback = null)
        {
            tween.Stop();
            tween = DOTween.To((x) => SetProgress(Mathf.Clamp01(x)), startProgress, endProgress, duration).SetDelay(delay).SetEase(Ease.OutCubic).OnComplete(callback);
        }

        public void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            UpdateFillProgress();
        }

        public float GetProgress()
        {
            return progress;
        }

        private void UpdateFillProgress()
        {
            switch (progressStyle)
            {
                case ProgressStyle.Rect:
                {
                    UpdateFillRect();
                    break;
                }
                case ProgressStyle.FillImage:
                {
                    UpdateFillImage();
                    break;
                }
            }
        }

        private void UpdateFillRect()
        {
            Vector2 newAnchor = fillRect.anchorMax;
            switch (fillStyle)
            {
                case FillStyle.Horizontal:
                {
                    newAnchor.x = progress;
                    break;
                }
                case FillStyle.Vertical:
                {
                    newAnchor.y = progress;
                    break;
                }
            }
            fillRect.anchorMax = newAnchor;
        }

        private void UpdateFillImage()
        {
            fillImage.fillAmount = progress;
        }
    }
}