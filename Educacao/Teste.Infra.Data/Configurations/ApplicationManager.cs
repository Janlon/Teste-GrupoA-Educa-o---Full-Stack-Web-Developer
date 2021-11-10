using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Teste.Domain.Entities;
using Teste.Infra.Data.Contexts;

namespace Teste.Infra.Data.Configurations
{
    public class ApplicationManager
    {
        #region Maintenance
        public Context Db { get; private set; }
        public RoleManager<IdentityRole> RM
        {
            get
            {
                return new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(Db), null, null, null, null);
            }
        }
        public UserManager<AspNetUsers> UM
        {
            get
            {
                return new UserManager<AspNetUsers>(new UserStore<AspNetUsers>(Db, null), null, null, null, null, null, null, null, null);
            }
        }
        #endregion

        #region Instance
        public ApplicationManager(Context db)
        {
            Db = db;
        }

        #endregion
    }
}
