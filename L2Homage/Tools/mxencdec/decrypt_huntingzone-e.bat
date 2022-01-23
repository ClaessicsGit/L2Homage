echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\huntingzone-e.dat" huntingzone-e.dat

echo x|mxencdec.exe huntingzone-e.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\huntingzone-e.ddf" -e "%~dp0l2asm-disasm\newdats\huntingzone-e-new.ddf" "huntingzone-e.dat" "%~dp0..\..\Data\decrypted client files\huntingzone-e.txt"
del huntingzone-e.dat /f /q

exit