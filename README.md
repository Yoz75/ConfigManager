# ConfigManager

##Eng
###How to use
_1) Import namespace:_ <br>
```using namespace ConfigManager;``` <br>
_2) Create new TextConfigManager object (or create new class that implements IConfigManager):_ <br>
```TextConfigManager GameSavesConfig = new TextConfigManager("save0".txt);``` <br>
_3) Add any data to config:_ <br>
```GameSavesConfig.AddDataToConfig("playerName","Andrey");``` <br>
_3) You can edit existing config data:_ <br>
```GameSavesConfig.SetDataInConfig("playerName", "New player name);``` <br>
_4) Or get variable from config:_ <br>
```string? playerName = GameSavesConfig.GetDataFromConfig("playerName);``` <br>


##Rus
###Использование:
_1) Импортируйте неймспейс:_ <br>
```using namespace ConfigManager;``` <br>
_2) Создайте новый объект типа TextConfigManager (или реализуйте интерфейс IConfigManager):_ <br>
```TextConfigManager GameSavesConfig = new TextConfigManager("save0".txt);``` <br>
_3) Добавьте любые данные в конфиг:_ <br>
```GameSavesConfig.AddDataToConfig("playerName","Andrey");``` <br>
_3) Или измените существующие данные:_ <br>
```GameSavesConfig.SetDataInConfig("playerName", "New player name);``` <br>
_4) Или просто получите данные:_ <br>
```string? playerName = GameSavesConfig.GetDataFromConfig("playerName);``` <br>
