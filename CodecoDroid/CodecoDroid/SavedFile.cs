using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Auth;

namespace CodecoDroid
{
    public class SavedKeyDictionary
    {
        ///<summary>AKA ServiceId.</summary>
        public string Name { get; set; }
        public Dictionary<string, string> KeysDictionary { get; private set; }

        //keep a file that stores all the serviceIDs--each ServiceID will be exposed as a "file" to the user. each "file" will contain all the keypairs.
        public SavedKeyDictionary()
        {
            var accountStore = Xamarin.Auth.AccountStore.Create();
            accountStore.
        }
    }
}
