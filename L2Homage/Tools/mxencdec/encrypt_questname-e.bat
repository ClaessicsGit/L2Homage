"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\questname-e-new.ddf" "%~dp0..\..\Data\decrypted client files\questname-e.txt" "questname-e.dat"
echo x|mxencdec.exe questname-e.dat
move questname-e.dat "%~dp0..\..\Export\Client"