class Emitter {
  events = new Map(); //Коллекция ключ-значение

  on(eventName, subscriber, handler) {
    if (!this.events.has(eventName)) {
      this.events.set(eventName, []);
    }

    this.events.get(eventName).push({
      subscriber: subscriber,
      handler: handler.bind(subscriber), //Привязка функции-обработчика
    });
    return this;
  }

  off(eventName, subscriber) {
    if (this.events.has(eventName)) {
      let event = this.events.get(eventName);
      for (let i = 0; i < event.length; i++) {
        if (event[i].subscriber === subscriber)
          this.events.get(eventName).splice(i, 1);
      }
    }
    return this;
  }

  emit(eventName) {
    if (this.events.has(eventName)) {
      console.info(this.events.get(eventName));
      let event = this.events.get(eventName);

      for (let i = 0; i < event.length; i++) {
        event[i].handler();
      }
    }
    return this;
  }
}

var emitter = new Emitter();

//Это объект-подписчик и функция-обработчик
var notifications = {
  counter: 0,
  count: function () {
    this.counter++;
  },
};

console.log(emitter.on("new_notification", notifications, notifications.count));
emitter.on("new_notification", notifications, notifications.count);
emitter.on("new_notification", notifications, notifications.count);
emitter.off("new_notification", notifications);

emitter.emit("new_notification");

console.info(notifications.counter);
