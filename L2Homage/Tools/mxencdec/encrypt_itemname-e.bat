"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\itemname-e-new.ddf" "%~dp0..\..\Data\decrypted client files\itemname-e.txt" "itemname-e.dat"
echo x|mxencdec.exe itemname-e.dat
move itemname-e.dat "%~dp0..\..\Export\Client"