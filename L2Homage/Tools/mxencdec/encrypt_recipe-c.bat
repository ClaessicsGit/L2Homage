"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\recipe-c-new.ddf" "%~dp0..\..\Data\decrypted client files\recipe-c.txt" "recipe-c.dat"
echo x|mxencdec.exe recipe-c.dat
move recipe-c.dat "%~dp0..\..\Export\Client"