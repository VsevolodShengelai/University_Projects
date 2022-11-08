//Задание 8.1
function sum(firstNumber, secondNumber){
    return Number(firstNumber, 10) + Number(secondNumber,10);
}

console.info("Задание1. Тест1: " + sum('один', 2));
console.info("Задание1. Тест2: " + sum('25', 7));
console.info("Задание1. Тест3: " + sum('38 попугаев', 'два'));
console.info("Задание1. Тест4: " + sum(30, '28'));
console.info("Задание1. Тест5: " + sum('12 бегемотов', 2));

//Задание 8.2
function isValidTime(first, second) {
    return (first >= 0 & first <= 23 & second >= 0 & second <= 59)? true: false;
}

console.info("Задание2. Тест1:" + isValidTime(12, 49));
console.info("Задание2. Тест3:" + isValidTime(10, 58));
console.info("Задание2. Тест2:" + isValidTime(24, 30));
console.info("Задание2. Тест4:" + isValidTime(30, 90));
console.info("Задание2. Тест5:" + isValidTime(13, 65));

//Задание 8.3
function timeIncrement(hours, min, interval){
    var newMin = (min + interval) % 60; // остаток от деления на 60
    var newHours = (hours + Math.floor((min + interval)/60)) % 24;  // Тут мы используем библиотечную функцию Math.floor для округления в меньшую сторону
    //Отформатируем строки к виду ЧЧ:ММ
    var newHoursString = String(newHours);
    var newMinString = String(newMin);
    if (newHoursString.length < 2) {
        newHoursString = "0" + newHoursString;
    }
    if (newMinString.length < 2) {
        newMinString = "0" + newMinString;
    }
    return `${newHoursString}:${newMinString}`;  // составляем строку из полученных значений
  }
  
console.log(timeIncrement(13, 30, 39));
console.log(timeIncrement(23, 30, 40));
console.log(timeIncrement(23, 59, 187));
console.log(timeIncrement(1, 20, 24));
console.log(timeIncrement(4, 4, 4));