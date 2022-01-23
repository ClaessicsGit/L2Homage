echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\skillsoundgrp.dat" skillsoundgrp.dat

echo x|mxencdec.exe skillsoundgrp.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\skillsoundgrp.ddf" -e "%~dp0l2asm-disasm\newdats\skillsoundgrp-new.ddf" "skillsoundgrp.dat" "%~dp0..\..\Data\decrypted client files\skillsoundgrp.txt"
del skillsoundgrp.dat /f /q

exit