using System;
using System.Linq;
using System.Net.WebSockets;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatHub.Library.Models;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;

namespace ChatHub.Mobile.Services.Implementation
{
    public class MessageService : IMessageService
    {
        private HubConnection _hubConnection;
        private readonly ClientWebSocket _clientWebSocket;
        private Subject<Message> _messageSubject = new Subject<Message>();

        public MessageService()
        {
            MessageObservable = _messageSubject;
            _clientWebSocket = new ClientWebSocket();
        }

        public async Task InitializeConnection(CancellationTokenSource cancellationTokenSource)
        {
          await _clientWebSocket.ConnectAsync(new Uri("ws://localhost:5000/chatHub"), cancellationTokenSource.Token);
          
          await Task.Factory.StartNew(async () =>
          {
              while (true)
              {
                  WebSocketReceiveResult result;
                  var message = new ArraySegment<byte>(new byte[4096]);
                  do
                  {
                      result = await _clientWebSocket.ReceiveAsync(message, cancellationTokenSource.Token);
                      var messageBytes = message.Skip(message.Offset).Take(result.Count).ToArray();
                      var serializedMessages = Encoding.UTF8.GetString(messageBytes);

                      try
                      {
                          var msg = JsonConvert.DeserializeObject<Message>(serializedMessages);
                          _messageSubject.OnNext(msg);
                      }
                      catch (Exception ex)
                      {
                          Console.WriteLine($"Invalid message format. {ex.Message}");
                      }

                  } while (!result.EndOfMessage);
              }
          }, cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }
        public IObservable<Message> MessageObservable { get; }

        public Task SendMessageAsync(Message message, CancellationTokenSource cancellationTokenSource)
        {
            
            var serialisedMessage = JsonConvert.SerializeObject(message);

            var byteMessage = Encoding.UTF8.GetBytes(serialisedMessage);
            var segment = new ArraySegment<byte>(byteMessage);

            return _clientWebSocket.SendAsync(segment, WebSocketMessageType.Text, true, cancellationTokenSource.Token);
        }
    }
}