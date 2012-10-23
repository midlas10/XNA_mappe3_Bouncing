using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Bouncing
{
    class AudioManager
    {
        AudioEngine engine;
        SoundBank sound;
        WaveBank wave;
        Cue trackCue;

        string songTitle;

        public AudioManager(string title)
        {
            songTitle = title;
        }

        public void PlayCue(string cueName)
        {
            sound.PlayCue(cueName);
        }


        internal void LoadContent()
        {
            
            
            //Loads the audioengine, wavebank and soundbank
            engine = new AudioEngine(@"Content/Audio/GameAudio.xgs");
            sound = new SoundBank(engine, @"Content/Audio/Sound Bank.xsb");
            wave = new WaveBank(engine, @"Content/Audio/Wave Bank.xwb");

            trackCue = sound.GetCue(songTitle);
            trackCue.Play();
        }

    }
}
