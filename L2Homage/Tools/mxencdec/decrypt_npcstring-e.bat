echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\npcstring-e.dat" npcstring-e.dat

echo x|mxencdec.exe npcstring-e.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\npcstring-e.ddf" -e "%~dp0l2asm-disasm\newdats\npcstring-e-new.ddf" "npcstring-e.dat" "%~dp0..\..\Data\decrypted client files\npcstring-e.txt"
del npcstring-e.dat /f /q

exit