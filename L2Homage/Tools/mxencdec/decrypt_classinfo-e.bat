echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\classinfo-e.dat" classinfo-e.dat

echo x|mxencdec.exe classinfo-e.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\classinfo-e.ddf" -e "%~dp0l2asm-disasm\newdats\classinfo-e-new.ddf" "classinfo-e.dat" "%~dp0..\..\Data\decrypted client files\classinfo-e.txt"
del classinfo-e.dat /f /q

exit