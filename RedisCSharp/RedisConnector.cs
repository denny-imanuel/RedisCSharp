using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using StackExchange.Redis;

namespace RedisCSharp
{
    public class RedisConnector
    {
        private string connStr = "localhost:6379";
        private ConnectionMultiplexer redis;
        private IDatabase database;
        private IServer server;
        public RedisConnector()
        {
            try
            {
                var config = new ConfigurationOptions
                {
                    EndPoints = {connStr}
                };
                redis = ConnectionMultiplexer.Connect(config);
                server = redis.GetServer(connStr);
                database = redis.GetDatabase();
                Console.WriteLine("Connection Successful");
            }
            catch (Exception err)
            {
                Console.WriteLine("Connection Failed: " + err.Message);
            }
        }
        public Dictionary<string, string> getKeyList()
        {
            var keys = new Dictionary<string, string>();
            try
            {
                var ikeys = server.Keys();
                foreach (var ikey in ikeys)
                {
                    var key = ikey.ToString();
                    var type = database.KeyType(ikey).ToString();
                    keys.Add(key, type);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return keys;
        }

        public string getStringValue(string key)
        {
            var val = "";
            try
            {
                val = database.StringGet(key);

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return val;
        }
        
        public List<string> getListValue(string key)
        {
            var vals = new List<string>();
            try
            {
                var ivals = database.ListRange(key);
                foreach (var ival in ivals)
                {
                    var val = ival.ToString();
                    vals.Add(val);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return vals;
        }
        
        public HashSet<string> getSetValue(string key)
        {
            var vals = new HashSet<string>();
            try
            {
                var ivals = database.SetScan(key);
                foreach (var ival in ivals)
                {
                    var val = ival.ToString();
                    vals.Add(val);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return vals;
        }
        
        public SortedSet<string> getSortedSetValue(string key)
        {
            var vals = new SortedSet<string>();
            try
            {
                var ivals = database.SortedSetScan(key);
                foreach (var ival in ivals)
                {
                    var val = ival.ToString();
                    vals.Add(val);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return vals;
        }
        
        public Dictionary<string, string> getHashValue(string key)
        {
            var vals = new Dictionary<string, string>();
            try
            {
                var ivals = database.HashScan(key);
                foreach (var ival in ivals)
                {
                    var dkey = ival.Key.ToString();
                    var dval = ival.Value.ToString();
                    vals.Add(dkey, dval);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return vals;
        }
        
        public Dictionary<string, Dictionary<string, string>> getStreamValue(string key)
        {
            var parents = new Dictionary<string, Dictionary<string, string>>();
            var childs = new Dictionary<string, string>();
            try
            {
                var iparents = database.StreamRange(key);
                foreach (var iparent in iparents)
                {
                    var id = iparent.Id.ToString();
                    var ichilds = iparent.Values;
                    foreach (var ichild in ichilds)
                    {
                        var name = ichild.Name.ToString();
                        var value = ichild.Value.ToString();
                        childs.Add(name, value);
                    }
                    parents.Add(id, childs);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return parents;
        }

        public void addStringValue(string key, string value)
        {
            //todo: implement this
        }
        
        public void addListValue(string key, List<string> value)
        {
            //todo: implement this
        }
        
        public void addSetValue(string key, HashSet<string> value)
        {
            //todo: implement this
        }
        
        public void addSortedSetValue(string key, SortedSet<string> value)
        {
            //todo: implement this
        }
        
        public void addHashValue(string key, Dictionary<string, string> value)
        {
            //todo: implement this
        }
        
        public void addStreamValue(string key, Dictionary<string, Dictionary<string, string>> value)
        {
            //todo: implement this
        }
    }
}