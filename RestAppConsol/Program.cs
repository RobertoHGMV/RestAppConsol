using RestSharp;
using System;

namespace RestAppConsol
{
    class Program
    {
        static IRestClient _client;

        static void Main(string[] args)
        {
            try
            {
                _client = new RestClient("http://localhost:39052");

                var data = Login();

                var response = GetBillOfExchanges(data);

                Console.WriteLine("Operação realizada com sucesso!");
                Console.WriteLine();
                Console.WriteLine(response);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static Models.DataToken Login()
        {
            var request = new RestRequest("/api/AuthenticationCustomer/v1/Login", Method.POST);
            request.AddJsonBody(new { Email = "", Password = "" });

            var response = _client.Post<Models.DataToken>(request);

            if (!response.IsSuccessful)
                throw new Exception(response.Content);

            var repCookie = response.Cookies[0];
            var cookie = new Models.DataCookie
            {
                Name = repCookie.Name,
                Value = repCookie.Value
            };

            response.Data.Cookie = cookie;

            return response.Data;
        }

        static string GetBillOfExchanges(Models.DataToken dataToken)
        {
            var request = new RestRequest("/api/BillOfExchange/v1/849", Method.GET);

            request.AddHeader("Authorization", $"bearer {dataToken.Token}");
            request.AddParameter(dataToken.Cookie.Name, dataToken.Cookie.Value, ParameterType.Cookie);

            var response = _client.Get(request);

            if (!response.IsSuccessful)
                throw new Exception(response.Content);

            return response.Content;
        }
    }
}
