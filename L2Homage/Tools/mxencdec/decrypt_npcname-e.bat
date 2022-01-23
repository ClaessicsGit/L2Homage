echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\npcname-e.dat" npcname-e.dat

echo x|mxencdec.exe npcname-e.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\npcname-e.ddf" -e "%~dp0l2asm-disasm\newdats\npcname-e-new.ddf" "npcname-e.dat" "%~dp0..\..\Data\decrypted client files\npcname-e.txt"
del npcname-e.dat /f /q

exit