namespace cocos2d
{
    using System;
    using System.Collections.Generic;

    public class CCScheduler
    {
        private static CCScheduler g_sharedScheduler;
        protected bool m_bCurrentTargetSalvaged;
        protected bool m_bUpdateHashLocked;
        protected float m_fTimeScale;
        private List<tListEntry> m_listToDelete;
        protected tHashSelectorEntry m_pCurrentTarget;
        protected Dictionary<SelectorProtocol, tHashSelectorEntry> m_pHashForSelectors;
        protected Dictionary<SelectorProtocol, tHashUpdateEntry> m_pHashForUpdates;
        protected List<tListEntry> m_pUpdates0List;
        protected List<tListEntry> m_pUpdatesNegList;
        protected List<tListEntry> m_pUpdatesPosList;

        private CCScheduler()
        {
        }

        private void appendIn(List<tListEntry> list, SelectorProtocol target, bool bPaused)
        {
            tListEntry item = new tListEntry
            {
                target = target,
                paused = bPaused,
                markedForDeletion = false
            };
            list.Add(item);
            tHashUpdateEntry entry2 = new tHashUpdateEntry
            {
                target = target,
                list = list,
                entry = item
            };
            this.m_pHashForUpdates.Add(target, entry2);
        }

        ~CCScheduler()
        {
            this.unscheduleAllSelectors();
            this.m_pHashForSelectors.Clear();
            this.m_pHashForUpdates.Clear();
            this.m_pUpdatesNegList.Clear();
            this.m_pUpdates0List.Clear();
            this.m_pUpdatesPosList.Clear();
            this.m_listToDelete.Clear();
            g_sharedScheduler = null;
        }

        private bool init()
        {
            this.m_fTimeScale = 1f;
            this.m_pUpdatesNegList = new List<tListEntry>();
            this.m_pUpdates0List = new List<tListEntry>();
            this.m_pUpdatesPosList = new List<tListEntry>();
            this.m_pHashForUpdates = new Dictionary<SelectorProtocol, tHashUpdateEntry>();
            this.m_pHashForSelectors = new Dictionary<SelectorProtocol, tHashSelectorEntry>();
            this.m_listToDelete = new List<tListEntry>();
            return true;
        }

        public bool isTargetPaused(SelectorProtocol target)
        {
            if (target == null)
            {
                return false;
            }
            return (this.m_pHashForSelectors.ContainsKey(target) && this.m_pHashForSelectors[target].paused);
        }

        public void pauseTarget(SelectorProtocol target)
        {
            if (target != null)
            {
                if (this.m_pHashForSelectors.ContainsKey(target))
                {
                    this.m_pHashForSelectors[target].paused = true;
                }
                if (this.m_pHashForUpdates.ContainsKey(target))
                {
                    tHashUpdateEntry entry = this.m_pHashForUpdates[target];
                    if (entry != null)
                    {
                        entry.entry.paused = true;
                    }
                }
            }
        }

        private void priorityIn(List<tListEntry> list, SelectorProtocol target, int nPriority, bool bPaused)
        {
            tListEntry item = new tListEntry
            {
                target = target,
                priority = nPriority,
                paused = bPaused,
                markedForDeletion = false
            };
            int index = 0;
            foreach (tListEntry entry2 in list)
            {
                if (nPriority < entry2.priority)
                {
                    break;
                }
                index++;
            }
            list.Insert(index, item);
            tHashUpdateEntry entry3 = new tHashUpdateEntry
            {
                target = target,
                list = list,
                entry = item
            };
            this.m_pHashForUpdates.Add(target, entry3);
        }

        public static void purgeSharedScheduler()
        {
            g_sharedScheduler = null;
        }

        private void removeHashElement(tHashSelectorEntry element)
        {
            element.timers.Clear();
            element.target = null;
            SelectorProtocol key = null;
            foreach (KeyValuePair<SelectorProtocol, tHashSelectorEntry> pair in this.m_pHashForSelectors)
            {
                if (element == pair.Value)
                {
                    key = pair.Key;
                    break;
                }
            }
            this.m_pHashForSelectors.Remove(key);
        }

        private void removeUpdateFromHash(tListEntry entry)
        {
            if (this.m_pHashForUpdates.ContainsKey(entry.target))
            {
                tHashUpdateEntry entry2 = this.m_pHashForUpdates[entry.target];
                entry2.list.Remove(entry);
                entry2.entry = null;
                entry2.target = null;
                SelectorProtocol key = null;
                foreach (KeyValuePair<SelectorProtocol, tHashUpdateEntry> pair in this.m_pHashForUpdates)
                {
                    if (entry2 == pair.Value)
                    {
                        key = pair.Key;
                        break;
                    }
                }
                this.m_pHashForUpdates.Remove(key);
            }
        }

