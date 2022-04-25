using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus.PreRoll
{
    public class PreRollManager : MenuManager, IPreRollManager
    {

        private VideoPlayer _videoPlayer;
        private SpriteRenderer _foreground;

        void Awake()
        {
            OnAwake();
        }

        void Start()
        {
            OnStart();
        }

        public void OnAwake()
        {
            _foreground = FindObjectsOfType<SpriteRenderer>().Single(x => x.name == "Foreground");

            _videoPlayer = FindObjectOfType<VideoPlayer>();
            _videoPlayer.loopPointReached += VideoPlayer_loopPointReached;
        }

        private void VideoPlayer_loopPointReached(VideoPlayer source)
        {
            StaticObjects.Messenger.PublishPreRollFinished(this, null);
        }

        public void OnStart()
        {
            this.StartCoroutine(CheckPositionInVideo());
            _videoPlayer?.Play();
        }

        IEnumerator CheckPositionInVideo()
        {
            var videoFrameCount = Convert.ToInt64(_videoPlayer.frameCount);
            var videoCurrentFrame = _videoPlayer.frame;
            var startFadingFactor = 0.8f;
            var startFadingFrame = (long)(videoFrameCount * startFadingFactor);
            Debug.Log($"FrameCount: {videoFrameCount}; StartFadingFrame: {startFadingFrame}");

            while (videoCurrentFrame < videoFrameCount)
            {
                Debug.Log($"CurrentFrame: {videoCurrentFrame}");
                if(videoCurrentFrame >= startFadingFrame)
                {
                    var factor = Mathf.InverseLerp(startFadingFrame, videoFrameCount, videoCurrentFrame);
                    Debug.Log($"Factor: {factor}");
                    var color = new Color(_foreground.color.r, _foreground.color.g, _foreground.color.b, factor * factor);
                    Debug.Log($"Color: {color}");
                    _foreground.color = color;
                }
                yield return new WaitForFixedUpdate();
                videoCurrentFrame = _videoPlayer.frame;
            }

            yield break;
        }
    }
}
