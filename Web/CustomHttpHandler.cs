using Microsoft.JSInterop;

namespace Web
{
    /// <summary>
    /// Handles CRUD request authentication by injecting a JWT from localStorage.
    /// </summary>
    public class CustomHttpHandler : DelegatingHandler
    {
        private readonly IJSRuntime _jsRuntime;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomHttpHandler"/> class.
        /// </summary>
        public CustomHttpHandler(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            string token = await _jsRuntime.InvokeAsync<string>(
                "localstorage.getItem",
                "jwtToken"
            );

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer",
                        token
                    );
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
