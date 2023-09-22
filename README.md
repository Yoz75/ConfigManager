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
```GameSavesConfig.GetDataFromConfig("playerName);``` <br>
