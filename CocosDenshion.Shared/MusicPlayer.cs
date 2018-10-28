using cocos2d;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;

namespace CocosDenshion
{
	public class MusicPlayer
	{
		public static ulong s_mciError;

		private uint m_nSoundID;

		private Song m_music;

		private bool m_didPlayGameSong;

		private Song m_SongToPlayAfterClose;

		private float m_VolumeAfterClose = 1f;

		private TimeSpan m_PlayPositionAfterClose = TimeSpan.Zero;

		private MediaQueue m_QueueAfterClose;

		private bool m_IsRepeatingAfterClose;

		private bool m_IsShuffleAfterClose;

		public float Volume
		{
			get
			{
				return Microsoft.Xna.Framework.Media.MediaPlayer.Volume;
			}
			set
			{
				if (value >= 0f && value <= 1f)
				{
					Microsoft.Xna.Framework.Media.MediaPlayer.Volume = value;
				}
			}
		}

		public MusicPlayer()
		{
			this.m_nSoundID = 0;
			if (Microsoft.Xna.Framework.Media.MediaPlayer.State == MediaState.Playing)
			{
				this.SaveMediaState();
			}
		}

		public void Close()
		{
			if (this.IsPlaying() && this.m_didPlayGameSong)
			{
				this.Stop();
			}
			if (this.m_music != null)
			{
				this.m_music = null;
			}
		}

		~MusicPlayer()
		{
			this.Close();
			this.RestoreMediaState();
		}

		public uint GetSoundID()
		{
			return this.m_nSoundID;
		}

		public bool IsPlaying()
		{
			if (MediaState.Playing == Microsoft.Xna.Framework.Media.MediaPlayer.State)
			{
				return true;
			}
			return false;
		}

		public bool IsPlayingMySong()
		{
			if (!this.m_didPlayGameSong)
			{
				return false;
			}
			if (MediaState.Playing == Microsoft.Xna.Framework.Media.MediaPlayer.State)
			{
				return true;
			}
			return false;
		}

		public void Open(string pFileName, uint uId)
		{
			if (pFileName == null || pFileName.Length == 0)
			{
				return;
			}
			this.Close();
			this.m_music = CCApplication.sharedApplication().content.Load<Song>(pFileName);
			this.m_nSoundID = uId;
		}

		public void Pause()
		{
			Microsoft.Xna.Framework.Media.MediaPlayer.Pause();
		}

		public void Play(bool bLoop)
		{
			if (null != this.m_music)
			{
				Microsoft.Xna.Framework.Media.MediaPlayer.IsRepeating = bLoop;
				Microsoft.Xna.Framework.Media.MediaPlayer.Play(this.m_music);
				this.m_didPlayGameSong = true;
			}
		}

		public void Play()
		{
			this.Play(false);
		}

		public void RestoreMediaState()
		{
			if (this.m_SongToPlayAfterClose != null && this.m_didPlayGameSong)
			{
				Microsoft.Xna.Framework.Media.MediaPlayer.IsShuffled = this.m_IsShuffleAfterClose;
				Microsoft.Xna.Framework.Media.MediaPlayer.IsRepeating = this.m_IsRepeatingAfterClose;
				Microsoft.Xna.Framework.Media.MediaPlayer.Volume = this.m_VolumeAfterClose;
				Microsoft.Xna.Framework.Media.MediaPlayer.Play(this.m_SongToPlayAfterClose);
			}
		}

		public void Resume()
		{
			Microsoft.Xna.Framework.Media.MediaPlayer.Resume();
		}

		public void Rewind()
		{
			Song activeSong = Microsoft.Xna.Framework.Media.MediaPlayer.Queue.ActiveSong;
			this.Stop();
			if (null != this.m_music)
			{
				Microsoft.Xna.Framework.Media.MediaPlayer.Play(this.m_music);
				return;
			}
			if (activeSong != null)
			{
				Microsoft.Xna.Framework.Media.MediaPlayer.Play(activeSong);
			}
		}

		public void SaveMediaState()
		{
			this.m_SongToPlayAfterClose = Microsoft.Xna.Framework.Media.MediaPlayer.Queue.ActiveSong;
			this.m_VolumeAfterClose = Microsoft.Xna.Framework.Media.MediaPlayer.Volume;
			this.m_PlayPositionAfterClose = Microsoft.Xna.Framework.Media.MediaPlayer.PlayPosition;
			this.m_IsRepeatingAfterClose = Microsoft.Xna.Framework.Media.MediaPlayer.IsRepeating;
			this.m_IsShuffleAfterClose = Microsoft.Xna.Framework.Media.MediaPlayer.IsShuffled;
		}

		public void Stop()
		{
			Microsoft.Xna.Framework.Media.MediaPlayer.Stop();
		}
	}
}