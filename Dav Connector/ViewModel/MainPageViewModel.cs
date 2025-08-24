using AsyncAwaitBestPractices.MVVM;
using Dav_Connector.Library;
using Dav_Connector.Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.Storage.Streams;
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
        public async Task StartSyncAsync(Guid parameter)
        {
            Account account;
            using (var dbContext = new DavConnectorDbContext())
            {
                account = await dbContext.Accounts.SingleAsync(acc => acc.Id == parameter);
            }

            var cards = await Dav_Connector.Library.DavSyncHelper.GetVCards(
                account.Url,
                account.UserName,
                account.Password
            );
            var ddd = await (await ContactManager.RequestStoreAsync()).CreateContactListAsync("dddd");

            foreach (var card in cards)
            {
                using (var stream = new InMemoryRandomAccessStream())
                {
                    await stream.WriteAsync(card.AsBuffer());
                    var contact = await ContactManager.ConvertVCardToContactAsync(RandomAccessStreamReference.CreateFromStream(stream));
                    await ddd.SaveContactAsync(contact);
                }
            }
        }
        public MainPageViewModel()
        {
            EditAccountCommand = new AsyncCommand<Guid?>(EditAccountAsync);
            DeleteAccountCommand = new AsyncCommand<Guid>(DeleteAccountAsync);
            StartSyncCommand = new AsyncCommand<Guid>(StartSyncAsync);
        }
        public IAsyncCommand<Guid?> EditAccountCommand { get; }
        public IAsyncCommand<Guid> DeleteAccountCommand { get; }
        public IAsyncCommand<Guid> StartSyncCommand { get; }
    }
}
