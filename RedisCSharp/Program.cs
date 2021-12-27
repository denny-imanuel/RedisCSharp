using System;

namespace RedisCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            RedisConnector redis = new RedisConnector();
            var keys = redis.getKeyList();
            var str = redis.getStringValue("key1");
            var list = redis.getListValue("key2");
            var set = redis.getSetValue("key3");
            var sset = redis.getSortedSetValue("key4");
            var hash = redis.getHashValue("key5");
            var stream = redis.getStreamValue("key6");
        }
    }
}