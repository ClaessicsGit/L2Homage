"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\gametip-e-new.ddf" "%~dp0..\..\Data\decrypted client files\gametip-e.txt" "gametip-e.dat"
echo x|mxencdec.exe gametip-e.dat
move gametip-e.dat "%~dp0..\..\Export\Client"