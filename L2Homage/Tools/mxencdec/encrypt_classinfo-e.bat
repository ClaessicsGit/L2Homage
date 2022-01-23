"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\classinfo-e-new.ddf" "%~dp0..\..\Data\decrypted client files\classinfo-e.txt" "classinfo-e.dat"
echo x|mxencdec.exe classinfo-e.dat
move classinfo-e.dat "%~dp0..\..\Export\Client"