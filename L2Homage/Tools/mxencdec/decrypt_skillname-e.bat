echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\skillname-e.dat" skillname-e.dat

echo x|mxencdec.exe skillname-e.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\skillname-e.ddf" -e "%~dp0l2asm-disasm\newdats\skillname-e-new.ddf" "skillname-e.dat" "%~dp0..\..\Data\decrypted client files\skillname-e.txt"
del skillname-e.dat /f /q

exit