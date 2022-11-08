//Задание 9.1
function getHashTags(str){
    var word_array = str.split(" ");

    var result = [];
    for(var i=0; i < word_array.length; i++){
        if(word_array[i].includes('#')) 
            result.push(word_array[i].slice(1));
    }
    return result;
}

console.info('Строка без Хештегов');
console.info(getHashTags('Прохожу курc по #javascript и #web'));

// Задание 9.2
console.info("Задание 9.2")
function normalizeHashTags(array){
    for (var i =0; i < array.length; i++) array[i] = array[i].toLowerCase();

    /*Решение с помощью создания коллекции из массива
    И приведения её к массиву*/
    const newSet = new Set(array);
    const uniqueStr = Array.from(newSet);

    return uniqueStr.join(', ');
}

console.info(normalizeHashTags([])); //Выведется пустая строка
console.info(normalizeHashTags(['web', 'JavaScipt', 'Web', 'script', 'programming']));


//Задание 9.3
function phoneBook(command) {
    const commands = command.split(' ');
    const nameOfCommand = commands[0];

    switch (nameOfCommand.toUpperCase()) {
        case "ADD":
            return add();
        case "REMOVE_PHONE":
            return remove();
        case "SHOW":
            return show();
        default:
            return 0;
    }

    function add() {
        const newName = commands[1];
        const numberToAdd = commands[2].split(',');

        if (phoneBook.hasOwnProperty(newName)) {
            const initialPhoneArray = phoneBook[newName].split(', ');
            const updatePhoneList = initialPhoneArray.concat(numberToAdd);
            phoneBook[newName] = updatePhoneList.join(', ');
            return;
        }
        phoneBook[newName] = numberToAdd.join(', ');
    }

    function remove() {
        const numberToRemove = commands[1];
        for (const userName in phoneBook) {
            if (phoneBook[userName] === numberToRemove) {
                delete phoneBook[userName];
            }
            else if (phoneBook[userName].split(', ').indexOf(numberToRemove) !== -1) {
                const initialPhoneArray = phoneBook[userName].split(', ');
                for (let i = 0; i < initialPhoneArray.length; i++) {
                    if (initialPhoneArray[i] === numberToRemove){
                        initialPhoneArray.splice(i, 1);
                    }
                }
                phoneBook[userName] = initialPhoneArray.join(', ');
            }
        }
    }

    function show() {
        const numberToShow = [];
        let i = 0;
        for (const number in phoneBook) {
            numberToShow[i] = `${number}: ${phoneBook[number]} `;
            i++;
        }
        return numberToShow.sort();
    }
}

console.info('Задание 3');
phoneBook('ADD Ivan 555-10-01,555-10-03');
phoneBook('ADD Ivan 555-10-02');
console.info(phoneBook('SHOW'));
phoneBook('REMOVE_PHONE 555-10-03');
phoneBook('ADD Alex 555-20-01');
console.info(phoneBook('SHOW'));
phoneBook('REMOVE_PHONE 555-20-01');
console.info(phoneBook('SHOW'));