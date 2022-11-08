//Блок с погодой
const weatherBlock = document.querySelector("#weather");

async function loadWeather(e) {
  weatherBlock.innerHTML = `
    <div class = "weather__loading">
      <img src = "img/loading.gif" alt = "Loading..."
    </div>`;

  const server =
    "https://api.openweathermap.org/data/2.5/weather?units=metric&lat=44.9572&lon=34.1108&appid=07add0da6ce99683eb312ff1d4882c8c";
    
  const response = await fetch(server, {
    method: "GET",
  }); //fetch-запрос

  const responseResult = await response.json(); //Пытаемся получить ответ в формате json

  if (response.ok) {
    getWeather(responseResult); //
  } else {
    weatherBlock.innerHTML = responseResult.message; //Выводится текст ошибки
  }
}

function getWeather(data) {
  //Обрабатываем и выводим данные

  const location = data.name;
  const temp = Math.round(data.main.temp);
  const feelsLike = Math.round(data.main.feels_like);
  const weatherStatus = data.weather[0].main;
  const weatherIcon = data.weather[0].icon;

  //Выведем эти данные в html
  const template = `
    <div class="weather__header">
         <div class="weather__main">
             <div class="weather__city">${location}</div>
             <div class="weather__status">${weatherStatus}</div>
         </div>
         <div class="weather__icon">
             <img src="http://openweathermap.org/img/wn/${weatherIcon}.png" alt="${weatherStatus}">
         </div>
    </div>
    <div class="weather__temp">${temp}</div>
     <div class="weather__feels-like">Feels like: ${feelsLike}</div>
    `;

    weatherBlock.innerHTML = template;
}

if (weatherBlock) {
  loadWeather();
}
