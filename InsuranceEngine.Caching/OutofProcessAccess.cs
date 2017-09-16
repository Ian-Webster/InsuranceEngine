using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace InsuranceEngine.Caching
{
    public class OutofProcessAccess : CacheAccessBase<OutofProcessAccess>
    {
        #region cache access methods

        public void InsertUserSpecificCacheItem<T>(string subsystem, string key, T value)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[GetKey(subsystem, key)] = value;
            }
        }
        public T GetUserSpecificCacheItem<T>(string subsystem, string key)
        {
            T result = default(T);
            if (HttpContext.Current != null && HttpContext.Current.Session[GetKey(subsystem, key)] != null)
            {
                result = (T)HttpContext.Current.Session[GetKey(subsystem, key)];
            }
            return result;
        }

        public void InsertApplicationSpecificCacheItem<T>(string subsystem, string key, T value)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Cache.Insert(GetKey(subsystem, key), value, null, OverNightExpiryTime, Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.NotRemovable, null);
            }
        }
        public void InsertApplicationSpecificCacheItem<T>(string subsystem, string key, DateTime expireTime, T value)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Cache.Insert(GetKey(subsystem, key), value, null, expireTime, Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.NotRemovable, null);
            }
        }
        public T GetApplicationSpecificCacheItem<T>(string subsystem, string key)
        {
            T result = default(T);
            if (HttpContext.Current != null && HttpContext.Current.Cache[GetKey(subsystem, key)] != null)
            {
                result = (T)HttpContext.Current.Cache[GetKey(subsystem, key)];
            }
            return result;
        }

        #endregion

        #region cache clearing methods

        public void ClearUserSpecificCacheItem(string subsystem, string key)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session.Remove(GetKey(subsystem, key));
            }
        }
        public void ClearUserSpecificSubsystemCacheItems(string subsystem)
        {
            if (HttpContext.Current != null)
            {
                List<string> keysToRemove = new List<string>();
                foreach (string entry in System.Web.HttpContext.Current.Session.Keys)
                {
                    if (entry.StartsWith(subsystem))
                    {
                        keysToRemove.Add(entry);

                    }
                }
                foreach (string key in keysToRemove)
                {
                    System.Web.HttpContext.Current.Session.Remove(key);
                }
            }
        }
        public void KillUserCache()
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
            }
        }

        public void ClearApplicationSpecificCacheItem(string subsystem, string key)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Cache.Remove(GetKey(subsystem, key));
            }
        }
        public void ClearApplicationSpecificSubsystemCacheItems(string subsystem)
        {
            if (HttpContext.Current != null)
            {
                List<string> keys = new List<string>();
                IDictionaryEnumerator enumerator = HttpContext.Current.Cache.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    keys.Add(enumerator.Key.ToString());
                }

                keys.Where(k => k.StartsWith(subsystem))
                    .ToList()
                    .ForEach(k => HttpContext.Current.Cache.Remove(k));
            }
        }
        public void KillApplicationCache()
        {
            if (HttpContext.Current != null)
            {
                foreach (DictionaryEntry entry in System.Web.HttpContext.Current.Cache)
                {
                    System.Web.HttpContext.Current.Cache.Remove((string)entry.Key);
                }

            }
        }

        #endregion

        #region helper methods

        private string GetKey(string subSystem, string key)
        {
            return (subSystem + "|" + key.Replace("|", ""));
        }

        #endregion

        public DateTime OverNightCacheExpiry
        {
            get { return OverNightExpiryTime; }
        }
    }
}
