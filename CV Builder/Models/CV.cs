using Microsoft.AspNetCore.Identity;

namespace CV_Builder.Models
{
    public class CV
    {
        public int Id { get; set; }
   
        public string? Title { get; set; }
        
        public string? UserId { get; set; }
        public CV()
        {
                
        }
    }
}
