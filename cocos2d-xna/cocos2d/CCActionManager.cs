namespace cocos2d
{
    using System;
    using System.Collections.Generic;

    public class CCActionManager : CCObject, SelectorProtocol
    {
        private static CCActionManager g_sharedActionManager;
        protected bool m_bCurrentTargetSalvaged;
        protected tHashElement m_pCurrentTarget;
        protected Dictionary<CCObject, tHashElement> m_pTargets;

        private CCActionManager()
        {
        }

        protected void actionAllocWithHashElement(tHashElement element)
        {
            if (element.actions == null)
            {
                element.actions = new List<CCAction>();
            }
        }

        public void addAction(CCAction action, CCNode target, bool paused)
        {
            tHashElement element = null;
            if (!this.m_pTargets.ContainsKey(target))
            {
                element = new tHashElement
                {
                    paused = paused,
                    target = target
                };
                this.m_pTargets.Add(target, element);
            }
            else
            {
                element = this.m_pTargets[target];
            }
            this.actionAllocWithHashElement(element);
            element.actions.Add(action);
            action.startWithTarget(target);
        }

        protected void deleteHashElement(tHashElement element)
        {
            element.actions.Clear();
            element.target = null;
            CCObject key = null;
            foreach (KeyValuePair<CCObject, tHashElement> pair in this.m_pTargets)
            {
                if (element == pair.Value)
                {
                    key = pair.Key;
                    break;
                }
            }
            if (key != null)
            {
                this.m_pTargets.Remove(key);
            }
        }

        ~CCActionManager()
        {
            this.removeAllActions();
            this.m_pTargets.Clear();
            g_sharedActionManager = null;
        }

        public CCAction getActionByTag(uint tag, CCObject target)
        {
            if (this.m_pTargets.ContainsKey(target))
            {
                tHashElement element = this.m_pTargets[target];
                if (element.actions != null)
                {
                    int count = element.actions.Count;
                    for (int i = 0; i < count; i++)
                    {
                        CCAction action = element.actions[i];
                        if (action.tag == tag)
                        {
                            return action;
                        }
                    }
                }
                else
                {
                    CCLog.Log("cocos2d : getActionByTag: Target not found");
                }
            }
            else
            {
                CCLog.Log("cocos2d : getActionByTag: Target not found");
            }
            return null;
        }

        public bool init()
        {
            this.m_pTargets = new Dictionary<CCObject, tHashElement>();
            CCScheduler.sharedScheduler().scheduleUpdateForTarget(this, 0, false);
            return true;
        }

        public uint numberOfRunningActionsInTarget(CCObject target)
        {
            if (!this.m_pTargets.ContainsKey(target))
            {
                return 0;
            }
            tHashElement element = this.m_pTargets[target];
            if (element.actions == null)
            {
                return 0;
            }
            return (uint)element.actions.Count;
        }

        public void pauseTarget(CCObject target)
        {
            if (this.m_pTargets.ContainsKey(target))
            {
                this.m_pTargets[target].paused = true;
            }
        }

        public void purgeSharedManager()
        {
            CCScheduler.sharedScheduler().unscheduleAllSelectorsForTarget(this);
            g_sharedActionManager = null;
        }

        public void removeAction(CCAction action)
        {
            if (action != null)
            {
                CCObject originalTarget = action.originalTarget;
                if (this.m_pTargets.ContainsKey(originalTarget))
                {
                    tHashElement element = this.m_pTargets[originalTarget];
                    int index = element.actions.IndexOf(action);
                    if (index != -1)
                    {
                        this.removeActionAtIndex(index, element);
                    }
                }
                else
                {
                    CCLog.Log("cocos2d: removeAction: Target not found");
                }
            }
        }

        protected void removeActionAtIndex(int index, tHashElement element)
        {
            CCAction action = element.actions[index];
            if ((action == element.currentAction) && !element.currentActionSalvaged)
            {
                element.currentActionSalvaged = true;
            }
            element.actions.RemoveAt(index);
            if (element.actionIndex >= index)
            {
                element.actionIndex--;
            }
            if (element.actions.Count == 0)
            {
                if (this.m_pCurrentTarget == element)
                {
                    this.m_bCurrentTargetSalvaged = true;
                }
                else
                {
                    this.deleteHashElement(element);
                }
            }
        }

        public void removeActionByTag(int tag, CCObject target)
        {
            if (this.m_pTargets.ContainsKey(target))
            {
                tHashElement element = this.m_pTargets[target];
                int count = element.actions.Count;
                for (int i = 0; i < count; i++)
                {
                    CCAction action = element.actions[i];
                    if ((action.tag == tag) && (action.originalTarget == target))
                    {
                        this.removeActionAtIndex(i, element);
                        return;
                    }
                }
            }
        }

        public void removeAllActions()
        {
            CCObject[] array = new CCObject[this.m_pTargets.Keys.Count];
            this.m_pTargets.Keys.CopyTo(array, 0);
            for (int i = 0; i < array.Length; i++)
            {
                this.removeAllActionsFromTarget(array[i]);
            }
        }

        public void removeAllActionsFromTarget(CCObject target)
        {
            if (target != null)
            {
                if (this.m_pTargets.ContainsKey(target))
                {
                    tHashElement element = this.m_pTargets[target];
                    if (element.actions.Contains(element.currentAction) && !element.currentActionSalvaged)
                    {
                        element.currentActionSalvaged = true;
                    }
                    element.actions.Clear();
                    if (this.m_pCurrentTarget == element)
                    {
                        this.m_bCurrentTargetSalvaged = true;
                    }
                    else
                    {
                        this.deleteHashElement(element);
                    }
                }
                else
                {
                    CCLog.Log("cocos2d: removeAllActionsFromTarget: Target not found");
                }
            }
        }

        public void resumeTarget(CCObject target)
        {
            if (this.m_pTargets.ContainsKey(target))
            {
                this.m_pTargets[target].paused = false;
            }
        }

        public static CCActionManager sharedManager()
        {
            CCActionManager manager = g_sharedActionManager;
            if (manager == null)
            {
                manager = g_sharedActionManager = new CCActionManager();
                if (!g_sharedActionManager.init())
                {
                    manager = (CCActionManager)(g_sharedActionManager = null);
                }
            }
            return manager;
        }

        public void update(float dt)
        {
            CCObject[] array = new CCObject[this.m_pTargets.Keys.Count];
            this.m_pTargets.Keys.CopyTo(array, 0);
            for (int i = 0; i < array.Length; i++)
            {
                if (this.m_pTargets.ContainsKey(array[i]))
                {
                    tHashElement element = this.m_pTargets[array[i]];
                    this.m_pCurrentTarget = element;
                    this.m_bCurrentTargetSalvaged = false;
                    if (!this.m_pCurrentTarget.paused)
                    {
                        this.m_pCurrentTarget.actionIndex = 0;
                        while (this.m_pCurrentTarget.actionIndex < this.m_pCurrentTarget.actions.Count)
                        {
                            this.m_pCurrentTarget.currentAction = this.m_pCurrentTarget.actions[this.m_pCurrentTarget.actionIndex];
                            if (this.m_pCurrentTarget.currentAction != null)
                            {
                                this.m_pCurrentTarget.currentActionSalvaged = false;
                                this.m_pCurrentTarget.currentAction.step(dt);
                                if (this.m_pCurrentTarget.currentAction.isDone())
                                {
                                    this.m_pCurrentTarget.currentAction.stop();
                                    CCAction currentAction = this.m_pCurrentTarget.currentAction;
                                    this.m_pCurrentTarget.currentAction = null;
                                    this.removeAction(currentAction);
                                }
                                this.m_pCurrentTarget.currentAction = null;
                            }
                            this.m_pCurrentTarget.actionIndex++;
                        }
                    }
                    if (this.m_bCurrentTargetSalvaged && (this.m_pCurrentTarget.actions.Count == 0))
                    {
                        this.deleteHashElement(this.m_pCurrentTarget);
                    }
                }
            }
            this.m_pCurrentTarget = null;
        }
    }
}
