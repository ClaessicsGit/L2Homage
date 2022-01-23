echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\raiddata-e.dat" raiddata-e.dat

echo x|mxencdec.exe raiddata-e.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\raiddata-e.ddf" -e "%~dp0l2asm-disasm\newdats\raiddata-e-new.ddf" "raiddata-e.dat" "%~dp0..\..\Data\decrypted client files\raiddata-e.txt"
del raiddata-e.dat /f /q

exit