using AsyncAwaitBestPractices.MVVM;
using Dav_Connector.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.UI.Popups;

namespace Dav_Connector.ViewModel
{
    class EditAccountPageViewModel : BindableBase
    {
        Account _account = null;
        public Account Account
        {
            get => _account;
            set => SetProperty(ref _account, value);
        }
        public AccountType[] AccountTypes
        {
            get
            {
                using (var dbContext = new DavConnectorDbContext())
                {
                    return dbContext.AccountTypes.AsNoTracking().ToArray();
                }
            }
        }
        public SyncType[] SyncTypes
        {
            get
            {
                using (var dbContext = new DavConnectorDbContext())
                {
                    return dbContext.SyncTypes.AsNoTracking().ToArray();
                }
            }
        }

        public void Load(Guid? id)
        {
            if (id != null)
                using (var dbContext = new DavConnectorDbContext())
                {
                    Account = dbContext.Accounts.AsNoTracking().SingleOrDefault(acc => acc.Id == id);
                }

            if (Account == null)
                Account = new Account { Id = Guid.NewGuid() };
        }
        public async Task SaveAsync()
        {
            using (var dbContext = new DavConnectorDbContext())
            {
                if (await dbContext.Accounts.AnyAsync(acc => acc.Id == Account.Id))
                    dbContext.Accounts.Update(Account);
                else
                    dbContext.Accounts.Add(Account);
                try
                {
                    await dbContext.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                    var dialog = new MessageDialog(ex.Message, "Error while Saving");
                    await dialog.ShowAsync();
                }
            }
        }
        public EditAccountPageViewModel()
        {
            SaveCommand = new AsyncCommand(SaveAsync);
        }
        public IAsyncCommand SaveCommand { get; }
    }
}
