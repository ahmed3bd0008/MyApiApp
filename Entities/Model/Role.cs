namespace Entities.Model
{
    public class Role: Microsoft.AspNetCore.Identity.IdentityRole
    {
        public int ImportantDegree { get; set; }
        
    }
}