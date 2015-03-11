var octIm = {};
var chat;
octIm.init = function (host,fun) {
    if (host != '') {
        $.connection.hub.url = host + "/signalr";
    }

    // Proxy created on the fly
    chat = $.connection.pushHub;
    // Declare a function on the chat hub so the server can invoke it
    chat.client.sysMsg = function (title, message) {
        writeEvent('<b>' + title + '</b> 对大家说: ' + message, 'event-message');
    };

    // Start the connection

    $.connection.hub.start({ xdomain: true }).done(function () {
        fun();
    });

    //A function to write events to the page
    function writeEvent(eventLog, logClass) {
        var now = new Date();
        var nowStr = now.getHours() + ':' + now.getMinutes() + ':' + now.getSeconds();
        $('#messages').prepend('<li class="' + logClass + '"><b>' + nowStr + '</b> ' + eventLog + '.</li>');
    }
}

octIm.login = function (guid, platform) {
    chat.server.login(guid, platform)
        .done(function () { try { console.log('登录成功!'); } catch (e) { } })
        .fail(function (e) { try { console.warn(e); } catch (e) { } });
}

octIm.sysMsg = function (title, msg) {
    chat.server.sysMsg(title, msg)
        .done(function () { try { console.log('消息发送成功!'); } catch (e) { } })
        .fail(function (e) { try { console.warn(e); } catch (e) { } });
}

octIm.send2 = function (from,to,title, msg) {
    chat.server.send2(from, to, title, msg)
        .done(function () { try { console.log('消息发送成功!'); } catch (e) { } })
        .fail(function (e) { try { console.warn(e); } catch (e) { } });
}