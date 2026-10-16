using sagut_packages_trpo6.Models;

namespace sagut_packages_trpo6.Service
{
    public class DBService
    {
        private WebApiDatabaseContext context; 
        public WebApiDatabaseContext Context => context; 
        private static DBService? instance; 
        public static DBService Instance { 
            get { 
                if (instance == null) 
                    instance = new DBService(); 
                return instance; 
            } 
        }
        private DBService() 
        { 
            context = new WebApiDatabaseContext(); 
        }
    }
}
