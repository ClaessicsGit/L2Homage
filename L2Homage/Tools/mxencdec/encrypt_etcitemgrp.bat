"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\etcitemgrp-new.ddf" "%~dp0..\..\Data\decrypted client files\etcitemgrp.txt" "etcitemgrp.dat"
echo x|mxencdec.exe etcitemgrp.dat
move etcitemgrp.dat "%~dp0..\..\Export\Client"