echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\zonename-e.dat" zonename-e.dat

echo x|mxencdec.exe zonename-e.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\zonename-e.ddf" -e "%~dp0l2asm-disasm\newdats\zonename-e-new.ddf" "zonename-e.dat" "%~dp0..\..\Data\decrypted client files\zonename-e.txt"
del zonename-e.dat /f /q

exit