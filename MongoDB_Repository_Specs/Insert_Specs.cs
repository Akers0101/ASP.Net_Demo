using MongoDB.Driver;
using MongoDB_Repository;

namespace MongoDB_Repository_Specs
{
    public class User
    {
        public string id { set; get; }
        public string name { set; get; }
        public string username { set; get; }
        public string password { set; get; }
       

        public User(string id, string username, string password, string name)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.name = name;
        }

        public override bool Equals(object? obj)
        {
            var other = obj as User;

            return other is not null && other.id ==id && other.name== name&& other.username==username && other.password==password ;
        }
    }
    public class Insert_Specs 
    {
        readonly IRepository<User> _collection;
        
        public Insert_Specs()
        {
            _collection =
                new Repository<User>(
                    conn: "mongodb+srv://admin:Admin123@cluster0.hnw2mbr.mongodb.net/",
                    db: "MongoDB_Demo",
                    collection: "Insert"
                    );
        }

        [Fact]
        public void connection_sucessfully()
        {
            Assert.NotNull(_collection);
            

        }
        [Fact]
        public async void insert()
        {
            var id =  Guid.NewGuid().GetHashCode().ToString();
            var name = $"name${id}";
            var user = $"{name}";
            var password = "123456";

            var u1 = new User(id: id, name: name, username: user, password: password);

            await _collection.Add(u1);

            var f1 =  _collection.GetById(id:id).GetAwaiter().GetResult();

            var eq = true;
            if (!u1.Equals(f1))
            {
                eq= false;
            }
            Assert.True(eq);

        }


    }
}