using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Travel.Application.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; init; }
        public int StatusCode { get; init; }
        public string Message { get; init; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; init; }

        /// <summary>
        /// Errores de validación por campo.
        /// Solo se incluye en la respuesta cuando hay errores (no null).
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IDictionary<string, string[]>? Errors { get; init; }

        // ----------------------------------------------------------------
        // Factories — uso desde controllers y servicios
        // ----------------------------------------------------------------

        /// <summary>Respuesta exitosa con datos.</summary>
        public static ApiResponse<T> Ok(T data, string message = "Operación exitosa.")
            => new()
            {
                Success = true,
                StatusCode = 200,
                Message = message,
                Data = data
            };

        /// <summary>Respuesta exitosa para creaciones (201).</summary>
        public static ApiResponse<T> Created(T data, string message = "Recurso creado exitosamente.")
            => new()
            {
                Success = true,
                StatusCode = 201,
                Message = message,
                Data = data
            };

        /// <summary>Respuesta exitosa sin datos (204 — delete, logout).</summary>
        public static ApiResponse<T> NoContent(string message = "Operación completada.")
            => new()
            {
                Success = true,
                StatusCode = 204,
                Message = message
            };

        /// <summary>Respuesta de error genérica.</summary>
        public static ApiResponse<T> Fail(
            string message,
            int statusCode,
            IDictionary<string, string[]>? errors = null)
            => new()
            {
                Success = false,
                StatusCode = statusCode,
                Message = message,
                Errors = errors
            };
    }

    // ============================================================
    // PagedResponse<T> — para listados paginados
    // ============================================================

    /// <summary>
    /// Extiende ApiResponse para incluir metadata de paginación.
    /// Enmanuel puede usar TotalPages y CurrentPage para construir
    /// el paginador en el frontend.
    /// </summary>
    public class PagedResponse<T> : ApiResponse<IEnumerable<T>>
    {
        public int TotalRecords { get; init; }
        public int TotalPages { get; init; }
        public int CurrentPage { get; init; }
        public int PageSize { get; init; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public static PagedResponse<T> Create(
            IEnumerable<T> data,
            int totalRecords,
            int currentPage,
            int pageSize,
            string message = "Operación exitosa.")
            => new()
            {
                Success = true,
                StatusCode = 200,
                Message = message,
                Data = data,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize),
                CurrentPage = currentPage,
                PageSize = pageSize
            };
    }
}
