"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\eula-e-new.ddf" "%~dp0..\..\Data\decrypted client files\eula-e.txt" "eula-e.dat"
echo x|mxencdec.exe eula-e.dat
move eula-e.dat "%~dp0..\..\Export\Client"