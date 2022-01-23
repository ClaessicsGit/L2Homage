echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\instantzonedata-e.dat" instantzonedata-e.dat

echo x|mxencdec.exe instantzonedata-e.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\instantzonedata-e.ddf" -e "%~dp0l2asm-disasm\newdats\instantzonedata-e-new.ddf" "instantzonedata-e.dat" "%~dp0..\..\Data\decrypted client files\instantzonedata-e.txt"
del instantzonedata-e.dat /f /q

exit