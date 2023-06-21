using MongoDB.Driver;
using MongoDB_Repository;

namespace MongoDB_Repository_Specs
{
   
    public class Update_Specs
    {
        readonly IRepository<User> _collection;
        readonly string _id;
        public Update_Specs()
        {
            _collection =
                new Repository<User>(
                    conn: "mongodb+srv://admin:Admin123@cluster0.hnw2mbr.mongodb.net/",
                    db: "MongoDB_Demo",
                    collection: "Update"
                    );
            _id = Guid.NewGuid().GetHashCode().ToString();
            var name = $"name${_id}";
            var user = $"{name}";
            var password = "123456";

            var u1 = new User(id: _id, name: name, username: user, password: password);

             _collection.Add(u1).GetAwaiter().GetResult();
        }

      
        [Fact]
        public async void update()
        {
           
            var u2 = new User(
                id: _id,
                username:"tphong1120",
                password:"123456",
                name:"phong"
                );

            await _collection.Update(id: _id, entity: u2);

            var f1 = _collection.GetById(id: _id).GetAwaiter().GetResult();

            var eq = true;
            if (!u2.Equals(f1))
            {
                eq = false;
            }
            Assert.True(eq);

        }


    }
}