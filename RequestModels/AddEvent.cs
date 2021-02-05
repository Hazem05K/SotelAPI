using System;
using Microsoft.AspNetCore.Http;

namespace SoTelAPI.RequestModels
{
    public class AddEvent
    {
       
        public string Title { get; set; }

        public string Descreption { get; set; }

        public IFormFile Banner { get; set; }

        public string time { get; set; }
    }
    
}
