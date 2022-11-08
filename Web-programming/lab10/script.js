//Задание 10
function date (dateString) {
    objectWithTime = {
      setDate(dateString) {
        this.date = new Date(dateString);
        return this;
      },
  
      method: {
        years: 'FullYear', 
        months: 'Month',
        days: 'Date',
        hours: 'Hours',
        minutes: 'Minutes'
      },
  
      add(num, unit, subtract) {
        if (num < 0) {
          throw new TypeError;
        } else if (unit in this.method) {
          return this.Calculate(num, this.method[unit], subtract);
        } else { //Если получили неизвестную единицу
          throw new TypeError;
        }
      },
  
      Calculate(num, method, subtract) {
        if (subtract) {
          this.date['set'+ method](this.date['get'+ method]() - num);
          return this;
        }
        this.date['set'+ method](this.date['get'+ method]() + num);
        return this;
      },
  
      subtract(num, unit) {
        return this.add(num, unit, 'subtract');
      },
  
      value() {
        var year = this.date.getFullYear();
        var month = this.date.toLocaleString("ru",{month: '2-digit'});
        var day = this.date.toLocaleString("ru",{day: '2-digit'});
        var time = this.date.toLocaleString("ru",{ hour: '2-digit', minute: '2-digit' });
        var result = year + "-" + month + "-" + day + " "+time;
        console.log(result);
        return result;
      }
    }
  
    return objectWithTime.setDate(dateString);
  };
  
  var string = '2022-06-11 14:15';
  
  var time = date(string);

  time.add(25, 'hours');
  time.subtract(1, 'months');
  time.add(1, 'days');
  time.add(15, 'minutes').add(1, 'days');
  time.value();