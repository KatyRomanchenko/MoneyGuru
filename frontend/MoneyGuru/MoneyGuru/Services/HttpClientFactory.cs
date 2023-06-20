using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MoneyGuru.Services
{
    public class HttpClientFactory
    {
        public HttpClient CreateAuthenticatedClient()
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            HttpClient client = new HttpClient(handler);
            return client;
        }

        public string mainURL = "http://192.168.1.3:5000";
    }
}
