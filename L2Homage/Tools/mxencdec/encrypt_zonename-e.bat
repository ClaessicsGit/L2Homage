"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\zonename-e-new.ddf" "%~dp0..\..\Data\decrypted client files\zonename-e.txt" "zonename-e.dat"
echo x|mxencdec.exe zonename-e.dat
move zonename-e.dat "%~dp0..\..\Export\Client"