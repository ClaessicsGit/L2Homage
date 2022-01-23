"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\raiddata-e-new.ddf" "%~dp0..\..\Data\decrypted client files\raiddata-e.txt" "raiddata-e.dat"
echo x|mxencdec.exe raiddata-e.dat
move raiddata-e.dat "%~dp0..\..\Export\Client"