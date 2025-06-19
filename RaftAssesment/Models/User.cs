namespace RaftAssesment.Models
{
    public class Users
    {
        public long page { get;set; }
        public long per_page { get; set; }
        public long total { get; set; }
        public long total_pages { get;set;}
        public List<UserData> data { get; set; } = new List<UserData>();
        public Support support { get; set; } = new Support();
       
    }
    public class UserData
    {
        public long id { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string avatar { get; set; }
    }

    public class Support
    {
        public string url { get; set; }
        public string text { get; set; }
    }

    public class User
    {
        public UserData data { get; set; } = new UserData();
        public Support support { get; set; } = new Support();
    }
}
