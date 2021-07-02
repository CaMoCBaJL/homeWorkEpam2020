﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using Entities;
using System.Security.Cryptography;
using System.IO;

namespace JsonDAL
{
    public class UserIdentities
    {
        internal void DeleteIdentity(int userId) => UpdateIdentities(new Identity(userId, default), IdentityUpdateType.Delete);

        internal void AddIdentity(int userId, int passwordHashSum) => UpdateIdentities(new Identity(userId, passwordHashSum), IdentityUpdateType.Add);

        void UpdateIdentities(Identity identity, IdentityUpdateType updateType)
        {
            List<Identity> identities = JsonConvert.DeserializeObject<List<Identity>>(File.ReadAllText(PathConstants.identitiesDataLocation));

            if (identities == null)
                identities = new List<Identity>();

            switch (updateType)
            {
                case IdentityUpdateType.Add:
                    identities.Add(identity);
                    break;
                case IdentityUpdateType.Delete:
                    identities.Remove(identities.Find(id => id.UserId == identity.UserId));
                    break;
                case IdentityUpdateType.None:
                default:
                    return;
            }

            File.WriteAllText(PathConstants.identitiesDataLocation, JsonConvert.SerializeObject(identities));
        }
    }
}
