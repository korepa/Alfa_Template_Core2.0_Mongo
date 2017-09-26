# Alfa_Template_Core2.0_Mongo

Шаблон Asp.Net Core 2.0 в связке с MongoDB:
ASP.NET MVC CORE 2.0
MONGO DB (> 3.4)

Базу Mongo DB можно взять тут:
https://www.mongodb.com/download-center#community



Инструкция:

1.	Скачиваем MongoDB с оффициального сайта https://www.mongodb.com/download-center#community
2.	Разворачиваем его, и создаем пользователя admin в бд
3.	Далее нужно создать файл конфигурации. Например, такой: 

systemLog:
	destination: file
	path: "C:\\data\\mongo\\db\\log\\mongo.log"
	logAppend: true
storage:
	dbPath: "C:\\data\\mongo\\db"
security:
	authorization: enabled

4.	Для запуска БД Mongo достаточно вызвать скрипт (MongoScripts\run_mongo.bat)
5.	Запускается проект по адресу http://localhost:50555/api/notes и отображает содержимое таблицы Notes
6.	Для инициализации БД нужно перейти по адресу http://localhost:50555/api/system/init
7.	Для мониторинга БД можно использовать Robo 3T, для теста WebApi можно использовать Postman
