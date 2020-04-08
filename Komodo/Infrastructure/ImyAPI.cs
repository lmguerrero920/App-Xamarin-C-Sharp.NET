using Komodo.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Komodo.Infrastructure
{
  public  interface ImyAPI
    {

        [Get("http://127.0.0.1:8093/api/UbicationItems")]
        Task<List<Ubication>> GetUser();

    }
}
