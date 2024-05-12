using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceTranscoding;


namespace Explorer.API.Controllers
{
    public class AuthenticationProtoController : Authorize.AuthorizeBase
    {
        private readonly ILogger<AuthenticationProtoController> _logger;

        public AuthenticationProtoController(ILogger<AuthenticationProtoController> logger)
        {
            _logger = logger;
        }

        public override async Task<AuthenticationTokens> Authorize(Credentials request,
            ServerCallContext context)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("https://localhost:44332", new GrpcChannelOptions { HttpHandler = httpHandler });
            //mislim da treba da bude https://localhost:44333 ?


            var client = new Authorize.AuthorizeClient(channel);
            var response = await client.AuthorizeAsync(request);

            Console.WriteLine(response.AccessToken);

            return await Task.FromResult(new AuthenticationTokens
            {
                Id = response.Id,
                AccessToken = response.AccessToken
            });
        }
    }
}
