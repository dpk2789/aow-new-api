namespace WebApp.UI2.Helpers
{
    public interface ICookieHelper
    {
        void Set(string str, string str2, int? i);

        string Get(string key);

        void Remove(string key);

    }
}
