echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\npcgrp.dat" npcgrp.dat

echo x|mxencdec.exe npcgrp.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\npcgrp.ddf" -e "%~dp0l2asm-disasm\newdats\npcgrp-new.ddf" "npcgrp.dat" "%~dp0..\..\Data\decrypted client files\npcgrp.txt"
del npcgrp.dat /f /q

exit