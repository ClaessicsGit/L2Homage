echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\gametip-e.dat" gametip-e.dat

echo x|mxencdec.exe gametip-e.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\gametip-e.ddf" -e "%~dp0l2asm-disasm\newdats\gametip-e-new.ddf" "gametip-e.dat" "%~dp0..\..\Data\decrypted client files\gametip-e.txt"
del gametip-e.dat /f /q

exit