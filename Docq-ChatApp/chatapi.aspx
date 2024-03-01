<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatapi.aspx.cs" Inherits="Docq_ChatApp.chatapi" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>SignalR Chat</title>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.signalr/2.4.2/jquery.signalR.min.js"></script>
</head>
<body>

<input type="text" id="messageInput" placeholder="Mesajınızı yazın">
<button onclick="sendMessage()">Gönder</button>

<script>
    $(function () {
        // SignalR'a bağlan
        const chatHub = $.connection.chatHub;

        // Gelen mesajları işle
        chatHub.client.broadcastMessage = function (message) {
            console.log('Gelen mesaj:', message);
            // Gelen mesajı işleyin ve görüntüleyin
        };

        // SignalR bağlantısını başlat
        $.connection.hub.start().done(function () {
            console.log('SignalR bağlantısı başlatıldı.');
        });

        window.sendMessage = function () {
            const messageInput = document.getElementById('messageInput');
            const message = messageInput.value;

            if (message) {
                // Sunucuya mesaj gönder
                chatHub.server.sendMessage(message);
                messageInput.value = '';
            }
        };
    });
</script>

</body>
</html>
