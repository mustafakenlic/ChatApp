<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="Docq_ChatApp.Chat" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <title>DOC-Q A Private Chap App</title>
    <meta name="description" content="A mini chat app for Doc-Q interview">
    <meta name="author" content="Mustafa Kenlic">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="/assest/css/chat.min.css?v=0.001" rel="stylesheet" />
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" rel="stylesheet" />
</head>
<body>
    <main class="container">
        <header id="header">
            <div class="left">
                <figure class="resp logo">
                    <img src="/assest/img/logo-min.png" alt="Doc-Q Private Chat Logo" />
                    <figcaption class="eob">Doc-Q Private Chat Logo</figcaption>
                </figure>
            </div>
            <div class="right user">
                <div id="headerusrinfo">
                    <%--<img src="/assest/img/user-icon-min.png" alt="user icon"/>
                <div id="userinfo">
                    <strong>Name</strong>
                    <p>Departmen</p>
                </div>--%>
                    <asp:Literal ID="LtrActiveChat" runat="server"></asp:Literal>
                </div>
                <div id="mblMenu">
                    <span></span>
                    <span></span>
                    <span></span>
                </div>
            </div>
        </header>
        <div id="chatWrap">

            <aside id="users" class="left">
                <ul class="listeyok floatyok">
                    <asp:Literal ID="LtrUserList" runat="server"></asp:Literal>
                    <%--<li class="user" data-userid="">
                    <img src="/assest/img/user-icon-min.png" alt="user icon"/>
                    <div id="userinfo">
                        <strong>Name</strong>
                        <p>Departmen</p>
                    </div>
                </li>--%>
                    <%--<li class="user" data-userid="">
                    <img src="/assest/img/user-icon-min.png" alt="user icon"/>
                    <div id="userinfo">
                        <strong>Name</strong>
                        <p>Departmen</p>
                    </div>
                </li>
                <li class="user" data-userid="">
                    <img src="/assest/img/user-icon-min.png" alt="user icon"/>
                    <div id="userinfo">
                        <strong>Name</strong>
                        <p>Departmen</p>
                    </div>
                </li>
                <li class="user" data-userid="">
                    <img src="/assest/img/user-icon-min.png" alt="user icon"/>
                    <div id="userinfo">
                        <strong>Name</strong>
                        <p>Departmen</p>
                    </div>
                </li>
                <li class="user" data-userid="">
                    <img src="/assest/img/user-icon-min.png" alt="user icon"/>
                    <div id="userinfo">
                        <strong>Name</strong>
                        <p>Departmen</p>
                    </div>
                </li>--%>
                </ul>
            </aside>

            <section id="chat" class="right">
                <div id="msglist">
                    <div id="msglistwrap">
                        <%--<div class="message recived">
                            <div class="mesaage">
                                a
                            </div>
                        </div>

                        <div class="message send">
                            <div class="mesaage">
                                message
                            </div>
                        </div>--%>
                        <asp:Literal ID="LtrMsgList" runat="server"></asp:Literal>
                    </div>

                </div>

                <footer id="msgtextbox">
                    <textarea name="msgtextinput" id="msgtextinput" placeholder="Type your message" rows="3"></textarea>
                    <i class="fa fa-file-o" title="Send file" id="sendFile"></i>&nbsp;&nbsp;
                    <i class="fa fa-file-image-o" title="Send Image" id="sendImage"></i>
                    <button id="sendbtn">Send</button>
                    <%--
                        <input type="hidden" id="activeuserid" value="2"/>
                        <input type="hidden" id="currentuserid" value="1"/>
                    --%>
                    <asp:Literal ID="LtrActiveUserId" runat="server"></asp:Literal>
                </footer>
            </section>
        </div>
    </main>


    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js" integrity="sha512-v2CJ7UaYy4JwqLDIrZUI/4hqeoQieOmAZNXBeQyjo21dadnwR+8ZaIJVT8EE2iyI61OV8e6M8PP2/4hpQINQ/g==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/signalr.js/2.4.3/jquery.signalR.min.js" integrity="sha512-GA74ohU6Jbe86KnYUjZvXq73wkBbhxNJ0vWBrZ/fU8cO+pAqaw6zi833NAnzAgRf7YSaqa9QB4TX3VLns/J9uw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="/signalr/hubs"></script>

    <script src="/assest/js/chat.min.js?v=0.001"></script>
</body>
</html>
