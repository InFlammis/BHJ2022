using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "New VideoContainer", menuName = "Game/Media/Video Container SO")]
public class VideoContainerSO : ScriptableObject
{
    #region Inspector
    #if !UNITY_EDITOR_LINUX || !UNITY_STANDALONE_LINUX
    [Header("Video Intros MP4")]
    [SerializeField]
    private VideoClip logoAnimationMP4;
    [SerializeField]
    private VideoClip logoAnimationWhiteMP4;
    #endif

    [Header("Video Intros VP8")]
    [SerializeField]
    private VideoClip logoAnimationWEBM;
    [SerializeField]
    private VideoClip logoAnimationWhiteWEBM;
    #endregion

    #region Variables
    #endregion

    #region Properties
    #if !UNITY_EDITOR_LINUX || !UNITY_STANDALONE_LINUX
    public VideoClip LogoAnimationMP4 { get => logoAnimationMP4; }
    public VideoClip LogoAnimationWhiteMP4 { get => logoAnimationWhiteMP4; }
    #endif
    public VideoClip LogoAnimationWEBM { get => logoAnimationWEBM; }
    public VideoClip LogoAnimationWhiteWEBM { get => logoAnimationWhiteWEBM; }
    #endregion
}
