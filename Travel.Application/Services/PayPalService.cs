using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Travel.Application.Contracts;
using Travel.Application.DTOs;
using Travel.Application.Request;
using Travel.Domain.Contracts;
using Travel.Domain.Exceptions;

namespace Travel.Application.Services
{
    public class PayPalService : IPaypalService
    {
        private readonly IUnitOfWork _uow;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PayPalService(HttpClient httpClient, IConfiguration configuration, IUnitOfWork _uow)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            this._uow = _uow;
        }


        public async Task<string> CreateOrderFromServiceAsync(long id, CancellationToken ct)
        {
            string currency = "USD";

            var service = await _uow.OfferedServices.GetByIdAsync(id)
                ?? throw new NotFoundException("Services", id);

            // 1. Obtener Access Token
            var accessToken = await GetAccessTokenAsync();

            // 2. Configurar Bearer Token
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            // 3. Crear body para PayPal
            var order = new PayPalOrderRequest
            {
                Intent = "CAPTURE",

                PurchaseUnits =
                [
                    new PurchaseUnitDto
            {
                Description = service.ServiceDescription,

                Amount = new AmountDto
                {
                    CurrencyCode = currency,
                    Value = service.Price.ToString("F2")
                }
            }
                ],

                ApplicationContext = new ApplicationContextDto
                {
                    ReturnUrl = "https://www.example.com",
                    CancelUrl = "https://www.example.com"
                }
            };

            // 4. Convertir a JSON
            var json = JsonSerializer.Serialize(order);

            // 5. Crear contenido HTTP
            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            // 6. Llamar PayPal
            var response = await _httpClient.PostAsync(
                "https://api-m.sandbox.paypal.com/v2/checkout/orders",
                content);

            // 7. Validar HTTP response
            //response.EnsureSuccessStatusCode();

            // 7. Leer posible error
            var errorContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"PayPal Error: {errorContent}");
            }

            // 8. Leer JSON response
            var responseJson = await response.Content.ReadAsStringAsync();

            // 9. Convertir JSON -> DTO
            var orderResponse = JsonSerializer.Deserialize<PayPalOrderResponseDto>(
                responseJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            // 10. Buscar approval link
            var approveLink = orderResponse?.Links
                .FirstOrDefault(l => l.Rel == "approve");

            if (approveLink is null)
                throw new Exception("PayPal no devolvió approval url.");

            // 11. Retornar URL para frontend
            return approveLink.Href;
        }

        public async Task<string> CreatePaymentAsync(decimal amount, string currency, string description)
        {
            // 1. Credentials
            var clientId = _configuration["Paypal:Client ID"];
            var secret = _configuration["Paypal:Secret key 1"];

            // 2. Convert to Base64
            var auth = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{clientId}:{secret}")
            );

            // 3. Auth Header
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", auth);

            // 4. Request Body
            var content = new FormUrlEncodedContent(
                new[]
                {
                 new KeyValuePair<string, string>(
                     "grant_type",
                     "client_credentials")
                });

            // 5. Call PayPal
            var authResponse = await _httpClient.PostAsync(
                "https://api-m.sandbox.paypal.com/v1/oauth2/token",
                content);

            // 6. Read raw JSON
            var json = await authResponse.Content.ReadAsStringAsync();

            // 7. Convert JSON -> C#
            var tokenResponse = JsonSerializer.Deserialize<PayPalTokenResponseDto>(json);

            // 8. Return access token
            return tokenResponse!.AccessToken;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            // 1. Obtener credenciales
            var clientId = _configuration["Paypal:Client ID"];
            var secret = _configuration["Paypal:Secret key 1"];

            // 2. Convertir a Base64
            var auth = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{clientId}:{secret}")
            );

            // 3. Configurar Authorization Header
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", auth);

            // 4. Body requerido por PayPal
            var content = new FormUrlEncodedContent(
                new[]
                {
            new KeyValuePair<string, string>(
                "grant_type",
                "client_credentials")
                });

            // 5. Llamar API de PayPal
            var response = await _httpClient.PostAsync(
                "https://api-m.sandbox.paypal.com/v1/oauth2/token",
                content);

            // 6. Validar respuesta HTTP
            response.EnsureSuccessStatusCode();

            // 7. Leer JSON
            var json = await response.Content.ReadAsStringAsync();

            // 8. Convertir JSON -> DTO
            var tokenResponse = JsonSerializer.Deserialize<PayPalTokenResponseDto>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            // 9. Validar token
            if (tokenResponse is null || string.IsNullOrEmpty(tokenResponse.AccessToken))
                throw new Exception("No se pudo obtener el access token de PayPal.");

            // 10. Retornar token
            return tokenResponse.AccessToken;
        }


        public async Task<string> CreateOrderAsync(
                                                   decimal amount,
                                                   string currency,
                                                   string description
                                                  )
        {
            // 1. Obtener Access Token
            var accessToken = await GetAccessTokenAsync();

            // 2. Configurar Bearer Token
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            // 3. Crear body para PayPal
            var order = new PayPalOrderRequest
            {
                Intent = "CAPTURE",

                PurchaseUnits =
                [
                    new PurchaseUnitDto
            {
                Description = description,

                Amount = new AmountDto
                {
                    CurrencyCode = currency,
                    Value = amount.ToString("F2")
                }
            }
                ],

                ApplicationContext = new ApplicationContextDto
                {
                    ReturnUrl = "https://www.example.com",
                    CancelUrl = "https://www.example.com"
                }
            };

            // 4. Convertir a JSON
            var json = JsonSerializer.Serialize(order);

            // 5. Crear contenido HTTP
            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            // 6. Llamar PayPal
            var response = await _httpClient.PostAsync(
                "https://api-m.sandbox.paypal.com/v2/checkout/orders",
                content);

            // 7. Validar HTTP response
            //response.EnsureSuccessStatusCode();

            // 7. Leer posible error
            var errorContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"PayPal Error: {errorContent}");
            }

            // 8. Leer JSON response
            var responseJson = await response.Content.ReadAsStringAsync();

            // 9. Convertir JSON -> DTO
            var orderResponse = JsonSerializer.Deserialize<PayPalOrderResponseDto>(
                responseJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            // 10. Buscar approval link
            var approveLink = orderResponse?.Links
                .FirstOrDefault(l => l.Rel == "approve");

            if (approveLink is null)
                throw new Exception("PayPal no devolvió approval url.");

            // 11. Retornar URL para frontend
            return approveLink.Href;
        }



        public async Task<PayPalCaptureResponseDto> CaptureOrderAsync(string orderId)
        {
            // 1. Obtener Access Token
            var accessToken = await GetAccessTokenAsync();

            // 2. Configurar Bearer Token
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);


            // 3. Crear body vacío JSON
            var content = new StringContent(
                "{}",
                Encoding.UTF8,
                "application/json");



            // 3. Llamar endpoint capture
            var response = await _httpClient.PostAsync(
               $"https://api-m.sandbox.paypal.com/v2/checkout/orders/{orderId}/capture",
               content);


            // DEBUG
            var responseText = await response.Content.ReadAsStringAsync();


            // 4. Validar respuesta HTTP
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"PayPal Capture Error: {responseText}");
            }


            // 5. Convertir JSON -> DTO
            var captureResponse = JsonSerializer.Deserialize<PayPalCaptureResponseDto>(
                responseText,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });



            // 7. Validar
            if (captureResponse is null)
                throw new Exception("No se pudo capturar la orden PayPal.");

            // 8. Retornar resultado
            return captureResponse;
        }



    }
}
