"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\npcstring-e-new.ddf" "%~dp0..\..\Data\decrypted client files\npcstring-e.txt" "npcstring-e.dat"
echo x|mxencdec.exe npcstring-e.dat
move npcstring-e.dat "%~dp0..\..\Export\Client"