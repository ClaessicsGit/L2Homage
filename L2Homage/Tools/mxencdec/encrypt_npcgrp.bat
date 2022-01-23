"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\npcgrp-new.ddf" "%~dp0..\..\Data\decrypted client files\npcgrp.txt" "npcgrp.dat"
echo x|mxencdec.exe npcgrp.dat
move npcgrp.dat "%~dp0..\..\Export\Client"