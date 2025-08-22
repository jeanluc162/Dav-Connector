using Dav_Connector.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace Dav_Connector.ViewModel
{
    class EditCredentialPageViewModel:BindableBase
    {
        private Guid? _id;

        String _password;
        public String Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        String _userName;
        public String UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }
        String _name;
        public String Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public void Load(Guid? id)
        {
            Account account = null;

            if(id != null)
                using (var dbContext = new DavConnectorDbContext())
                {
                    account = dbContext.Accounts.AsNoTracking().SingleOrDefault(acc => acc.Id == id);
                }

            _id = id;
            Name = account?.Name ?? "";
            UserName = account?.UserName ?? "";

            var vault = new Windows.Security.Credentials.PasswordVault();
            if(account != null)
                try
                {
                    Password = vault.Retrieve(_id.ToString(), UserName).Password;
                }
                catch
                {
                    Password = "";
                }
        }
        public void Save()
        {
            using (var dbContext = new DavConnectorDbContext())
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    Account account = null;
                    if (_id != null)
                        account = dbContext.Accounts.SingleOrDefault(acc => acc.Id == _id);
                    else
                        _id = Guid.NewGuid();

                    if (account == null)
                        account = dbContext.Accounts.Add(new Account
                        {
                            Id = _id.Value
                        }).Entity;

                    account.Name = Name;
                    account.UserName = UserName;

                    dbContext.SaveChanges();
                }
                    
            }
        }
    }
}
