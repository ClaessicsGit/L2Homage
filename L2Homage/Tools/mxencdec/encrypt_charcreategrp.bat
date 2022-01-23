"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\charcreategrp-new.ddf" "%~dp0..\..\Data\decrypted client files\charcreategrp.txt" "charcreategrp.dat"
echo x|mxencdec.exe charcreategrp.dat
move charcreategrp.dat "%~dp0..\..\Export\Client"