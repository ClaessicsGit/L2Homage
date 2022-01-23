"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\skillname-e-new.ddf" "%~dp0..\..\Data\decrypted client files\skillname-e.txt" "skillname-e.dat"
echo x|mxencdec.exe skillname-e.dat
move skillname-e.dat "%~dp0..\..\Export\Client"