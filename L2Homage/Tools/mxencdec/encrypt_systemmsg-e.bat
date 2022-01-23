"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\systemmsg-e-new.ddf" "%~dp0..\..\Data\decrypted client files\systemmsg-e.txt" "systemmsg-e.dat"
echo x|mxencdec.exe systemmsg-e.dat
move systemmsg-e.dat "%~dp0..\..\Export\Client"