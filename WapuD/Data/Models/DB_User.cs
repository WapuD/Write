namespace WapuD.Data.Models

{
    public class DB_User
    {
        public int Id { get; set; }
        public string UserSurname { get; set; }
        public string UserName { get; set; }
        public string UserPatronymic { get; set; }
        public int UserRole { get; set; }
    }
}
