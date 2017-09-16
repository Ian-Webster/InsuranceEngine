using InsuranceEngine.Caching;

namespace InsuranceEngine.Business.Contexts
{
    public class WebContext : ContextBase<WebContext>
    {

        public void ClearCache()
        {
            OutofProcessAccess.Instance.KillApplicationCache();
            OutofProcessAccess.Instance.KillUserCache();
        }

    }
}
