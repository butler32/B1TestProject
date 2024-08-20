using B1TestProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1TestProject.Services
{
    public class CancellationTokenService : ICancellationTokenService
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public CancellationToken GetToken()
        {
            return _cts.Token;
        }

        public void Cancel()
        {
            _cts.Cancel();
        }
    }
}
