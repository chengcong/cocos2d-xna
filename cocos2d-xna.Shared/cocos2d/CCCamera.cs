using Microsoft.Xna.Framework;
using System;

namespace cocos2d
{
	public class CCCamera : CCObject
	{
		protected float m_fEyeX;

		protected float m_fEyeY;

		protected float m_fEyeZ;

		protected float m_fCenterX;

		protected float m_fCenterY;

		protected float m_fCenterZ;

		protected float m_fUpX;

		protected float m_fUpY;

		protected float m_fUpZ;

		protected bool m_bDirty;

		public bool Dirty
		{
			get
			{
				return this.m_bDirty;
			}
			set
			{
				this.m_bDirty = value;
			}
		}

		public CCCamera()
		{
			this.init();
		}

		public string description()
		{
			return string.Format("<CCCamera | center = ({0},{1},{2})>", this.m_fCenterX, this.m_fCenterY, this.m_fCenterZ);
		}

		public void getCenterXYZ(out float pCenterX, out float pCenterY, out float pCenterZ)
		{
			pCenterX = this.m_fCenterX / CCDirector.sharedDirector().ContentScaleFactor;
			pCenterY = this.m_fCenterY / CCDirector.sharedDirector().ContentScaleFactor;
			pCenterZ = this.m_fCenterZ / CCDirector.sharedDirector().ContentScaleFactor;
		}

		public void getEyeXYZ(out float pEyeX, out float pEyeY, out float pEyeZ)
		{
			pEyeX = this.m_fEyeX / CCDirector.sharedDirector().ContentScaleFactor;
			pEyeY = this.m_fEyeY / CCDirector.sharedDirector().ContentScaleFactor;
			pEyeZ = this.m_fEyeZ / CCDirector.sharedDirector().ContentScaleFactor;
		}

		public void getUpXYZ(out float pUpX, out float pUpY, out float pUpZ)
		{
			pUpX = this.m_fUpX;
			pUpY = this.m_fUpY;
			pUpZ = this.m_fUpZ;
		}

		public static float getZEye()
		{
			return 1.1920929E-07f;
		}

		public void init()
		{
			this.restore();
		}

		public Matrix? locate()
		{
			if (!this.m_bDirty)
			{
				return null;
			}
			return new Matrix?(Matrix.CreateLookAt(new Vector3(this.m_fEyeX, this.m_fEyeY, this.m_fEyeZ), new Vector3(this.m_fCenterX, this.m_fCenterY, this.m_fCenterZ), new Vector3(this.m_fUpX, this.m_fUpY, this.m_fUpZ)));
		}

		public void restore()
		{
			float single = 0f;
			float single1 = single;
			this.m_fEyeY = single;
			this.m_fEyeX = single1;
			this.m_fEyeZ = CCCamera.getZEye();
			float single2 = 0f;
			float single3 = single2;
			this.m_fCenterZ = single2;
			float single4 = single3;
			float single5 = single4;
			this.m_fCenterY = single4;
			this.m_fCenterX = single5;
			this.m_fUpX = 0f;
			this.m_fUpY = 1f;
			this.m_fUpZ = 0f;
			this.m_bDirty = false;
		}

		public void setCenterXYZ(float fCenterX, float fCenterY, float fCenterZ)
		{
			this.m_fCenterX = fCenterX * CCDirector.sharedDirector().ContentScaleFactor;
			this.m_fCenterY = fCenterY * CCDirector.sharedDirector().ContentScaleFactor;
			this.m_fCenterZ = fCenterZ * CCDirector.sharedDirector().ContentScaleFactor;
			this.m_bDirty = true;
		}

		public void setEyeXYZ(float fEyeX, float fEyeY, float fEyeZ)
		{
			this.m_fEyeX = fEyeX * CCDirector.sharedDirector().ContentScaleFactor;
			this.m_fEyeY = fEyeY * CCDirector.sharedDirector().ContentScaleFactor;
			this.m_fEyeZ = fEyeZ * CCDirector.sharedDirector().ContentScaleFactor;
			this.m_bDirty = true;
		}

		public void setUpXYZ(float fUpX, float fUpY, float fUpZ)
		{
			this.m_fUpX = fUpX;
			this.m_fUpY = fUpY;
			this.m_fUpZ = fUpZ;
			this.m_bDirty = true;
		}
	}
}