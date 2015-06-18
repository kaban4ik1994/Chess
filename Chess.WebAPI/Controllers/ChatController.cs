using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Chess.WebAPI.Filters.AuthorizationFilters;
using Chess.WebAPI.Models;
using Microsoft.Web.WebSockets;
using Newtonsoft.Json;

namespace Chess.WebAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ChatController : ApiController
    {
        public HttpResponseMessage Get(string userName)
        {
            HttpContext.Current.AcceptWebSocketRequest(new ChatWebSocketHandler(userName));
            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
        }

        class ChatWebSocketHandler : WebSocketHandler
        {
            private static readonly WebSocketCollection ChatClients = new WebSocketCollection();
            private readonly string _userName;

            public ChatWebSocketHandler(string userName)
            {
                _userName = userName;
            }

            public override void OnOpen()
            {
                ChatClients.Add(this);
            }

            public override void OnMessage(string message)
            {
                ChatClients.Broadcast(JsonConvert.SerializeObject(new MessageViewModel { From = _userName, Content = message }));
            }
        }
    }
}
