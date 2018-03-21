using Microsoft.AspNet.Identity.EntityFramework;

namespace CorkCollector.Web.API
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {

        }
    }
}