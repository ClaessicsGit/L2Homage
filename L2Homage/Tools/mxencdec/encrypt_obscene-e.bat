"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\obscene-e-new.ddf" "%~dp0..\..\Data\decrypted client files\obscene-e.txt" "obscene-e.dat"
echo x|mxencdec.exe obscene-e.dat
move obscene-e.dat "%~dp0..\..\Export\Client"