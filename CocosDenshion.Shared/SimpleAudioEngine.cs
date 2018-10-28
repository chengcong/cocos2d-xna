using System;
using System.Collections.Generic;

namespace CocosDenshion
{
	public class SimpleAudioEngine
	{
		private static string s_szRootPath;

		private static ulong s_dwRootLen;

		private static string s_szFullPath;

		private static SimpleAudioEngine s_SharedEngine;

		private static Dictionary<uint, EffectPlayer> s_List;

		private static MusicPlayer s_Music;

		public SimpleAudioEngine()
		{
		}

		public static string _FullPath(string szPath)
		{
			return szPath;
		}

		public static uint _Hash(string key)
		{
			uint num = 0;
			char[] charArray = key.ToUpper().ToCharArray();
			for (int i = 0; i < key.Length; i++)
			{
				num = num * 16777619;
				num = num ^ charArray[i];
			}
			return num;
		}

		public void end()
		{
			SimpleAudioEngine.sharedMusic().Close();
			foreach (KeyValuePair<uint, EffectPlayer> keyValuePair in SimpleAudioEngine.sharedList())
			{
				keyValuePair.Value.Close();
			}
			SimpleAudioEngine.sharedList().Clear();
		}

		~SimpleAudioEngine()
		{
		}

		public float getBackgroundMusicVolume()
		{
			return SimpleAudioEngine.sharedMusic().Volume;
		}

		public float getEffectsVolume()
		{
			return EffectPlayer.Volume;
		}

		public bool isBackgroundMusicPlaying()
		{
			return SimpleAudioEngine.sharedMusic().IsPlaying();
		}

		public void pauseBackgroundMusic()
		{
			SimpleAudioEngine.sharedMusic().Pause();
		}

		public void playBackgroundMusic(string pszFilePath, bool bLoop)
		{
			if (pszFilePath == null)
			{
				return;
			}
			SimpleAudioEngine.sharedMusic().Open(SimpleAudioEngine._FullPath(pszFilePath), SimpleAudioEngine._Hash(pszFilePath));
			SimpleAudioEngine.sharedMusic().Play(bLoop);
		}

		public void playBackgroundMusic(string pszFilePath)
		{
			this.playBackgroundMusic(pszFilePath, false);
		}

		public uint playEffect(string pszFilePath, bool bLoop)
		{
			uint num = SimpleAudioEngine._Hash(pszFilePath);
			this.preloadEffect(pszFilePath);
			foreach (KeyValuePair<uint, EffectPlayer> keyValuePair in SimpleAudioEngine.sharedList())
			{
				if (num != keyValuePair.Key)
				{
					continue;
				}
				keyValuePair.Value.Play(bLoop);
			}
			return num;
		}

		public uint playEffect(string pszFilePath)
		{
			return this.playEffect(pszFilePath, false);
		}

		public void preloadBackgroundMusic(string pszFilePath)
		{
			SimpleAudioEngine.sharedMusic().Open(SimpleAudioEngine._FullPath(pszFilePath), SimpleAudioEngine._Hash(pszFilePath));
		}

		public void preloadEffect(string pszFilePath)
		{
			if (pszFilePath.Length <= 0)
			{
				return;
			}
			uint num = SimpleAudioEngine._Hash(pszFilePath);
			if (SimpleAudioEngine.sharedList().ContainsKey(num))
			{
				return;
			}
			EffectPlayer effectPlayer = new EffectPlayer();
			effectPlayer.Open(SimpleAudioEngine._FullPath(pszFilePath), num);
			SimpleAudioEngine.sharedList().Add(num, effectPlayer);
		}

		public void RestoreMediaState()
		{
			SimpleAudioEngine.sharedMusic().RestoreMediaState();
		}

		public void resumeBackgroundMusic()
		{
			SimpleAudioEngine.sharedMusic().Resume();
		}

		public void rewindBackgroundMusic()
		{
			SimpleAudioEngine.sharedMusic().Rewind();
		}

		public void SaveMediaState()
		{
			SimpleAudioEngine.sharedMusic().SaveMediaState();
		}

		public void setBackgroundMusicVolume(float volume)
		{
			SimpleAudioEngine.sharedMusic().Volume = volume;
		}

		public void setEffectsVolume(float volume)
		{
			EffectPlayer.Volume = volume;
		}

		public static void setResource(string pszZipFileName)
		{
		}

		public static SimpleAudioEngine sharedEngine()
		{
			if (SimpleAudioEngine.s_SharedEngine == null)
			{
				SimpleAudioEngine.s_SharedEngine = new SimpleAudioEngine();
			}
			return SimpleAudioEngine.s_SharedEngine;
		}

		public static Dictionary<uint, EffectPlayer> sharedList()
		{
			if (SimpleAudioEngine.s_List == null)
			{
				SimpleAudioEngine.s_List = new Dictionary<uint, EffectPlayer>();
			}
			return SimpleAudioEngine.s_List;
		}

		private static MusicPlayer sharedMusic()
		{
			if (SimpleAudioEngine.s_Music == null)
			{
				SimpleAudioEngine.s_Music = new MusicPlayer();
			}
			return SimpleAudioEngine.s_Music;
		}

		public void stopBackgroundMusic(bool bReleaseData)
		{
			if (bReleaseData)
			{
				SimpleAudioEngine.sharedMusic().Close();
				return;
			}
			SimpleAudioEngine.sharedMusic().Stop();
		}

		public void stopBackgroundMusic()
		{
			this.stopBackgroundMusic(false);
		}

		public void stopEffect(uint nSoundId)
		{
			foreach (KeyValuePair<uint, EffectPlayer> keyValuePair in SimpleAudioEngine.sharedList())
			{
				if (nSoundId != keyValuePair.Key)
				{
					continue;
				}
				keyValuePair.Value.Stop();
			}
		}

		public void unloadEffect(string pszFilePath)
		{
			uint num = SimpleAudioEngine._Hash(pszFilePath);
			if (SimpleAudioEngine.sharedList().ContainsKey(num))
			{
				SimpleAudioEngine.sharedList().Remove(num);
			}
		}

		public bool willPlayBackgroundMusic()
		{
			return false;
		}
	}
}