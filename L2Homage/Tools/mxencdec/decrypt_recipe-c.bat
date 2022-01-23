echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\recipe-c.dat" recipe-c.dat

echo x|mxencdec.exe recipe-c.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\recipe-c.ddf" -e "%~dp0l2asm-disasm\newdats\recipe-c-new.ddf" "recipe-c.dat" "%~dp0..\..\Data\decrypted client files\recipe-c.txt"
del recipe-c.dat /f /q

exit