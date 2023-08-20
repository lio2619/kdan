# kdan 專案部屬說明

## 安裝環境
### 資料庫
請先去[mariadb官網](https://mariadb.org/download/?t=mariadb&p=mariadb&r=10.11.5&os=windows&cpu=x86_64&pkg=msi&m=ossplanet)安裝資料庫
資料庫版本請安裝10.11版

### C#環境
請先去[Visual Studio官網](https://visualstudio.microsoft.com/zh-hant/downloads/)安裝C#
Visual Stuudio版本請安裝2022年的

## 建立資料庫
### 步驟1
請開啟visual studio後到appsettings.json裡面，把ConnectionStrings下的密碼改成自己電腦裡設定的資料庫密碼

### 步驟2
進入套件管理器主控台，輸入以下指令
```
Update-Database
```
輸入完畢就建立完成資料庫了

## 啟動server
### visual studio
直接按F5或點擊上面的綠色啟動鍵啟動server

### docker
請先開啟CLI介面並且移動到專案路徑，然後輸入以下指令
```
docker build . -t example
docker run -p 8888:80 example
```