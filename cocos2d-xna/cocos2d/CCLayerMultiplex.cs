namespace cocos2d
{
    using System;
    using System.Collections.Generic;

    public class CCLayerMultiplex : CCLayer
    {
        protected uint m_nEnabledLayer;
        protected List<CCLayer> m_pLayers;

        public virtual void addLayer(CCLayer layer)
        {
            this.m_pLayers.Add(layer);
        }

        public virtual bool initWithLayer(CCLayer layer)
        {
            this.m_pLayers = new List<CCLayer>(1);
            this.m_pLayers.Add(layer);
            this.m_nEnabledLayer = 0;
            this.addChild(layer);
            return true;
        }

        public virtual bool initWithLayers(params CCLayer[] layer)
        {
            this.m_pLayers = new List<CCLayer>(5);
            this.m_pLayers.AddRange(layer);
            this.m_nEnabledLayer = 0;
            this.addChild(this.m_pLayers[(int)this.m_nEnabledLayer]);
            return true;
        }

        public static CCLayerMultiplex layerWithLayer(CCLayer layer)
        {
            CCLayerMultiplex multiplex = new CCLayerMultiplex();
            multiplex.initWithLayer(layer);
            return multiplex;
        }

        public static CCLayerMultiplex layerWithLayers(params CCLayer[] layer)
        {
            CCLayerMultiplex multiplex = new CCLayerMultiplex();
            if ((multiplex != null) && multiplex.initWithLayers(layer))
            {
                return multiplex;
            }
            multiplex = null;
            return null;
        }

        public virtual void switchTo(uint n)
        {
            if (n >= this.m_pLayers.Count)
            {
                CCLog.Log("Invalid index in MultiplexLayer switchTo message");
            }
            else
            {
                this.removeChild(this.m_pLayers[(int)this.m_nEnabledLayer], false);
                this.m_nEnabledLayer = n;
                this.addChild(this.m_pLayers[(int)n]);
            }
        }

        public virtual void switchToAndReleaseMe(uint n)
        {
            if (n >= this.m_pLayers.Count)
            {
                CCLog.Log("Invalid index in MultiplexLayer switchTo message");
            }
            else
            {
                this.removeChild(this.m_pLayers[(int)this.m_nEnabledLayer], true);
                this.m_pLayers[(int)this.m_nEnabledLayer] = null;
                this.m_nEnabledLayer = n;
                this.addChild(this.m_pLayers[(int)n]);
            }
        }
    }
}
