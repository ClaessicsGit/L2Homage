echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\charcreategrp.dat" charcreategrp.dat

echo x|mxencdec.exe charcreategrp.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\charcreategrp.ddf" -e "%~dp0l2asm-disasm\newdats\charcreategrp-new.ddf" "charcreategrp.dat" "%~dp0..\..\Data\decrypted client files\charcreategrp.txt"
del charcreategrp.dat /f /q

exit