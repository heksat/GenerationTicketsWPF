for /r %CD% %%i in (*.cs *.xaml) do (echo %%i > out.txt & more %%i > out.txt)