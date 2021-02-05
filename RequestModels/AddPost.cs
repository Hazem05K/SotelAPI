using System;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace SoTelAPI.RequestModels
{
    public class AddPost
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public string idUser { get; set; }
    }
}
