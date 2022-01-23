echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\obscene-e.dat" obscene-e.dat

echo x|mxencdec.exe obscene-e.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\obscene-e.ddf" -e "%~dp0l2asm-disasm\newdats\obscene-e-new.ddf" "obscene-e.dat" "%~dp0..\..\Data\decrypted client files\obscene-e.txt"
del obscene-e.dat /f /q

exit