        public void resumeTarget(SelectorProtocol target)
        {
            if (target != null)
            {
                if (this.m_pHashForSelectors.ContainsKey(target))
                {
                    this.m_pHashForSelectors[target].paused = false;
                }
                if (this.m_pHashForUpdates.ContainsKey(target))
                {
                    tHashUpdateEntry entry = this.m_pHashForUpdates[target];
                    entry.entry.paused = false;
                }
            }
        }

        public void scheduleSelector(SEL_SCHEDULE selector, SelectorProtocol target, float fInterval, bool bPaused)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector", "Schedule selector can not be null");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target", "Schedule target must be set.");
            }
            tHashSelectorEntry entry = null;
            if (!this.m_pHashForSelectors.ContainsKey(target))
            {
                entry = new tHashSelectorEntry
                {
                    target = target
                };
                this.m_pHashForSelectors[target] = entry;
                entry.paused = bPaused;
            }
            else
            {
                entry = this.m_pHashForSelectors[target];
            }
            if (entry.timers == null)
            {
                entry.timers = new List<CCTimer>();
            }
            else
            {
                foreach (CCTimer timer in entry.timers)
                {
                    if (selector == timer.m_pfnSelector)
                    {
                        CCLog.Log("CCSheduler#scheduleSelector. Selector already scheduled.");
                        timer.m_fInterval = fInterval;
                        return;
                    }
                }
            }
            CCTimer item = new CCTimer();
            item.initWithTarget(target, selector, fInterval);
            entry.timers.Add(item);
        }

        public void scheduleUpdateForTarget(SelectorProtocol targt, int nPriority, bool bPaused)
        {
            tHashUpdateEntry entry = null;
            if (this.m_pHashForUpdates.ContainsKey(targt))
            {
                entry = this.m_pHashForUpdates[targt];
                entry.entry.markedForDeletion = false;
            }
            if (nPriority == 0)
            {
                this.appendIn(this.m_pUpdates0List, targt, bPaused);
            }
            else if (nPriority < 0)
            {
                this.priorityIn(this.m_pUpdatesNegList, targt, nPriority, bPaused);
            }
            else
            {
                this.priorityIn(this.m_pUpdatesPosList, targt, nPriority, bPaused);
            }
        }

        public static CCScheduler sharedScheduler()
        {
            if (g_sharedScheduler == null)
            {
                g_sharedScheduler = new CCScheduler();
                g_sharedScheduler.init();
            }
            return g_sharedScheduler;
        }

        public void tick(float dt)
        {
            this.m_bUpdateHashLocked = true;
            if (this.m_fTimeScale != 1f)
            {
                dt *= this.m_fTimeScale;
            }
            for (int i = 0; i < this.m_pUpdatesNegList.Count; i++)
            {
                tListEntry entry = this.m_pUpdatesNegList[i];
                if (!entry.paused && !entry.markedForDeletion)
                {
                    entry.target.update(dt);
                }
            }
            for (int j = 0; j < this.m_pUpdates0List.Count; j++)
            {
                tListEntry entry2 = this.m_pUpdates0List[j];
                if (!entry2.paused && !entry2.markedForDeletion)
                {
                    entry2.target.update(dt);
                }
            }
            for (int k = 0; k < this.m_pUpdatesPosList.Count; k++)
            {
                tListEntry entry3 = this.m_pUpdatesPosList[k];
                if (!entry3.paused && !entry3.markedForDeletion)
                {
                    entry3.target.update(dt);
                }
            }
            SelectorProtocol[] array = new SelectorProtocol[this.m_pHashForSelectors.Keys.Count];
            this.m_pHashForSelectors.Keys.CopyTo(array, 0);
            for (int m = 0; m < array.Length; m++)
            {
                if (this.m_pHashForSelectors.ContainsKey(array[m]))
                {
                    tHashSelectorEntry entry4 = this.m_pHashForSelectors[array[m]];
                    this.m_pCurrentTarget = entry4;
                    this.m_bCurrentTargetSalvaged = false;
                    if (!this.m_pCurrentTarget.paused)
                    {
                        entry4.timerIndex = 0;
                        while (entry4.timerIndex < entry4.timers.Count)
                        {
                            entry4.currentTimer = entry4.timers[(int)entry4.timerIndex];
                            entry4.currentTimerSalvaged = false;
                            entry4.currentTimer.update(dt);
                            entry4.currentTimer = null;
                            entry4.timerIndex++;
                        }
                    }
                    if (this.m_bCurrentTargetSalvaged && (this.m_pCurrentTarget.timers.Count == 0))
                    {
                        this.removeHashElement(this.m_pCurrentTarget);
                    }
                }
            }
            for (int n = 0; n < this.m_pUpdatesNegList.Count; n++)
            {
                tListEntry item = this.m_pUpdatesNegList[n];
                if (item.markedForDeletion)
                {
                    this.m_listToDelete.Add(item);
                }
            }
            for (int num6 = 0; num6 < this.m_pUpdates0List.Count; num6++)
            {
                tListEntry entry6 = this.m_pUpdates0List[num6];
                if (entry6.markedForDeletion)
                {
                    this.m_listToDelete.Add(entry6);
                }
            }
            for (int num7 = 0; num7 < this.m_pUpdatesPosList.Count; num7++)
            {
                tListEntry entry7 = this.m_pUpdatesPosList[num7];
                if (entry7.markedForDeletion)
                {
                    this.m_listToDelete.Add(entry7);
                }
            }
            foreach (tListEntry entry8 in this.m_listToDelete)
            {
                this.removeUpdateFromHash(entry8);
            }
            this.m_listToDelete.Clear();
        }

        public void unscheduleAllSelectors()
        {
            tHashSelectorEntry[] array = new tHashSelectorEntry[this.m_pHashForSelectors.Values.Count];
            this.m_pHashForSelectors.Values.CopyTo(array, 0);
            for (int i = 0; i < array.Length; i++)
            {
                this.unscheduleAllSelectorsForTarget(array[i].target);
            }
            tListEntry[] entryArray2 = this.m_pUpdates0List.ToArray();
            for (int j = 0; j < entryArray2.Length; j++)
            {
                this.unscheduleAllSelectorsForTarget(entryArray2[j].target);
            }
            entryArray2 = this.m_pUpdatesNegList.ToArray();
            for (int k = 0; k < entryArray2.Length; k++)
            {
                this.unscheduleAllSelectorsForTarget(entryArray2[k].target);
            }
            entryArray2 = this.m_pUpdatesPosList.ToArray();
            for (int m = 0; m < entryArray2.Length; m++)
            {
                this.unscheduleAllSelectorsForTarget(entryArray2[m].target);
            }
        }

        public void unscheduleAllSelectorsForTarget(SelectorProtocol target)
        {
            if (target != null)
            {
                if (this.m_pHashForSelectors.ContainsKey(target))
                {
                    tHashSelectorEntry element = this.m_pHashForSelectors[target];
                    if (element.timers.Contains(element.currentTimer))
                    {
                        element.currentTimerSalvaged = true;
                    }
                    element.timers.Clear();
                    if (this.m_pCurrentTarget == element)
                    {
                        this.m_bCurrentTargetSalvaged = true;
                    }
                    else
                    {
                        this.removeHashElement(element);
                    }
                }
                this.unscheduleUpdateForTarget(target);
            }
        }

        public void unscheduleSelector(SEL_SCHEDULE selector, SelectorProtocol target)
        {
            if (((selector != null) && (target != null)) && this.m_pHashForSelectors.ContainsKey(target))
            {
                tHashSelectorEntry element = this.m_pHashForSelectors[target];
                for (int i = 0; i < element.timers.Count; i++)
                {
                    CCTimer timer = element.timers[i];
                    if (selector == timer.m_pfnSelector)
                    {
                        if ((timer == element.currentTimer) && !element.currentTimerSalvaged)
                        {
                            element.currentTimerSalvaged = true;
                        }
                        element.timers.RemoveAt(i);
                        if (element.timerIndex >= i)
                        {
                            element.timerIndex--;
                        }
                        if (element.timers.Count == 0)
                        {
                            if (this.m_pCurrentTarget == element)
                            {
                                this.m_bCurrentTargetSalvaged = true;
                                return;
                            }
                            this.removeHashElement(element);
                        }
                        return;
                    }
                }
            }
        }

        public void unscheduleUpdateForTarget(SelectorProtocol target)
        {
            if ((target != null) && this.m_pHashForUpdates.ContainsKey(target))
            {
                tHashUpdateEntry entry = this.m_pHashForUpdates[target];
                if (this.m_bUpdateHashLocked)
                {
                    entry.entry.markedForDeletion = true;
                }
                else
                {
                    this.removeUpdateFromHash(entry.entry);
                }
            }
        }

        public float timeScale
        {
            get
            {
                return this.m_fTimeScale;
            }
            set
            {
                this.m_fTimeScale = value;
            }
        }
    }
}
