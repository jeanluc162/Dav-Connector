using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace Dav_Connector.Library.Model
{
    public class Account:EntityBase
    {
        String _name;
        public String Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        String _userName;
        public String UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }
        [NotMapped]
        public String Password
        {
            get => StringCipher.Decrypt(EncryptedPassword, DavConnectorDbContext.GetEncryptionPassword?.Invoke() ?? "TEST");
            set
            {
                EncryptedPassword = StringCipher.Encrypt(value, DavConnectorDbContext.GetEncryptionPassword?.Invoke() ?? "TEST");
                OnPropertyChanged();
            }
        }
        String _encryptedPassword;
        public String EncryptedPassword
        {
            get => _encryptedPassword;
            set => SetProperty(ref _encryptedPassword, value);
        }
        String _url;
        public String Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }
        [ForeignKey(nameof(AccountType))]
        public Guid AccountTypeId { get; set; }
        AccountType _accountType;
        public AccountType AccountType
        {
            get => _accountType;
            set => SetProperty(ref _accountType, value);
        }
        [ForeignKey(nameof(SyncType))]
        public Guid SyncTypeId { get; set; }
        SyncType _syncType;
        public SyncType SyncType
        {
            get => _syncType;
            set => SetProperty(ref _syncType, value);
        }

        public Account()
        {
            Id = Guid.Empty;
            Name = "";
            UserName = "";
            Password = "";
            Url = "";
            AccountType = null;
            SyncType = null;
        }
    }
}
