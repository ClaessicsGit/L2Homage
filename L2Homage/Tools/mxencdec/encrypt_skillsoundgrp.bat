"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\skillsoundgrp-new.ddf" "%~dp0..\..\Data\decrypted client files\skillsoundgrp.txt" "skillsoundgrp.dat"
echo x|mxencdec.exe skillsoundgrp.dat
move skillsoundgrp.dat "%~dp0..\..\Export\Client"