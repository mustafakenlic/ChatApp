//define dom objects
const mblMenuBtn = document.getElementById("mblMenu");
const usersMenu = document.getElementById("users");
const userList = document.querySelectorAll('li.user');
const msglist = document.getElementById("msglist");
const msglistWrap = document.getElementById("msglistwrap");
const activeUserID = document.getElementById("activeuserid");
const msgTextInput = document.getElementById("msgtextinput");
const sendBtn = document.getElementById("sendbtn");

//get global varibles
const currentUserID = Number(document.getElementById("currentuserid").value);


// mobil user list show hide
mblMenuBtn.onclick = () => {
    usersMenu.classList.toggle("active");
}

// at windows 
window.onload = () => {
    // scrool to last msg
    msglist.scrollTop = msglist.scrollHeight;
};

// for each li.user onclick event 
userList.forEach(user =>
    user.addEventListener('click', () => {
        const userId = user.getAttribute('data-userid');
        getMessages(userId);

        // remove unread class
        document.getElementById("user" + userId).classList.remove("unread");
    })
);

const getMessages = (userID) => {

    const url = "/chatapi.aspx/getmessages";

    const postData = {
        userid: userID
    };

    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(postData)
    };

    fetch(url, requestOptions)
        .then(response => {
            // İsteğin başarılı olup olmadığını kontrol et
            if (!response.ok) {
                console.log(`HTTP error! Status: ${response.status}`);
            }
            // JSON olarak cevabı al
            return response.json();
        })
        .then(data => {
            // Gelen JSON verileriyle istediğiniz işlemleri yapın
            //console.log('Response Data:', data);

            const responseData = JSON.parse(data.d);
            let msgs = "";
            
            for (let i = 0; i < responseData.length; i++) {

                let SenderId = Number(responseData[i].senderid);
                let Content = responseData[i].content;
                
                // if it is a send msg
                if (SenderId === currentUserID) {
                    msgs += `<div class="message send">
                            <div class="mesaage">
                                ${Content}
                            </div>
                        </div>`;
                } else {
                    msgs += `<div class="message recived">
                            <div class="mesaage">
                                ${Content}
                            </div>
                        </div>`;
                }
            }


            //set the mesaggess
            msglistWrap.innerHTML = msgs;

            //change active user ID
            activeUserID.value = userID;

            // scrool to last msg
            msglist.scrollTop = msglist.scrollHeight;
        })
        .catch(error => {
            console.error('Error:', error);
        });
}


// SignalR

$(function () {
    let chat = $.connection.chatHub;

    chat.client.boardcastMessage = function (senderId, receiverId, content) {

        /*console.log(`senderId : ${senderId} - receiverId : ${receiverId} - content : ${content} `);*/

        //if mesage send to us
        if (currentUserID === Number(receiverId)) {

            // if sende is active user
            if (Number($(activeUserID).val()) === Number(senderId)) {
                msglistWrap.innerHTML += `<div class="message recived">
                                            <div class="mesaage">
                                                ${htmlEncode(content)}
                                            </div>
                                        </div>`;

                // scrool to last msg
                msglist.scrollTop = msglist.scrollHeight;
            } else {
                // add unread class to user
                document.getElementById("user" + senderId).classList.add("unread");
            }
            
        }

        
    }

    //$('#displayname').val(prompt('Enter Your Name:', ""));
    msgTextInput.focus();

    $.connection.hub.start().done(function () {
        $(sendBtn).click(function () {

            const content = $(msgTextInput).val();

            //if content not empty
            if (content) {

                //send msg
                chat.server.send(
                    currentUserID, // senderId
                    $(activeUserID).val(), // receiverId
                    content// content
                );

                // add content to msg list
                msglistWrap.innerHTML += `<div class="message send">
                                            <div class="mesaage">
                                                ${htmlEncode(content)}
                                            </div>
                                        </div>`;

                

                // clean input
                $(msgTextInput).val('');
            }
            
            $(msgTextInput).focus();


            // scrool to last msg
            msglist.scrollTop = msglist.scrollHeight;

        });
    });
});


const htmlEncode = (input) => {
    const element = document.createElement("div");
    element.innerText = input;
    return element.innerHTML;
}