namespace FinalProject;

public class JsonSerializeClass
{
    public List<User> Users { get; set; }
    public List<Showroom> Showrooms { get; set; }


   
    public JsonSerializeClass(List<User> users, List<Showroom> showrooms)
    {
        Users = users;
        Showrooms = showrooms;
    }

   
}