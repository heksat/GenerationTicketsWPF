sqlcmd -S .\SQLEXPRESS -v FullScriptDir="%CD%" -i SQLQuery_CreateDB.sql -o log.txt