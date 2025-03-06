namespace Shared.Models.Service;

public class AppHostTemporaryCacheModels
{
    public class TemporaryDataCache
    {
        public int secconds { get; set; }
        public DateTime now = DateTime.Now;
        public string data { get; set; } = string.Empty;
    }

    public class AppHostTemporaryDataCache
    {
        private Dictionary<string, TemporaryDataCache> data;

        public AppHostTemporaryDataCache() { data = new Dictionary<string, TemporaryDataCache>(); }

        public TemporaryDataCache? Get(string key) 
            => this.data.ContainsKey(key.ToUpperInvariant()) ? this.data[key.ToUpperInvariant()] : null;

        public void Unset(string key)
        {
            if (!this.data.ContainsKey(key.ToUpperInvariant())) 
                return;
            this.data.Remove(key);
        }

        public void Set(string key, string value, int secconds)
        {
            var toAdd = new TemporaryDataCache { data = value, secconds = secconds, now = DateTime.UtcNow };

            if (!this.data.ContainsKey(key.ToUpperInvariant()))
            {
                this.data.Add(key.ToUpperInvariant(), toAdd);
                return;
            }

            this.data.Remove(key.ToUpperInvariant());
            this.data.Add(key.ToUpperInvariant(), toAdd);
        }

        public List<string> GetKeys()
        {
            List<string> keys = new List<string>();
            foreach (string key in this.data.Keys) keys.Add(key);
            return keys;
        }
    }
}