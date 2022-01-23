"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\sysstring-e-new.ddf" "%~dp0..\..\Data\decrypted client files\sysstring-e.txt" "sysstring-e.dat"
echo x|mxencdec.exe sysstring-e.dat
move sysstring-e.dat "%~dp0..\..\Export\Client"