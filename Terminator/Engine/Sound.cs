using System;
using System.Media;
using System.Windows.Forms;

namespace Terminator.Engine
{
    class Sound
    {
        private SoundPlayer soundPlayer;

        public Sound()
        {
            soundPlayer = new SoundPlayer();
        }

        public void play(string soundName, bool looping)
        {
            try
            {
                soundPlayer.SoundLocation = @"Sound\" + soundName;
                if (looping)
                    soundPlayer.PlayLooping();
                else
                    soundPlayer.Play();
            }
            catch (Exception)
            {
                MessageBox.Show("Falta el archivo " + soundName + ", reinstale Terminator para solucionar el problema. El programa se cerrara.", "Error en sonido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        public void stop()
        {
            soundPlayer.Stop();
        }
    }
}