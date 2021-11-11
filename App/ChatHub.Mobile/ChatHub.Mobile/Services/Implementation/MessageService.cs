using System;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using ChatHub.Library.Models;
using ChatHub.Mobile.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace ChatHub.Mobile.Services.Implementation
{
    public class MessageService : IMessageService
    {
        private HubConnection _hubConnection;

        private Subject<MessageUIModel> _messageSubject = new Subject<MessageUIModel>();

        public MessageService()
        {
            MessageObservable = _messageSubject;
        }

        public async Task InitializeConnection()
        {
            try
            {
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl("https://chathubtests2021.azurewebsites.net/chatHub")
                    //.WithUrl("https://10.0.2.2:5001/chatHub")
                    .ConfigureLogging(builder =>
                    {
                        builder.SetMinimumLevel(LogLevel.Trace);
                   //     builder.AddConsole();
                    })
                    .WithAutomaticReconnect()
                    .Build();

                await _hubConnection.StartAsync();

                _hubConnection.On("ReceiveMessage", (object obj) =>
                {
                    // var message = (Message) obj;
                    // _messageSubject.OnNext(new MessageUIModel(message, false));
                });
                
                // _hubConnection.On("ReceiveMessage", (Message obj) =>
                // {
                //     // var message = (Message) obj;
                //     // _messageSubject.OnNext(new MessageUIModel(message, false));
                // });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            
            //await _hubConnection.Start()
          // await _clientWebSocket.ConnectAsync(new Uri("ws://localhost:5000/chatHub"), cancellationTokenSource.Token);
          //
          // await Task.Factory.StartNew(async () =>
          // {
          //     while (true)
          //     {
          //         WebSocketReceiveResult result;
          //         var message = new ArraySegment<byte>(new byte[4096]);
          //         do
          //         {
          //             result = await _clientWebSocket.ReceiveAsync(message, cancellationTokenSource.Token);
          //             var messageBytes = message.Skip(message.Offset).Take(result.Count).ToArray();
          //             var serializedMessages = Encoding.UTF8.GetString(messageBytes);
          //
          //             try
          //             {
          //                 var msg = JsonConvert.DeserializeObject<Message>(serializedMessages);
          //                 _messageSubject.OnNext(msg);
          //             }
          //             catch (Exception ex)
          //             {
          //                 Console.WriteLine($"Invalid message format. {ex.Message}");
          //             }
          //
          //         } while (!result.EndOfMessage);
          //     }
          // }, cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

        }
        public IObservable<MessageUIModel> MessageObservable { get; }

        public Task SendMessageAsync(Message message, CancellationTokenSource cancellationTokenSource)
        {
            _messageSubject.OnNext(new MessageUIModel(message, true));
            return _hubConnection.SendAsync("SendMessageAsync", message);

            // async Task SendMessage(string user, string message)
            // {
            //     await hubConnection.InvokeAsync("SendMessage", user, message);
            // }
        }
    }
}