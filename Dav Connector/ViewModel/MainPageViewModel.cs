using AsyncAwaitBestPractices.MVVM;
using Dav_Connector.Library;
using Dav_Connector.Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Dav_Connector.ViewModel
{
    class MainPageViewModel : BindableBase
    {
        public event EventHandler<Guid?> AccountEditRequested;
        public Account[] Accounts
        {
            get
            {
                using (var dbContext = new DavConnectorDbContext())
                {
                    return dbContext.Accounts.AsNoTracking().ToArray();
                }
            }
        }
        public async Task EditAccountAsync(Guid? parameter)
        {
            AccountEditRequested?.Invoke(this, parameter);
        }
        public async Task DeleteAccountAsync(Guid parameter)
        {
            using (var dbContext = new DavConnectorDbContext())
            {
                var account = await dbContext.Accounts.SingleOrDefaultAsync(acc => acc.Id == parameter);
                if(account != null)
                {
                    dbContext.Accounts.Remove(account);
                    await dbContext.SaveChangesAsync();
                    OnPropertyChanged(nameof(Accounts));
                }
            }
        }
        public MainPageViewModel()
        {
            EditAccountCommand = new AsyncCommand<Guid?>(EditAccountAsync);
            DeleteAccountCommand = new AsyncCommand<Guid>(DeleteAccountAsync);
        }
        public IAsyncCommand<Guid?> EditAccountCommand { get; }
        public IAsyncCommand<Guid> DeleteAccountCommand { get; }
    }
}
