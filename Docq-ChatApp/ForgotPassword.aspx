<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Docq_ChatApp.ForgotPassword" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <title>DOC-Q A Private Chap App</title>
    <meta name="description" content="A mini chat app for Doc-Q interview">
    <meta name="author" content="Mustafa Kenlic">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="https://fonts.googleapis.com/css2?family=Poppins:ital,wght@0,400;0,600;1,400;1,600&family=Teko:wght@500&display=swap" rel="stylesheet">
    <link href="/assest/css/default.min.css?v=0.001" rel="stylesheet" />
</head>
<body>
<section class="center">
    <%--<form method="post" class="form" action="">
            <div>
                <h2 class="formBaslik">Doc-Q<br>
                    <strong>Private Chat</strong></h2>
            </div>
            <div class="formWrap">
                <label for="username" class="eob">User Name</label>
                <input autocomplete="off" name="username" type="email" class="mainForm-input" id="username" placeholder="Your Email" required>
            </div>
            <div class="formWrap">
                <label for="password" class="eob">Password</label>
                <input autocomplete="off" name="password" type="password" class="mainForm-input" id="password" placeholder="Your Password" required>
            </div>

            <div class="formWrap">
                <button class="buton formSubmit" id="mainSliderformSubmit">Reset Password</button>
            </div>
            <div id="errorMessages">
                <p id="errorMessage" class="errorMessage">Hata Mesajı</p>
            </div>            
        </form>--%>
    <asp:Literal ID="LtrForm" runat="server"></asp:Literal>
        
</section>
</body>
</html>

