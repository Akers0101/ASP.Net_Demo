using MongoDB_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB_Repository_Specs
{
    public class Batch_Insert_Specs
    {
        readonly IRepository<User> _collection;

        public Batch_Insert_Specs()
        {
            _collection =
                new Repository<User>(
                    conn: "mongodb+srv://admin:Admin123@cluster0.hnw2mbr.mongodb.net/",
                    db: "MongoDB_Demo",
                    collection: "Batch_Insert");
        }
        
        [Fact]
        public async void batch_insert()
        {
           

            var u1 = new List<User>();
            int range = 100;
            for(int i=0; i<range; i++)
            {
                var us = new User(id: Guid.NewGuid().GetHashCode().ToString(),
                    name: "Phong123",
                    username: $"tphong123",
                    password: "123456@");
                u1.Add(us);

            }
            await _collection.AddMany(u1);

            var f1 = _collection.GetAll().GetAwaiter().GetResult();

            var eq = true;
            u1.Sort((x, y) => x.id.CompareTo(y.id));
            f1.Sort((x, y) => x.id.CompareTo(y.id));
            for (int i=0; i < u1.Count; i++)
            {
                var s1 = u1[i];
                var s2 = f1[i];
                if (!s1.Equals(s2))
                {
                    eq = false;
                }
            }
            
            Assert.True(eq);

        }


    }
}
