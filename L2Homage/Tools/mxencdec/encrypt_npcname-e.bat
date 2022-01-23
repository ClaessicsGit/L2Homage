"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\npcname-e-new.ddf" "%~dp0..\..\Data\decrypted client files\npcname-e.txt" "npcname-e.dat"
echo x|mxencdec.exe npcname-e.dat
move npcname-e.dat "%~dp0..\..\Export\Client"