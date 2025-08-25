using Dav_Connector.Library.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebDav;
using Windows.ApplicationModel.Contacts;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace BackgroundTasks
{
    public static class AccountSync
    {
        private class RemoteIdContainer
        {
            public Guid AccountId { get; set; }
            public String RessourceUrl { get; set; }
            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }
        public static IAsyncAction SyncAccountAsync(Guid accountId)
        {
            return AsyncInfo.Run(async c =>
            {
                using (var dbContext = new DavConnectorDbContext())
                {
                    var account = await dbContext.Accounts.SingleOrDefaultAsync(acc => acc.Id == accountId, c);
                    if (account == null)
                        throw new KeyNotFoundException(accountId.ToString());

                    if (account.AccountTypeId == AccountType.CardDav)
                        await SyncCardDavAsync(dbContext, account);
                    else
                        throw new NotImplementedException();
                }
            }); 
        }
        private static IAsyncAction SyncCardDavAsync(DavConnectorDbContext dbContext, Account account)
        {
            return AsyncInfo.Run(async c =>
            {
                if (account.SyncTypeId != SyncType.RemoteToLocal)
                    throw new NotImplementedException();

                var contactStore = await ContactManager.RequestStoreAsync(ContactStoreAccessType.AppContactsReadWrite);
                var contactList = (await contactStore.FindContactListsAsync()).FirstOrDefault();
                if (contactList == null)
                    contactList = await contactStore.CreateContactListAsync("Dav Connector");

                var urlUri = new Uri(account.Url);
                var cards = new List<Byte[]>();
                using (var client = new WebDavClient(new WebDavClientParams { BaseAddress = new Uri($"{urlUri.Scheme}://{urlUri.Host}:{urlUri.Port}"), Credentials = new NetworkCredential(account.UserName, account.Password) }))
                {
                    var result = await client.Propfind(account.Url, new PropfindParameters());
                    foreach (var ressource in result.Resources.Where(r => r.ContentType?.Contains("text/vcard") ?? false))
                    {
                        try
                        {
                            using (var response = await client.GetFileResponse(new Uri(ressource.Uri, UriKind.Relative), false, new GetFileParameters()))
                            {
                                using (var stream = new InMemoryRandomAccessStream())
                                {
                                    await stream.WriteAsync((await response.Content.ReadAsByteArrayAsync()).AsBuffer());
                                    var contact = await ContactManager.ConvertVCardToContactAsync(RandomAccessStreamReference.CreateFromStream(stream));
                                    contact.RemoteId = new RemoteIdContainer { AccountId = account.Id, RessourceUrl = ressource.Uri }.ToString();

                                    var existingContact = await contactList.GetContactFromRemoteIdAsync(contact.RemoteId);
                                    if (existingContact != null)
                                        await contactList.DeleteContactAsync(existingContact);

                                    await contactList.SaveContactAsync(contact);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //TODO
                        }
                    }
                }
            });  
        }
    }
}
