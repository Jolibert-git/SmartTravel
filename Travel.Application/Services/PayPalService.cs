using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Travel.Application.Contracts;

namespace Travel.Application.Services
{
    public class PayPalService : IPaypalService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PayPalService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> CreatePaymentAsync(decimal amount, string currency, string description)
        {
            // 1. Get credentials from secrets.json
            var clientId = _configuration["Paypal:Client ID"];
            var secret = _configuration["Paypal:Secret key 1"];

            // 2. Get Access Token from PayPal
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{secret}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

            var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("grant_type", "client_credentials") });
            var authResponse = await _httpClient.PostAsync("https://api-m.sandbox.paypal.com/v1/oauth2/token", content);

            // Note: In a real project, you'd parse the token here.
            // For now, this is the structure to start your integration.

            return "Payment_ID_Placeholder";
        }
    }
}
