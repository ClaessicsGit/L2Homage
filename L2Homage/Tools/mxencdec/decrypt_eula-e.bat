echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\eula-e.dat" eula-e.dat

echo x|mxencdec.exe eula-e.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\eula-e.ddf" -e "%~dp0l2asm-disasm\newdats\eula-e-new.ddf" "eula-e.dat" "%~dp0..\..\Data\decrypted client files\eula-e.txt"
del eula-e.dat /f /q

exit