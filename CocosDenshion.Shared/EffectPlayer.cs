using cocos2d;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;

namespace CocosDenshion
{
	public class EffectPlayer
	{
		public static ulong s_mciError;

		private uint m_nSoundID;

		private SoundEffect m_effect;

		public static float Volume
		{
			get
			{
				return SoundEffect.MasterVolume;
			}
			set
			{
				if (value >= 0f && value <= 1f)
				{
					SoundEffect.MasterVolume = value;
				}
			}
		}

		public EffectPlayer()
		{
			this.m_nSoundID = 0;
		}

		public void Close()
		{
			this.Stop();
			this.m_effect = null;
		}

		~EffectPlayer()
		{
			this.Close();
		}

		public uint GetSoundID()
		{
			return this.m_nSoundID;
		}

		public bool IsPlaying()
		{
			CCLog.Log("IsPlaying is invalid for sound effect");
			return false;
		}

		public void Open(string pFileName, uint uId)
		{
			if (pFileName != null)
			{
				if (pFileName.Length == 0)
				{
					return;
				}
				this.Close();
				this.m_effect = CCApplication.sharedApplication().content.Load<SoundEffect>(pFileName);
				this.m_nSoundID = uId;
			}
		}

		public void Pause()
		{
			CCLog.Log("Pause is invalid for sound effect");
		}

		public void Play(bool bLoop)
		{
			if (this.m_effect == null)
			{
				return;
			}
			this.m_effect.Play();
		}

		public void Play()
		{
			this.Play(false);
		}

		public void Resume()
		{
			CCLog.Log("Resume is invalid for sound effect");
		}

		public void Rewind()
		{
			CCLog.Log("Rewind is invalid for sound effect");
		}

		public void Stop()
		{
			CCLog.Log("Stop is invalid for sound effect");
		}
	}
}