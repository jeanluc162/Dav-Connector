using AsyncAwaitBestPractices.MVVM;
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
    class EditCredentialPageViewModel : BindableBase
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

            if (id != null)
                using (var dbContext = new DavConnectorDbContext())
                {
                    account = dbContext.Accounts.AsNoTracking().SingleOrDefault(acc => acc.Id == id);
                }

            _id = id;
            Name = account?.Name ?? "";
            UserName = account?.UserName ?? "";
            Password = account?.Password ?? "";
        }
        public async Task SaveAsync()
        {
            using (var dbContext = new DavConnectorDbContext())
            {
                Account account = null;
                if (_id != null)
                    account = await dbContext.Accounts.SingleOrDefaultAsync(acc => acc.Id == _id);
                else
                    _id = Guid.NewGuid();

                if (account == null)
                    account = dbContext.Accounts.Add(new Account
                    {
                        Id = _id.Value
                    }).Entity;

                account.Name = Name;
                account.UserName = UserName;
                account.Password = Password;

                await dbContext.SaveChangesAsync();
            }
        }
        public EditCredentialPageViewModel()
        {
            SaveCommand = new AsyncCommand(SaveAsync);
        }
        public IAsyncCommand SaveCommand { get; }
    }
}
