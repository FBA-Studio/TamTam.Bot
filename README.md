<h1 align="center"><img src="https://github.com/FBA-Studio/TamTam.Bot/blob/main/raws/tamtamb-bot-logo.svg" align="center" height="46"></img>TamTam.Bot</h1>
<h2>Установка библиотеки</h2>
В терминале через менеджер пакетов NuGet для .NET-проектов(Pre-Release):

```
dotnet add package TamTam.Bot
```

## Документация
### - Bot Long Poll
- Получаем токен нашего бота в https://tt.me/PrimeBot
- Инициализируем объект TamTamClient:
```csharp
using TamTam.Bot;
using TamTam.Bot.Types;

var bot = new TamTamClient("your token here");
```
- Стартуем лонг полл!
```csharp
static async Task Main() {
  bot.StartPolling(YourUpdateHandler);
  while(true) { //your backend } // Чтобы не завершилась программа
}

static async Task YourUpdateHandler(Update update) { // ⚠️Метод обязательно должен быть асинхронным Task, принимающий аргумент формата Update
  // Код обработки вашего апдейта
}

```

#### ❕Документация находится в разработке! В релизе мы доделаем её

