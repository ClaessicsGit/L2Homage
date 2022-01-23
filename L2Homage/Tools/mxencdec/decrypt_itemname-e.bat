echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\itemname-e.dat" itemname-e.dat

echo x|mxencdec.exe itemname-e.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\itemname-e.ddf" -e "%~dp0l2asm-disasm\newdats\itemname-e-new.ddf" "itemname-e.dat" "%~dp0..\..\Data\decrypted client files\itemname-e.txt"
del itemname-e.dat /f /q

exit