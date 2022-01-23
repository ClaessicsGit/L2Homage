"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\instantzonedata-e-new.ddf" "%~dp0..\..\Data\decrypted client files\instantzonedata-e.txt" "instantzonedata-e.dat"
echo x|mxencdec.exe instantzonedata-e.dat
move instantzonedata-e.dat "%~dp0..\..\Export\Client"