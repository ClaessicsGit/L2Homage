echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\questname-e.dat" questname-e.dat

echo x|mxencdec.exe questname-e.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\questname-e.ddf" -e "%~dp0l2asm-disasm\newdats\questname-e-new.ddf" "questname-e.dat" "%~dp0..\..\Data\decrypted client files\questname-e.txt"
del questname-e.dat /f /q

exit