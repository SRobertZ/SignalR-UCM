var globalConnection = null;
var hubApi = {
    initNewConnection: function (options) {
        var connection = $.hubConnection(options.url);
        connection.logging = (options && options.logging ? true : false);
        return connection;
    },
    getConnection: function (options) {
        var useSharedConnection = !(options && options.useSharedConnection === false);
        if (useSharedConnection) {
            return globalConnection === null ? globalConnection = hubApi.initNewConnection(options) : globalConnection;
        }
        else {
            return hubApi.initNewConnection(options);
        }
    }
};
var logger = {
    logEnabled: false,
    createLogger: function (options) {
        this.logEnabled = (options && options.logging ? true : false);
    },
    log: function (message) {
        if (this.logEnabled) {
            console.log(message);
        }
    }
};

var options = {
    hubName: 'emptyHub',
    timeoutReconnect: 5000,
    url: 'http://localhost:1604',
    logging: true,
    useSharedConnection: true
};
var hub = {};

var bus = {
    publish: function (topic,data) {
        postal.publish({
            channel: "test",
            topic: topic,
            data: data
        });
    }
}

logger.createLogger(options);
hub.connection = hubApi.getConnection(options);
hub.proxy = hub.connection.createHubProxy(options.hubName);
hub.proxy.on('notification', function (handlerName, data) {
    logger.log("IO event handled: ", handlerName, data);
    console.log(data.Text);
    bus.publish(handlerName, data);
});
hub.connection.disconnected(function (connection) {
    //if (connection) {
    //	logger.log("Disconnected, transport = " + connection.transport.name);
    //	logger.log('Now connected, connection ID=' + connection.id);
    //}
    bus.publish('signalr.disconnect', connection);
    setTimeout(function () {
        hub.connection.start();
    }, options.timeoutReconnect); // Re-start connection after 5 seconds
});

setTimeout(function () {
    hub.connection.start({ transport: 'longPolling' }).done(function (connection) {
        if (connection) {
            logger.log("Connected, transport = " + connection.transport.name);
            logger.log('Now connected, connection ID=' + connection.id);
            
            bus.publish('signalr.connect', connection);
        }
    });
}, 1000);