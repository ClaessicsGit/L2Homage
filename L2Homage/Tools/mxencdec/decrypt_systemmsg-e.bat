echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\systemmsg-e.dat" systemmsg-e.dat

echo x|mxencdec.exe systemmsg-e.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\systemmsg-e.ddf" -e "%~dp0l2asm-disasm\newdats\systemmsg-e-new.ddf" "systemmsg-e.dat" "%~dp0..\..\Data\decrypted client files\systemmsg-e.txt"
del systemmsg-e.dat /f /q

exit