using Common.Entitites;
using DLL.Context;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    public class ClientManager : IClientManager
    {
        private EFdbContext db;

        public ClientManager(EFdbContext context)
        {
            this.db = context;
        }
        public void Create(ClientProfile item)
        {
            db.ClientProfiles.Add(item);
           db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
