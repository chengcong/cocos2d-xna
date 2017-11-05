namespace cocos2d
{
    using System;
    using System.Collections.Generic;

    public class CCAnimationCache : CCObject
    {
        private Dictionary<string, CCAnimation> m_pAnimations;
        private static CCAnimationCache s_pSharedAnimationCache;

        public void addAnimation(CCAnimation animation, string name)
        {
            this.m_pAnimations.Add(name, animation);
        }

        public CCAnimation animationByName(string name)
        {
            CCAnimation animation = new CCAnimation();
            if (this.m_pAnimations.TryGetValue(name, out animation))
            {
                return animation;
            }
            return null;
        }

        ~CCAnimationCache()
        {
        }

        public bool init()
        {
            this.m_pAnimations = new Dictionary<string, CCAnimation>();
            return true;
        }

        public static void purgeSharedAnimationCache()
        {
            s_pSharedAnimationCache = null;
        }

        public void removeAnimationByName(string name)
        {
            if (name != null)
            {
                this.m_pAnimations.Remove(name);
            }
        }

        public static CCAnimationCache sharedAnimationCache()
        {
            if (s_pSharedAnimationCache == null)
            {
                s_pSharedAnimationCache = new CCAnimationCache();
                s_pSharedAnimationCache.init();
            }
            return s_pSharedAnimationCache;
        }
    }
}
