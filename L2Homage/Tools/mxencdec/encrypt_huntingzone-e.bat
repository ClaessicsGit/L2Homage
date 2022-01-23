"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\huntingzone-e-new.ddf" "%~dp0..\..\Data\decrypted client files\huntingzone-e.txt" "huntingzone-e.dat"
echo x|mxencdec.exe huntingzone-e.dat
move huntingzone-e.dat "%~dp0..\..\Export\Client"