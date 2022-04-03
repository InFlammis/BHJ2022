using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.SoundManagement
{
    /// <summary>
    /// Class responsible of managing the sound and music execution
    /// </summary>
    public interface ISoundManager : IMyMonoBehaviour
    {
        public StaticObjectsSO StaticObjects { get; }

        ///// <summary>
        ///// Sound clip of the menu music to play
        ///// </summary>
        //AudioClip MenuMusic { get; }

        ///// <summary>
        ///// Sound clip of the game music to play
        ///// </summary>
        //AudioClip GameMusic { get; }

        ///// <summary>
        ///// Play an audioclip for music
        ///// </summary>
        ///// <param name="audioClip">Audio clip to play</param>
        //void PlayMusic(AudioClip audioClip);

        ///// <summary>
        ///// Play a sound
        ///// </summary>
        ///// <param name="sound">Sound to play</param>
        //void PlaySound(Sound sound);
    }
}
