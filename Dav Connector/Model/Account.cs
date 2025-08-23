using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace Dav_Connector.Model
{
    class Account:EntityBase
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
            get => StringCipher.Decrypt(EncryptedPassword, GetEncryptionPassword());
            set
            {
                EncryptedPassword = StringCipher.Encrypt(value, GetEncryptionPassword());
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
        AccountType _accountType;
        public AccountType AccountType
        {
            get => _accountType;
            set => SetProperty(ref _accountType, value);
        }
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

        private String GetEncryptionPassword()
        {
            const String VaultIdentifier = "DavConnector";
            var vault = new PasswordVault();
            PasswordCredential credential;
            try
            {
                credential = vault.Retrieve(VaultIdentifier, VaultIdentifier);
            }
            catch
            {
                credential = new PasswordCredential(VaultIdentifier, VaultIdentifier, Guid.NewGuid().ToString());
                vault.Add(credential);
            }
            return credential.Password;
        }
    }
}
