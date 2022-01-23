"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\skillgrp-new.ddf" "%~dp0..\..\Data\decrypted client files\skillgrp.txt" "skillgrp.dat"
echo x|mxencdec.exe skillgrp.dat
move skillgrp.dat "%~dp0..\..\Export\Client"