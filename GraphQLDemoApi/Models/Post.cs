namespace GraphQLDemoApi.Models
{

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int UserId { get; set; }
        public User? User { get; set; }
        public List<Comment> Comments { get; set; } = new();
    }

}