using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Program
    {
        static void Main(string[] args)
        {
            MemcachedCs _MemCache = new MemcachedCs();
            _MemCache.Appent();
            List<Person> _list = new List<Person>();
            _list = _MemCache.GetList();
            
        }
    }

    public class MemcachedCs
    {
        public void Appent()
        {
            MemcachedClientConfiguration config = new MemcachedClientConfiguration();
            config.AddServer("127.0.0.1", 11211);
            config.Protocol = MemcachedProtocol.Binary;
            MemcachedClient client = new MemcachedClient(config);
            List<Person> Persons1 = new List<Person>()
            {
                new Person { Isim= "hasan", Yas = 37,Soyad="sert" },
                new Person { Isim= "Bahacan", Yas = 17,Soyad="Alak" },
                new Person { Isim= "Suat", Yas = 41,Soyad="Alak"  }
            };
            bool result = client.Store(StoreMode.Set, "Persons1", Persons1);
        }       
        public List<Person> GetList()
        {
            try
            {
                MemcachedClientConfiguration config = new MemcachedClientConfiguration();
                config.AddServer("127.0.0.1", 11211);
                config.Protocol = MemcachedProtocol.Binary;
                MemcachedClient client = new MemcachedClient(config);

                return client.Get<List<Person>>("Persons");
            }
            catch (Exception)
            {
                return new List<Person>();
            }
        }
        public void Delete()
        {
            MemcachedClientConfiguration config = new MemcachedClientConfiguration();
            config.AddServer("127.0.0.1", 11211);
            config.Protocol = MemcachedProtocol.Binary;
            MemcachedClient client = new MemcachedClient(config);
            client.Remove("Persons");
        }

    }
    [Serializable]
    public class Person
    {
        public string Isim { get; set; }
        public int Yas { get; set; }
        public string Soyad { get; set; }
    }
}
