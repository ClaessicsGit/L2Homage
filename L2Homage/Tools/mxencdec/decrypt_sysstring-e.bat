echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\sysstring-e.dat" sysstring-e.dat

echo x|mxencdec.exe sysstring-e.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\sysstring-e.ddf" -e "%~dp0l2asm-disasm\newdats\sysstring-e-new.ddf" "sysstring-e.dat" "%~dp0..\..\Data\decrypted client files\sysstring-e.txt"
del sysstring-e.dat /f /q

exit