<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChatHubTest.aspx.cs" Inherits="Docq_ChatApp.ChatHubTest" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

</head>
<body>
    <div>
        <input type="text" id="senderId"/>
        <input type="text" id="receiverId"/>
        <input type="text" id="content"/>
        <input type="button" id="sendmessage" value="send"/>
        <%--<input type="hidden" id="displayname"/>--%>
        <ul id="discussion">

        </ul>
        
        
        <%--<button id="sendButton">Send Message</button>--%>

    </div>
    
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js" integrity="sha512-v2CJ7UaYy4JwqLDIrZUI/4hqeoQieOmAZNXBeQyjo21dadnwR+8ZaIJVT8EE2iyI61OV8e6M8PP2/4hpQINQ/g==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/signalr.js/2.4.3/jquery.signalR.min.js" integrity="sha512-GA74ohU6Jbe86KnYUjZvXq73wkBbhxNJ0vWBrZ/fU8cO+pAqaw6zi833NAnzAgRf7YSaqa9QB4TX3VLns/J9uw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
<script src="/signalr/hubs"></script>


    <script>
        $(function() {
            let chat = $.connection.chatHub;

            chat.client.boardcastMessage = function (senderId, receiverId, content) {
                let encodedsenderId = $('<div />').text(senderId).html();
                let encodedreceiverId = $('<div />').text(receiverId).html();
                let encodedcontent = $('<div />').text(content).html();

                $('#discussion').append('<li><strong>' + encodedsenderId + ' => ' + encodedreceiverId + '</strong>:&nbsp;' + encodedcontent + '</li>');
            }

            //$('#displayname').val(prompt('Enter Your Name:', ""));
            $('#content').focus();

            $.connection.hub.start().done(function() {
                $('#sendmessage').click(function() {
                    chat.server.send($('#senderId').val(), $('#receiverId').val(), $('#content').val());
                    $('#content').val('').focus();
                });
            });
        });
    </script>

    <!-- JavaScript kodları -->
    <%--<script>
        $(function () {
            // SignalR hub'ını oluşturun
            var chatHub = $.connection.chatHub;

            // Hub üzerinde broadcastMessage fonksiyonunu dinleyin
            chatHub.client.broadcastMessage = function (senderId, receiverId, content) {
                // Gelen mesajları ekrana yazdırın (veya işlem yapın)
                console.log(`Received message from ${senderId} to ${receiverId}: ${content}`);
            };

            // SignalR bağlantısını başlatın
            $.connection.hub.start().done(function () {
                console.log("SignalR connection started.");

                // Sayfadaki bir butonu seçin ve tıklama olayını tanımlayın
                $("#sendButton").on("click", function () {
                    // Kullanıcıdan alınan verilerle bir mesaj gönderme fonksiyonu
                    var senderId = "1"; // Gönderen kullanıcının ID'si
                    var receiverId = "2"; // Alıcı kullanıcının ID'si
                    var content = "Hello, world!"; // Gönderilecek mesaj

                    // Server tarafındaki SendMessage fonksiyonunu çağırarak mesajı gönderin
                    chatHub.server.send(senderId, receiverId, content);
                    console.log("Button clicked");
                });

                // broadcastMessage fonksiyonunu dinleyerek gelen mesajları loglayın
                chatHub.client.broadcastMessage = function (senderId, receiverId, content) {
                    console.log(`Received message from ${senderId} to ${receiverId}: ${content}`);
                };
            });
        });
    </script>--%>

</body>
</html>

