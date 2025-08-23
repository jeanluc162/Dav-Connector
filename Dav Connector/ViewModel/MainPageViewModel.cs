using AsyncAwaitBestPractices.MVVM;
using Dav_Connector.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dav_Connector.ViewModel
{
    class MainPageViewModel : BindableBase
    {
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
        public async Task EditAccountAsync(Guid parameter)
        {
            
        }
        public MainPageViewModel()
        {
            EditAccountCommand = new AsyncCommand<Guid>(EditAccountAsync);
        }
        public IAsyncCommand<Guid> EditAccountCommand { get; }
    }
}
