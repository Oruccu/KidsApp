using System.Collections.Generic;

namespace KidsAppBackend.Business.Utilities
{
    public interface ITokenBlacklist
    {
        void AddToBlacklist(string token);
        bool IsTokenBlacklisted(string token);
    }

    public class TokenBlacklist : ITokenBlacklist
    {
        private readonly HashSet<string> _blacklist = new();

        public void AddToBlacklist(string token)
        {
            _blacklist.Add(token);
        }

        public bool IsTokenBlacklisted(string token)
        {
            return _blacklist.Contains(token);
        }
    }
}
